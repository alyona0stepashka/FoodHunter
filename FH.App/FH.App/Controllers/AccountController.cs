﻿using System;
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
    [Route("api/account")]
    [ApiController]
    public class AccountController : ControllerBase
    {

        private readonly IAccountService _accountService;
        //private readonly IUserService _userService;
        public AccountController(IAccountService accountService/*, IUserService userService*/)
        {
            _accountService = accountService;
            // _userService = userService;
        }

        /// <summary>
        /// Register new user
        /// </summary>
        [HttpPost]
        [Route("register")]
        public async Task<object> Register([FromForm] RegisterVM model)
        {
            try
            {
                model.Photo = HttpContext.Request.Form.Files[0];
                if (await _accountService.RegisterUserAsync(model, HttpContext.Request.Host.ToString()) == null)
                {
                    throw new Exception("Register fail");
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Login user
        /// </summary>
        [HttpPost]
        [Route("login")]
        public async Task<IActionResult> Login([FromBody] LoginVM model)
        {
            try
            {
                var token = await _accountService.LoginUserAsync(model);
                if (token == null)
                {
                    throw new Exception("Username or password is incorrect or not confirmed email.");
                }

                return Ok(new {token});
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get staff list by location
        /// </summary>
        [HttpGet]
        [Route("staff")]
        [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
        public IActionResult GetStaffList() 
        {
            try
            { 
                var staff = _accountService.GetLocationStaff(User.Claims.First(c => c.Type == "UserID").Value);
                staff.Reverse();
                return Ok(staff);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Email confirmation
        /// </summary>
        [HttpGet]
        [Route("email")]
        public async Task<IActionResult> ConfirmEmail(string user_id, string code)  //user_id
        {
            try
            {
                if (string.IsNullOrWhiteSpace(user_id) || string.IsNullOrWhiteSpace(code))
                {
                    throw new Exception("UserId and Code are required");
                }
                await _accountService.ConfirmEmailAsync(user_id, code);
                return Redirect("http://localhost:4200/auth/login");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Edit account info
        /// </summary>
        [HttpPut]
        [Authorize]
        public async Task<IActionResult> EditMyAccountInformation([FromBody] EditAccountInfoVM editUser)
        {
            try
            {
                if (editUser == null)
                {
                    throw new Exception("editUser param is null");
                } 
                editUser.Id = User.Claims.First(c => c.Type == "UserID").Value;
                await _accountService.EditAccountInfo(editUser); 
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Delete user
        /// </summary>
        [HttpDelete]
        [Route("delete")]
        public IActionResult DeleteUser(string userId)
        {
            try
            {
                _accountService.DeleteUser(userId);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}