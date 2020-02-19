using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using FH.BLL.Infrastructure;
using FH.BLL.VMs;

namespace FH.BLL.Interfaces
{
    public interface IMenuService
    {
        Task<MenuPageVM> GetMenuPageAsync(int id);
        List<MenuPageVM> GetAllMenusByLocationId(int locationId);
        Task<MenuPageVM> CreateMenuAsync(CreateMenuVM menu);
        Task<MenuPageVM> UpdateMenuAsync(CreateMenuVM menu);
        Task DeleteMenuAsync(int id);
        Task<MenuItemPageVM> GetMenuItemPageAsync(int id);
        Task DeleteMenuItemAsync(int id);
        Task<MenuItemPageVM> CreateMenuItemAsync(CreateMenuItemVM menu);
        Task<MenuItemPageVM> UpdateMenuItemAsync(CreateMenuItemVM menu);
    }
}
