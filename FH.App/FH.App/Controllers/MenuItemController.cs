using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FH.BLL.Interfaces;
using FH.BLL.VMs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FH.App.Controllers
{
    [Route("api/menu/item")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MenuItemController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuItemController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        /// <summary>
        /// Get menuItem by menuItemId
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetMenuItemPage(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("MenuItemId is missing");
                }
                var menuItemPage = await _menuService.GetMenuItemPageAsync(id);
                return Ok(menuItemPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create menu item
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateMenuItem([FromForm] CreateMenuItemVM menu)
        {
            try
            {
                if (menu == null)
                {
                    throw new Exception("MenuItem object is missing");
                }

                menu.Photo = HttpContext.Request.Form.Files[0];
                var menuItemPage = await _menuService.CreateMenuItemAsync(menu);
                return Ok(menuItemPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update menu item
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdateMenuItem([FromForm] CreateMenuItemVM menu)
        {
            try
            {
                if (menu == null)
                {
                    throw new Exception("Menu object is missing");
                }
                var menuItemPage = await _menuService.UpdateMenuItemAsync(menu);
                return Ok(menuItemPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete menu item
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteMenuItem(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("MenuItemId is missing");
                }
                await _menuService.DeleteMenuItemAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}