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
        private readonly IFileService _fileService;
        //private readonly
        public MenuService(IUnitOfWork uow, IFileService fileService)
        {
            _db = uow;
            _fileService = fileService;
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

        public async Task DeleteMenuItemAsync(int id)
        {
            await _db.MenuItems.DeleteAsync(id);
        }

        public async Task<MenuItemPageVM> GetMenuItemPageAsync(int id)
        {
            var menu = await _db.MenuItems.GetByIdAsync(id);
            if (menu == null)
            {
                throw new Exception("MenuItem not found");
            }
            var retMenu = new MenuItemPageVM(menu);
            return retMenu;
        }

        public async Task<MenuItemPageVM> CreateMenuItemAsync(CreateMenuItemVM menu)
        {
            var fileId = (await _fileService.CreateFileDbAsync(menu.Photo)).Id;
            var dbMenu = new MenuItem
            {
                Title = menu.Title,
                Info = menu.Info,
                Note = menu.Note,
                Price = menu.Price,
                PriceWithSales = menu.PriceWithSales,
                IsActive = menu.IsActive,
                MenuId = menu.MenuId,
                FileModelId = fileId
            };
            var menuNew = await _db.MenuItems.CreateAsync(dbMenu);
            var retMenu = new MenuItemPageVM(menuNew);
            return retMenu;
        }

        public async Task DeleteMenuAsync(int id)
        {
            await _db.Menus.DeleteAsync(id);
        }

        public async Task<MenuPageVM> UpdateMenuAsync(CreateMenuVM menu)
        {
            var item = await _db.Menus.GetByIdAsync(menu.Id);
            item.Title = menu.Title;
            item.Info = menu.Info;
            item.LocationId = menu.LocationId;
            item.Title = menu.Title;
            item.IconId = menu.IconId;
            var newItem = await _db.Menus.UpdateAsync(item);
            return new MenuPageVM(newItem);
        }

        public async Task<MenuItemPageVM> UpdateMenuItemAsync(CreateMenuItemVM menu)
        {
            var item = await _db.MenuItems.GetByIdAsync(menu.Id);
            item.Title = menu.Title;
            item.Info = menu.Info;
            item.Note = menu.Note;
            item.Price = menu.Price;
            item.PriceWithSales = menu.PriceWithSales;
            item.IsActive = menu.IsActive;
            item.MenuId = menu.MenuId;
            var newItem = await _db.MenuItems.UpdateAsync(item);
            return new MenuItemPageVM(newItem);
        }
    }
}
