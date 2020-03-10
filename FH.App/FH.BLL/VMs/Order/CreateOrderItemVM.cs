using System;
using System.Collections.Generic;
using System.Text;
using FH.Models.Models;
using Microsoft.AspNetCore.Http;

namespace FH.BLL.VMs
{
    public class CreateOrderItemVM
    {
        public int Id{ get; set; } 
        public double Count { get; set; }
        public decimal PricePerItem { get; set; }
        public string Status { get; set; } = "In progress";
        public string Title { get; set; }
        public int OrderId { get; set; } 
        public int UserProfileId { get; set; }
        public int MenuItemId { get; set; }

        public CreateOrderItemVM(OrderItemVM o)
        { 
            Count = o.Count; 
            Status = o.Status; 
        }

        public CreateOrderItemVM()
        {
            
        }
    }
}
