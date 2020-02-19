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
    }
}
