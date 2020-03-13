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
    [Route("api/menu")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class MenuController : ControllerBase
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        /// <summary>
        /// Get menu by menuId
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetMenu(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("MenuId is missing");
                }
                var menuPage = await _menuService.GetMenuPageAsync(id);
                return Ok(menuPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get menu by locationId
        /// </summary>
        [HttpGet]
        [Route("location/{id}")]
        public IActionResult GetLocationMenus(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("LocationId is missing");
                }

                var menuPage = _menuService.GetAllMenusByLocationId(id);
//                var menuPage = new MenuPageVM();
                return Ok(menuPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create menu
        /// </summary>
        [HttpPost] 
        public async Task<IActionResult> CreateMenu([FromForm] CreateMenuVM menu)
        {
            try
            {
                if (menu == null)
                {
                    throw new Exception("Menu object is missing");
                }
                var menuPage = await _menuService.CreateMenuAsync(menu);
                return Ok(menuPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update menu
        /// </summary>
        [HttpPut] 
        public async Task<IActionResult> UpdateMenu([FromForm] CreateMenuVM menu)
        {
            try
            {
                if (menu == null)
                {
                    throw new Exception("Menu object is missing");
                }
                var menuPage = await _menuService.UpdateMenuAsync(menu);
                return Ok(menuPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete menu
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteMenus(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("MenuId is missing");
                }

                var menuPage = _menuService.DeleteMenuAsync(id);
                return Ok(menuPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}