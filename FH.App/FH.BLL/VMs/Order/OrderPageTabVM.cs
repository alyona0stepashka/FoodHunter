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
        public UserTabVM User { get; set; }
        public List<OrderItemVM> OrderItems { get; set; }

        public OrderPageTabVM(OrderUser o)
        {
            User = new UserTabVM(o.UserProfile);
            OrderItems = o.Order.OrderItems.Where(m => m.UserId == User.UserProfileId).Select(m=>new OrderItemVM(m)).ToList();
        }
    }
}
