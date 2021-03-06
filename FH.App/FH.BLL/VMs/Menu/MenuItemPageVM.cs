﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FH.Models.EnumModels;
using FH.Models.Models;

namespace FH.BLL.VMs
{
    public class MenuItemPageVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Info { get; set; } 
        public string Note { get; set; }
        public string Currency { get; set; } = "BYN";
        public decimal Price { get; set; }
        public decimal? PriceWithSales { get; set; }
        public bool IsActive { get; set; }
        public int MenuId { get; set; }
        public string MenuTitle { get; set; }
        public IconVM Photo { get; set; }
        public int Rate { get; set; }
        public int FeedbacksCount { get; set; }
        //public List<FeedbackVM> Feedbacks { get; set; } = new List<FeedbackVM>();

        public MenuItemPageVM(MenuItem m, string currency)
        {
            Id = m.Id;
            Title = m.Title;
            Info = m.Info;
            Note = m.Note;
            Price = m.Price;
            if (m.Menu != null)
            { 
                if (m.Menu.Location != null) {Currency = m.Menu.Location.Currency;}
                else
                {
                    Currency = currency;
                }

                Rate = 0;
                PriceWithSales = m.PriceWithSales;
                IsActive = m.IsActive;
                if (m.MenuId != null) MenuId = m.MenuId.Value;
                MenuTitle = m.Menu?.Title;
            }

            Photo = new IconVM(m.Id, $"{m.FileModel?.Path}{m.FileModel?.Name}{m.FileModel?.Extension}");
            if (m.Feedbacks != null && m.Feedbacks.Any())
            {
                FeedbacksCount = m.Feedbacks.Count;
                //Feedbacks = m.Feedbacks.Select(e => new FeedbackVM(e)).ToList();
                Rate = m.Feedbacks.Sum(e => e.Stars) / FeedbacksCount;
            }
        }
    }
}
