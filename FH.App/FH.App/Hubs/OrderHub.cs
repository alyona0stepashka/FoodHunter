using Microsoft.AspNetCore.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FH.BLL.Infrastructure;
using FH.BLL.Interfaces;
using FH.BLL.VMs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;

namespace FH.App.Hubs
{
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class OrderHub : Hub
    {
        //public static List<OrderConnect> ordConnects = new List<OrderConnect>();
        public static List<Connect> connects = new List<Connect>();
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;

        public OrderHub(IOrderService orderService, IUserService userService)
        {
            _orderService = orderService;
            _userService = userService;
        }

        public override async Task OnConnectedAsync()
        {
            try
            {
                var callerId = Context.User.Claims.First(c => c.Type == "UserID").Value;
                UpdateList(callerId);
                await base.OnConnectedAsync();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public override async Task OnDisconnectedAsync(Exception ex)
        {
            try
            {
                connects.Remove(connects.Find(m => m.ConnectionId == Context.ConnectionId));
                await base.OnDisconnectedAsync(ex);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void DisconnectByOrder(int orderId, string userId)
        {
            foreach (var c in connects)
            {
                c.OrderIds.RemoveAll(m => m == orderId);
            }
            //connects.RemoveAll(m => m.OrderIds.Contains(orderId));
            //UpdateList(userId, true, null);
        }

        void UpdateList(string callerId, bool isConnect = true, int? orderId = null)
        {
            try
            {
                var index = connects.FindIndex(m => m.UserId == callerId);
                if (isConnect)
                {
                    if (index != -1 && connects[index].ConnectionId != Context.ConnectionId)
                    {
                        connects[index].ConnectionId = Context.ConnectionId;
                        var orders = _orderService.GetAllMyOrders(callerId).Where(m=>m.IsActive).Select(m => m.Id).ToList();
                        if (orders.Any())
                        {
                            foreach (var o in orders)
                            {
                                connects[index].OrderIds.Add(o);
                            }
                        }
                        //TODO 10.03.2020 Add update for manager, now it's only for clients
                        if (orderId != null)
                        {
                            connects[index].OrderIds.Add(orderId.Value);
                        }
                    }
                    else
                    {
                        var newConnect = new Connect {ConnectionId = Context.ConnectionId, UserId = callerId};
                        var orders = _orderService.GetAllMyOrders(callerId).Where(m => m.IsActive).Select(m => m.Id).ToList();
                        if (orders.Any())
                        {
                            foreach (var o in orders)
                            {
                                newConnect.OrderIds.Add(o);
                            }
                        }
                        if (orderId != null)
                        {
                            newConnect.OrderIds.Add(orderId.Value);
                        }

                        connects.Add(newConnect);
                    }
                }
                else
                {
                    if (index != -1 && connects[index].ConnectionId != Context.ConnectionId)
                    {
                        connects[index].ConnectionId = Context.ConnectionId;
                        if (orderId != null)
                        {
                            connects[index].OrderIds.Remove(orderId.Value);
                        }
                    }
                    else
                    {
                        var newConnect = new Connect { ConnectionId = Context.ConnectionId, UserId = callerId };
                        if (orderId != null)
                        {
                            newConnect.OrderIds = new List<int>();
                        }
                        connects.Add(new Connect());
                    }

                }
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task StartSession(CreateOrderVM vm, string userId = null)
        {
            try
            {
                if (userId == null)
                {
                    userId = Context.User.Claims.First(c => c.Type == "UserID").Value;
                }

                var order = await _orderService.CreateNewOrder(vm, userId);
                UpdateList(userId, true, order.Id);
                var orderers = connects.Where(m => m.OrderIds.Contains(order.Id));
                var tab = await _orderService.AssignMeToOrder(order.WelcomeCode.ToString(), userId);
                var page = _orderService.GetOrderByIdAsync(order.Id, userId);
                await Clients.Clients(orderers.Select(m => m.ConnectionId).ToList()).SendAsync("StartSession", page);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task CancelSession(int orderId, string userId = null)
        {
            try
            {
                if (userId == null)
                {
                    userId = Context.User.Claims.First(c => c.Type == "UserID").Value;
                }

                var order = await _orderService.GetOrderByIdAsync(orderId, userId);
                await _orderService.CancelOrder(orderId);
                var orderers = connects.Where(m => m.OrderIds.Contains(order.Id));
//                var tab = await _orderService.AssignMeToOrder(order.Id, order.WelcomeCode.ToString(), userId);
                var page = _orderService.GetOrderByIdAsync(order.Id, userId);
                await Clients.Clients(orderers.Select(m => m.ConnectionId).ToList()).SendAsync("StartSession", page);
                DisconnectByOrder(order.Id, userId);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task AssignClientToSession(string welcomeCode, string userId = null)
        {
            try
            {
                if (userId == null)
                {
                    userId = Context.User.Claims.First(c => c.Type == "UserID").Value;
                }
                var tab = await _orderService.AssignMeToOrder(welcomeCode, userId);
                UpdateList(userId, true, tab.Id);
                var orderers = connects.Where(m => m.OrderIds.Contains(tab.Id));
                await Clients.Clients(orderers.Select(m => m.ConnectionId).ToList()).SendAsync("AssignClient", tab);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task AssignManagerToSession(int orderId, string userId = null)
        {
            try
            {
                if (userId == null)
                {
                    userId = Context.User.Claims.First(c => c.Type == "UserID").Value;
                }
                UpdateList(userId, true, orderId);
                var orderers = connects.Where(m => m.OrderIds.Contains(orderId));
                var manager = _userService.GetUserTabVM(userId);
                var orderPage = await _orderService.AssignManagerToOrder(orderId, userId);
                await Clients.Clients(orderers.Select(m => m.ConnectionId).ToList()).SendAsync("AssignManager", orderPage);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task ExitClientFromSession(int orderId, string userId = null)
        {
            try
            {
                if (userId == null)
                {
                    userId = Context.User.Claims.First(c => c.Type == "UserID").Value;
                }
                UpdateList(userId, false, orderId);
                var orderers = connects.Where(m => m.OrderIds.Contains(orderId));
                var client = _userService.GetUserTabVM(userId);
                await Clients.Clients(orderers.Select(m => m.ConnectionId).ToList()).SendAsync("ExitClient", client);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task ExitManagerFromSession(int orderId, string userId = null)
        {
            try
            {
                if (userId == null)
                {
                    userId = Context.User.Claims.First(c => c.Type == "UserID").Value;
                }
                UpdateList(userId, false, orderId);
                var orderers = connects.Where(m => m.OrderIds.Contains(orderId));
                var manager = _userService.GetUserTabVM(userId);
                await Clients.Clients(orderers.Select(m => m.ConnectionId).ToList()).SendAsync("ExitManager", manager);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task CallManager(int orderId, string message, string userId = null)
        {
            try
            {
                if (userId == null)
                {
                    userId = Context.User.Claims.First(c => c.Type == "UserID").Value;
                }
                var call = await _orderService.CreateNewManagerCall(new ManagerCallVM {Comment = message, OrderId = orderId, CallTime = DateTime.Now, IsActive = true});
                var orderers = connects.Where(m => m.OrderIds.Contains(orderId));
                var manager = _userService.GetUserTabVM(userId);
                await Clients.Clients(orderers.Select(m => m.ConnectionId).ToList()).SendAsync("CallManager", call);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task AcceptCallManager(int callId)
        {
            try
            {
                var call = await _orderService.AcceptManagerCall(callId);
                var orderers = connects.Where(m => m.OrderIds.Contains(call.OrderId));
                await Clients.Clients(orderers.Select(m => m.ConnectionId).ToList()).SendAsync("AcceptCallManager", call);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task AddOrderItem(CreateOrderItemVM vm)
        {
            try
            {
                var item = await _orderService.CreateNewOrderItem(vm);
                var orderers = connects.Where(m => m.OrderIds.Contains(item.OrderId));
                await Clients.Clients(orderers.Select(m => m.ConnectionId).ToList()).SendAsync("AddOrderItem", item);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task RemoveOrderItem(int orderItemId)
        {
            try
            {
                var item = await _orderService.GetOrderItemByIdAsync(orderItemId);
                await _orderService.DeleteOrderItem(orderItemId);
                var orderers = connects.Where(m => m.OrderIds.Contains(item.OrderId));
                await Clients.Clients(orderers.Select(m => m.ConnectionId).ToList()).SendAsync("RemoveOrderItem", item);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task ChangeOrderItemStatus(int orderItemId, string status)
        {
            try
            {
                var item = await _orderService.GetOrderItemByIdAsync(orderItemId);
                item.Status = status;
                item = await _orderService.UpdateNewOrderItem(new CreateOrderItemVM(item));
                var orderers = connects.Where(m => m.OrderIds.Contains(item.OrderId));
                await Clients.Clients(orderers.Select(m => m.ConnectionId).ToList()).SendAsync("ChangeOrderItemStatus", item);
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public async Task UpdateOrderItem(CreateOrderItemVM vm)
        {
            try
            {
                var item = await _orderService.UpdateNewOrderItem(vm);
                var orderers = connects.Where(m => m.OrderIds.Contains(item.OrderId));
                await Clients.Clients(orderers.Select(m => m.ConnectionId).ToList()).SendAsync("UpdateOrderItem", item);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
