﻿using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FH.BLL.Infrastructure;
using FH.BLL.VMs;

namespace FH.BLL.Interfaces
{
    public interface IOrderService
    {
        Task<OrderPageVM> CreateNewOrder(CreateOrderVM vm, string userId);
        Task<OrderPageTabVM> AssignMeToOrder(string welcomeCode, string userId);
        Task CancelOrder(int orderId);
        Task<OrderPageVM> GetOrderByIdAsync(int orderId, string userId);
        List<OrderTabVM> GetAllMyOrders(string userId);
        List<OrderTabVM> GetAllMyManagerOrders(string userId);
        Task<OrderPageVM> AssignManagerToOrder(int orderId, string userId);
        Task<ManagerCallVM> CreateNewManagerCall(ManagerCallVM vm);
        Task<ManagerCallVM> AcceptManagerCall(int callId);
        Task<OrderItemVM> CreateNewOrderItem(CreateOrderItemVM vm);
        Task<OrderItemVM> UpdateNewOrderItem(CreateOrderItemVM vm);
        Task<OrderItemVM> GetOrderItemByIdAsync(int id); 
        OrderPageVM GetCurrentOrder(string userId);
        List<OrderTabVM> GetAllActiveLocationOrders(string userId);
        //List<OrderTabVM> GetAllNoManagersLocationOrders(string userId);
        List<OrderTabVM> GetAllLocationOrders(string userId);
        Task DeleteOrderItem(int id);
    }
}
