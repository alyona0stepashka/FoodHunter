using System;
using System.Collections.Generic;
using System.Text;
using FH.Models.EnumModels;
using System.Linq;
using FH.Models.Models;
using Microsoft.AspNetCore.Http;

namespace FH.BLL.VMs
{
    public class OrderItemVM
    {
        public int Id{ get; set; }
        public Icon Photo { get; set; }
        public string Title { get; set; } 
        public double Count { get; set; } 
        public decimal PricePerItem { get; set; } 
        public string Status { get; set; } 
        public int OrderId { get; set; } 
        public int UserProfileId { get; set; }
        public int Rate { get; set; }
        public List<FeedbackVM> Feedbacks { get; set; } = new List<FeedbackVM>();

        public OrderItemVM(OrderItem o)
        {
            Rate = 0;
            Id = o.Id;
            Title = o.Title;
            Count = o.Count;
            PricePerItem = o.PricePerItem;
            Status = o.Status;
            OrderId = o.OrderId.Value;
            UserProfileId = UserProfileId;
            Photo = new Icon(o.MenuItemId.Value, $"{o.MenuItem.FileModel?.Path}{o.MenuItem.FileModel?.Name}{o.MenuItem.FileModel?.Extension}");
            if (o.MenuItem.Feedbacks != null && o.MenuItem.Feedbacks.Any())
            {
                Feedbacks = o.MenuItem.Feedbacks.Select(e => new FeedbackVM(e)).ToList();
                Rate = o.MenuItem.Feedbacks.Sum(e => e.Stars) / o.MenuItem.Feedbacks.Count;
            }
        }
    }
}
