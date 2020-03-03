using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FH.BLL.Hubs;
using FH.BLL.Infrastructure;
using FH.BLL.Interfaces;
using FH.BLL.VMs;
using FH.DAL.EF.Interfaces;
using FH.Models.Models;
using FH.Models.StaticModels;
using Microsoft.AspNetCore.SignalR;

namespace FH.BLL.Services
{
    public class OrderService : IOrderService
    {
        private IUnitOfWork _db { get; set; } 
        private readonly IHubContext<OrderHub> _hub;
        //private readonly 
        public OrderService(IUnitOfWork uow, IHubContext<OrderHub> hub)
        {
            _db = uow;
            _hub = hub;
        }

        public async Task<OrderPageVM> CreateNewOrder(CreateOrderVM vm, string userId)
        {
            var dbOrder = new Order
            {  
                IsActive = vm.IsActive,
                    Status = "In progress",
                    StartDate = vm.StartDate,
                    TableId = vm.TableId,
                    //LocationId = vm.LocationId
            };
            var orderNew = await _db.Orders.CreateAsync(dbOrder);
            var dbOrderUser = new OrderUser
            {
                OrderId = orderNew.Id,
                UserProfileId = _db.UserProfiles.GetAll().FirstOrDefault(m=>m.UserId==userId).Id
            };
            var orderUserNew = await _db.OrderUsers.CreateAsync(dbOrderUser);
            var ret = new OrderPageVM(orderNew);
            return ret;
        }

        public async Task<OrderPageVM> AssignManagerToOrder(int orderId, string userId)
        {
            var order = await _db.Orders.GetByIdAsync(orderId);
            if (!order.IsActive)
            {
                throw new Exception("Order session is already cancel.");
            }

            var myId = _db.UserProfiles.GetAll().FirstOrDefault(m => m.UserId == userId).Id;
            if (order.ManagerId==myId)
            {
                throw new Exception("You are already assigned to this order");
            }

            var myManagerId = _db.Managers.GetAll().FirstOrDefault(m => m.UserProfileId == myId).Id;
            if (order.ManagerId == myManagerId)
            {
                throw new Exception("You are already assigned to this order");
            }

            order.ManagerId = myManagerId;
            var dbOrder = await _db.Orders.UpdateAsync(order);
            return new OrderPageVM(await _db.Orders.CreateAsync(dbOrder));
        }

        public async Task<OrderPageTabVM> AssignMeToOrder(int orderId, string welcomeCode, string userId)
        {
            var order = await _db.Orders.GetByIdAsync(orderId);
            if (!order.IsActive || order.WelcomeCode.ToString() != welcomeCode)
            {
                throw new Exception("Order session is already cancel (or invalid WelcomeCode)");
            }

            var myId = _db.UserProfiles.GetAll().FirstOrDefault(m => m.UserId == userId).Id;
            if (_db.OrderUsers.GetAll().Any(m => m.OrderId == order.Id && m.UserProfileId == myId))
            {
                throw new Exception("You are already assigned this order");
            }
            var dbOrderUser = new OrderUser
            {
                OrderId = order.Id,
                UserProfileId = _db.UserProfiles.GetAll().FirstOrDefault(m => m.UserId == userId).Id
            }; 
            return new OrderPageTabVM(await _db.OrderUsers.CreateAsync(dbOrderUser));
        }

        public async Task CancelOrder(int orderId)
        {
            var order = await _db.Orders.GetByIdAsync(orderId);
            if (!order.IsActive)
            {
                throw new Exception("Order session is already cancel");
            }

            order.IsActive = false;
            order.EndDate=DateTime.Now;
            await _db.Orders.UpdateAsync(order);
        }

        public async Task<OrderPageVM> GetOrderByIdAsync(int orderId, string userId)
        {
            var order = await _db.Orders.GetByIdAsync(orderId); 

            var myId = _db.UserProfiles.GetAll().FirstOrDefault(m => m.UserId == userId).Id;
            if (!_db.OrderUsers.GetAll().Any(m => m.OrderId == order.Id && m.UserProfileId == myId)) 
            {
                throw new Exception("You are not assigned this order");
            }
            return new OrderPageVM(order);
        }

        public async Task<OrderPageVM> GetCurrentOrder(string userId)
        {
            var myId = _db.UserProfiles.GetAll().FirstOrDefault(m => m.UserId == userId).Id;
            var order = _db.OrderUsers.GetAll().FirstOrDefault(m => m.UserProfileId == myId && m.Order.IsActive).Order;  
            return new OrderPageVM(order);
        }

        public List<OrderTabVM> GetAllMyOrders(string userId)
        {
            var myId = _db.UserProfiles.GetAll().FirstOrDefault(m => m.UserId == userId).Id;
            var history = _db.OrderUsers.GetAll().Where(m => m.UserProfileId == myId).ToList();
            var orders = history.Select(m => new OrderTabVM(m.Order)).ToList();
            return orders;
        }

        public async Task<ManagerCallVM> CreateNewManagerCall(ManagerCallVM vm)
        {
            var dbCall = new ManagerCall
            {
                IsActive = vm.IsActive,
                Comment = vm.Comment,
                CallTime = vm.CallTime,
                OrderId = vm.OrderId
            };
            var newCall = await _db.ManagerCalls.CreateAsync(dbCall); 
            var ret = new ManagerCallVM(newCall);
            return ret;
        }

        public async Task<OrderItemVM> CreateNewOrderItem(CreateOrderItemVM vm)
        {
            var dbItem = new OrderItem
            {
                OrderId = vm.OrderId,
                Count = vm.Count,
                MenuItemId = vm.MenuItemId,
                PricePerItem = vm.PricePerItem,
                Status = vm.Status,
                Title = vm.Title,
                UserId = vm.UserProfileId
            };
            var newItem = await _db.OrderItems.CreateAsync(dbItem);
            var ret = new OrderItemVM(newItem);
            return ret;
        }

        public async Task DeleteOrderItem(int id)
        { 
            await _db.OrderItems.DeleteAsync(id);
        }

        public async Task<OrderItemVM> UpdateNewOrderItem(CreateOrderItemVM vm)
        {
            var dbItem = await _db.OrderItems.GetByIdAsync(vm.Id);
            dbItem.Count = vm.Count;
            dbItem.Status = vm.Status;
            var newItem = await _db.OrderItems.UpdateAsync(dbItem);
            var ret = new OrderItemVM(newItem);
            return ret;
        }
        public async Task<OrderItemVM> GetOrderItemByIdAsync(int id)
        {
            var dbItem = await _db.OrderItems.GetByIdAsync(id); 
            var ret = new OrderItemVM(dbItem);
            return ret;
        }

        public async Task<ManagerCallVM> AcceptManagerCall(int callId)
        {
            var callDb = await _db.ManagerCalls.GetByIdAsync(callId);
            callDb.IsActive = false;
            var newCall = await _db.ManagerCalls.UpdateAsync(callDb);
            var ret = new ManagerCallVM(newCall);
            return ret;
        }
    }
}
