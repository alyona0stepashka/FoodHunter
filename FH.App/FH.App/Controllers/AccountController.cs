using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FH.BLL.Interfaces;
using FH.BLL.VMs;
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

        [HttpPost]
        [Route("register")]
        public async Task<object> Register([FromBody]RegisterVM model)
        {
            try
            {
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
    }
}