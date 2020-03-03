using System;
using System.Collections.Generic;
using System.Text;
using FH.Models.Models;
using Microsoft.AspNetCore.Http;

namespace FH.BLL.VMs
{
    public class OrderItemVM
    {
        public int Id{ get; set; }
        public string Title { get; set; } 
        public double Count { get; set; } 
        public decimal PricePerItem { get; set; } 
        public string Status { get; set; } 
        public int OrderId { get; set; } 
        public int UserProfileId { get; set; }

        public OrderItemVM(OrderItem o)
        {
            Id = o.Id;
            Title = o.Title;
            Count = o.Count;
            PricePerItem = o.PricePerItem;
            Status = o.Status;
            OrderId = o.OrderId.Value;
            UserProfileId = UserProfileId;
        }
    }
}
