﻿using System;
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
        public IconVM Photo { get; set; }
        public string Title { get; set; }
        public string Currency { get; set; }
        public double Count { get; set; } 
        public decimal PricePerItem { get; set; } 
        public string Status { get; set; } 
        public int OrderId { get; set; } 
        public int UserProfileId { get; set; }
        public int MenuItemId { get; set; }
        public int Rate { get; set; }
        public List<FeedbackVM> Feedbacks { get; set; } = new List<FeedbackVM>();

        public OrderItemVM(OrderItem o, string currency)
        {
            Rate = 0;
            Id = o.Id;
            Title = o.Title;
            Currency = o.Order.Location.Currency ?? currency;
            
            Count = o.Count;
            PricePerItem = o.PricePerItem;
            Status = o.Status;
            if (o.OrderId != null) {OrderId = o.OrderId.Value; }

            if (o.MenuItemId != null)
            {
                MenuItemId = o.MenuItemId.Value;
                if (o.UserId != null) UserProfileId = o.UserId.Value;
                if (o.MenuItemId != null)
                {
                    if (o.MenuItem != null)
                        Photo = new IconVM(o.MenuItemId.Value,
                            $"{o.MenuItem.FileModel?.Path}{o.MenuItem.FileModel?.Name}{o.MenuItem.FileModel?.Extension}");
                }
            }

            if (o.MenuItem != null && (o.MenuItem.Feedbacks != null && o.MenuItem.Feedbacks.Any()))
            {
                Feedbacks = o.MenuItem.Feedbacks.Select(e => new FeedbackVM(e, e.UserProfile, e.Photos)).ToList();
                Rate = o.MenuItem.Feedbacks.Sum(e => e.Stars) / o.MenuItem.Feedbacks.Count;
            }
        }
    }
}
