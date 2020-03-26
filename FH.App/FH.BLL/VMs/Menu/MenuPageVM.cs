using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FH.Models.EnumModels;
using FH.Models.Models;

namespace FH.BLL.VMs
{
    public class MenuPageVM
    {
        public int Id { get; set; }
        public int LocationId { get; set; }
        public string LocationName { get; set; }
        public string Title { get; set; }
        public string Info { get; set; }
        public IconVM Icon { get; set; }
        public List<MenuItemPageVM> MenuItems { get; set; } = new List<MenuItemPageVM>();

        public MenuPageVM(Menu m)
        {
            Id = m.Id;
            if (m.LocationId != null) LocationId = m.LocationId.Value;
            Title = m.Title;
            Info = m.Info;
            if (m.Icon != null) Icon = new IconVM(m.Icon);
            LocationName = m.Location?.Name;
            var currency = "";
            if (m.Location != null)
            {
                currency = m.Location.Currency;
            }

            if (m.MenuItems != null && m.MenuItems.Any())
            {
                MenuItems = m.MenuItems?.Select(e => new MenuItemPageVM(e, currency)).ToList();
            }
        }

        public MenuPageVM()
        {
            
        }
    }
    
}
