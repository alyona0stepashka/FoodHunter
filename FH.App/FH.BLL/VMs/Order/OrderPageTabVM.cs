using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FH.Models.Models;
using FH.Models.StaticModels;
using Microsoft.AspNetCore.Http;

namespace FH.BLL.VMs
{
    public class OrderPageTabVM
    {
        public int Id { get; set; }
        public UserTabVM User { get; set; }
        public List<OrderItemVM> OrderItems { get; set; }

        public OrderPageTabVM(OrderUser o, string currency)
        {
            Id = o.OrderId;
            if (o.UserProfile != null) User = new UserTabVM(o.UserProfile, o.UserProfile.Sex, o.UserProfile.File);
            OrderItems = o.Order.OrderItems.Where(m => m.UserId == User.UserProfileId).Select(m=>new OrderItemVM(m, currency)).ToList(); 
        }
    }
}
