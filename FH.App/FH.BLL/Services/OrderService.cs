﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using FH.App.Hubs;
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
        //private readonly IHubContext<OrderHub> _hub;
        //private readonly 
        public OrderService(IUnitOfWork uow/*, IHubContext<OrderHub> hub*/)
        {
            _db = uow;
           // _hub = hub;
        }

        public async Task<OrderPageVM> CreateNewOrder(CreateOrderVM vm, string userId)
        {
            var dbOrder = new Order
            {  
                IsActive = vm.IsActive,
                    Status = "In progress",
                    StartDate = vm.StartDate,
                    TableId = vm.TableId,
                    LocationId = vm.LocationId
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
            if (order == null || !order.IsActive)
            {
                throw new Exception("Order session is already cancel.");
            }

            var myId = _db.UserProfiles.GetAll().FirstOrDefault(m => m.UserId == userId).Id;

            var myManagerId = _db.Managers.GetAll().FirstOrDefault(m => m.UserProfileId == myId).Id;

            order.ManagerId = myManagerId;
            var dbOrder = await _db.Orders.UpdateAsync(order);
            return new OrderPageVM(dbOrder);
        }

        public async Task<OrderPageTabVM> AssignMeToOrder(string welcomeCode, string userId)
        {
            var order = _db.Orders.GetAll().FirstOrDefault(m=>m.WelcomeCode.ToString()== welcomeCode);
            if (order != null && (!order.IsActive || order.WelcomeCode.ToString() != welcomeCode))
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
            return new OrderPageTabVM(await _db.OrderUsers.CreateAsync(dbOrderUser), order.Location.Currency);
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
            if (order==null)
            {
                throw new Exception("Order is not found");
            }

            var myId = _db.UserProfiles.GetAll().FirstOrDefault(m => m.UserId == userId).Id;
            if (!_db.OrderUsers.GetAll().Any(m => m.OrderId == order.Id && m.UserProfileId == myId) && !_db.OrderUsers.GetAll().Any(m => m.OrderId == order.Id && m.Order.Manager.UserProfileId == myId)) 
            {
                throw new Exception("You are not assigned this order");
            }
            return new OrderPageVM(order);
        }

        public OrderPageVM GetCurrentOrder(string userId)
        {
            var myId = _db.UserProfiles.GetAll().FirstOrDefault(m => m.UserId == userId).Id;
            var orderUs = _db.OrderUsers.GetAll().FirstOrDefault(m => m.UserProfileId == myId && m.Order.IsActive);
            if (orderUs == null)
            {
                throw new Exception("You are not assign to any active order.");
            }
            return new OrderPageVM(orderUs.Order);
        }

        public List<OrderTabVM> GetAllMyOrders(string userId)
        {
            var myId = _db.UserProfiles.GetAll().FirstOrDefault(m => m.UserId == userId).Id;
            var history = _db.OrderUsers.GetAll().Where(m => m.UserProfileId == myId).ToList();
            var orders = history.Select(m =>
            {
                if (m.Order != null && m.Order.Location != null && m.Order.Manager != null && m.Order.Manager.UserProfile != null)
                            return new OrderTabVM(m.Order, m.Order.Location, m.Order.Manager.UserProfile);
                return new OrderTabVM();
            }).ToList();
            return orders;
        }

        public List<OrderTabVM> GetAllMyManagerOrders(string userId)
        {
            try
            {
                var myId = _db.UserProfiles.GetAll().FirstOrDefault(m => m.UserId == userId).Id;
                var history = _db.Managers.GetAll().FirstOrDefault(m => m.UserProfileId == myId)?.Orders;
                if (history != null)
                {
                    var orders = history.Select(m => new OrderTabVM(m, m.Location, m.Manager.UserProfile)).ToList();
                    return orders;
                }
                return new List<OrderTabVM>();
            }
            catch (Exception e)
            {
                return new List<OrderTabVM>();
            }
        }

        public List<OrderTabVM> GetAllActiveLocationOrders(string userId)
        {
            var manager = _db.Managers.GetAll().FirstOrDefault(m => m.UserProfile.UserId == userId);
            if (manager == null)
            {
                return new List<OrderTabVM>();
            }
            var orders = manager.Location.Orders.Where(m => m.IsActive).Select(m => new OrderTabVM(m, m.Location, m.Manager.UserProfile)).ToList();
            return orders;
        }

        //public List<OrderTabVM> GetAllNoManagersLocationOrders(string userId)
        //{
        //    var manager = _db.Managers.GetAll().FirstOrDefault(m => m.UserProfile.UserId == userId);
        //    if (manager == null)
        //    {
        //        return new List<OrderTabVM>();
        //    }
        //    var orders = manager.Location.Orders.Where(m => m.ManagerId == null).Select(m => new OrderTabVM(m)).ToList();
        //    return orders;
        //}

        public List<OrderTabVM> GetAllLocationOrders(string userId)
        {
            var manager = _db.Managers.GetAll().FirstOrDefault(m => m.UserProfile.UserId == userId);
            if (manager == null)
            {
                return new List<OrderTabVM>();
            }

            if (manager.Location != null && manager.Location.Orders != null)
            {
                var orders = manager.Location.Orders;
                var orders2 = new List<OrderTabVM>();
                    foreach (var ord in orders)
                    {
                        var ord2 = _db.Orders.GetAll().FirstOrDefault(m=>m.Id==ord.Id);
                        if (ord2 != null) orders2.Add(new OrderTabVM(ord2, ord2.Location, ord2.Manager?.UserProfile??new UserProfile()));
                    } 
                    return orders2;
            }
            return new List<OrderTabVM>();
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
            var orderUser = _db.OrderUsers.GetAll().FirstOrDefault(m => m.UserProfileId == vm.UserProfileId && m.Order.IsActive);
            if (vm.OrderId == 0 && orderUser?.Order.Id != null)
            {
                vm.OrderId = orderUser.Order.Id;
            }

            var orderItem = orderUser?.Order.OrderItems.FirstOrDefault(m => m.MenuItemId == vm.MenuItemId && m.UserId==vm.UserProfileId);

            if (orderItem != null)
            {
                vm.Count = orderItem.Count+1;
                vm.Id = orderItem.Id;
                return await UpdateNewOrderItem(vm);
            }

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
            var order = _db.Orders.GetAll().FirstOrDefault(m => m.Id == vm.OrderId);
            return new OrderItemVM(newItem, order.Location.Currency);
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
            var order = _db.Orders.GetAll().FirstOrDefault(m => m.Id == dbItem.OrderId);
            if (order != null)
            {
                var ret = new OrderItemVM(newItem, order.Location.Currency);
                return ret;
            }
            throw new Exception("order not found");
        }
        public async Task<OrderItemVM> GetOrderItemByIdAsync(int id)
        {
            var dbItem = await _db.OrderItems.GetByIdAsync(id);
            var order = _db.Orders.GetAll().FirstOrDefault(m => m.Id == dbItem.OrderId);
            var ret = new OrderItemVM(dbItem, order.Location.Currency);
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
