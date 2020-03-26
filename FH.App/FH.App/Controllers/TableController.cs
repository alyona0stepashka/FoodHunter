using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FH.BLL.Interfaces;
using FH.BLL.VMs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FH.App.Controllers
{
    [Route("api/table")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class TableController : ControllerBase
    {
        private readonly ITableService _tableService;
        private readonly UserManager<IdentityUser> _userManager;

        public TableController(ITableService tableService, UserManager<IdentityUser> userManager)
        {
            _tableService = tableService;
            _userManager = userManager;
        }

        /// <summary>
        /// Get table-booking history for current user (as client)
        /// </summary>
        [HttpGet]
        [Route("my")]
        public IActionResult GetMyTableBooks()
        {
            try
            {
                var companyPage = _tableService.GetMyTableBooks(User.Claims.First(c => c.Type == "UserID").Value);
                companyPage.Reverse();
                return Ok(companyPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get all table-booking by locationId
        /// </summary>
        [HttpGet]
        [Route("{id}/booking")]
        public IActionResult GetAllTableBooksByLocation(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("locationId is missing");
                }
                var companyPage = _tableService.GetAllTableBooksByLocation(id);
                companyPage.Reverse();
                return Ok(companyPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Accept table-book by bookId
        /// </summary>
        [HttpGet]
        [Route("{id}/booking/accept")]
        public IActionResult AcceptTableBook(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("bookId is missing");
                }
                var companyPage = _tableService.AcceptTableBookAsync(id);
                return Ok(companyPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Decline table-book by bookId
        /// </summary>
        [HttpGet]
        [Route("{id}/booking/decline")]
        public IActionResult DeclineTableBook(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("bookId is missing");
                }
                var companyPage = _tableService.DeclineTableBookAsync(id);
                return Ok(companyPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Cancel table-book by bookId (as client)
        /// </summary>
        [HttpGet]
        [Route("{id}/booking/cancel")]
        public IActionResult CancelTableBook(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("bookId is missing");
                }
                var companyPage = _tableService.CancelTableBookAsync(id);
                return Ok(companyPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get all tables by locationId
        /// </summary>
        [HttpGet]
        [Route("{id}")]
        public IActionResult GetAllTablesByLocation(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("locationId is missing");
                }
                var companyPage = _tableService.GetAllTablesByLocation(id);
                companyPage.Reverse();
                return Ok(companyPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create table-book
        /// </summary>
        [HttpPost]
        [Route("booking")]
        public async Task<IActionResult> CreateTableBook([FromForm] CreateTableBookVM table)
        {
            try
            {
                if (table == null)
                {
                    throw new Exception("table object is missing");
                }
                var menuPage = await _tableService.CreateTableBookAsync(table);
                return Ok(menuPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create table
        /// </summary>
        [HttpPost] 
        public async Task<IActionResult> CreateTable([FromForm] CreateTableVM table)
        {
            try
            {
                if (table == null)
                {
                    throw new Exception("table object is missing");
                }
                var menuPage = await _tableService.CreateTableAsync(table);
                return Ok(menuPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Update table
        /// </summary>
        [HttpPut]
        public async Task<IActionResult> UpdateTable([FromForm] CreateTableVM table)
        {
            try
            {
                if (table == null)
                {
                    throw new Exception("Menu object is missing");
                }
                var tablePage = await _tableService.UpdateTableAsync(table);
                return Ok(tablePage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete table
        /// </summary>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult DeleteTables(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("MenuId is missing");
                }

                var menuPage = _tableService.DeleteTableAsync(id);
                return Ok(menuPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}