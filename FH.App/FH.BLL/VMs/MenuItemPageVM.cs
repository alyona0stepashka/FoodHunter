using System;
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
        public decimal Price { get; set; }
        public int Rate { get; set; }
        public decimal? PriceWithSales { get; set; }
        public bool IsActive { get; set; }
        public int MenuId { get; set; }
        public string MenuTitle { get; set; }
        public Icon Photo { get; set; }
        public List<FeedbackVM> Feedbacks { get; set; } = new List<FeedbackVM>();

        public MenuItemPageVM(MenuItem m)
        {
            Id = m.Id;
            Title = m.Title;
            Info = m.Info;
            Note = m.Note;
            Price = m.Price;
            Rate = 0;
            PriceWithSales = m.PriceWithSales;
            IsActive = m.IsActive;
            MenuId = m.MenuId;
            MenuTitle = m.Menu?.Title;
            Photo = new Icon(m.Id, $"{m.FileModel?.Path}{m.FileModel?.Name}{m.FileModel?.Extension}");
            if (m.Feedbacks != null && m.Feedbacks.Any())
            {
                Feedbacks = m.Feedbacks.Select(e => new FeedbackVM(e)).ToList();
                Rate = m.Feedbacks.Sum(e => e.Stars) / m.Feedbacks.Count;
            }
        }
    }
}
