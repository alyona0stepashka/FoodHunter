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
        public Icon Icon { get; set; }
        public List<MenuItemPageVM> MenuItems { get; set; } = new List<MenuItemPageVM>();

        public MenuPageVM(Menu m)
        {
            Id = m.Id;
            LocationId = m.LocationId;
            Title = m.Title;
            Info = m.Info;
            Icon = m.Icon;
            LocationName = m.Location?.Name;
            if (m.MenuItems != null && m.MenuItems.Any())
            {
                MenuItems = m.MenuItems?.Select(e => new MenuItemPageVM(e)).ToList();
            }
        }
    }
}
