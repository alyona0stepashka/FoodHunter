using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FH.BLL.Interfaces;
using FH.BLL.VMs;
using FH.DAL.EF.Interfaces;
using FH.Models.Models;

namespace FH.BLL.Services
{
    public class MenuService : IMenuService
    {
        private IUnitOfWork _db { get; set; }
        //private readonly 
        public MenuService(IUnitOfWork uow)
        {
            _db = uow;
        }

        public List<MenuPageVM> GetAllMenusByLocationId(int locationId)
        {
            var menus = _db.Menus.GetAll().Where(m=>m.LocationId==locationId).ToList();
            if (menus == null)
            {
                throw new Exception("Menu not found");
            }

            var retMenus = menus.Select(m =>new MenuPageVM(m)).ToList();
            return retMenus; 
        }

        public async Task<MenuPageVM> GetMenuPageAsync(int id)
        {
            var menu = await _db.Menus.GetByIdAsync(id);
            if (menu == null)
            {
                throw new Exception("Menu not found");
            }
            var retMenu = new MenuPageVM(menu);
            return retMenu;
        }

        public async Task<MenuPageVM> CreateMenuAsync(CreateMenuVM menu)
        {
            var dbMenu = new Menu
            {
                Title = menu.Title,
                Info = menu.Info,
                LocationId = menu.LocationId,
                IconId = menu.IconId
            };
            var menuNew = await _db.Menus.CreateAsync(dbMenu);
            var retMenu = new MenuPageVM(menuNew);
            return retMenu;
        }
    }
}
