using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
    [Route("api/company")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class CompanyController : ControllerBase
    {
        private readonly ICompanyService _companyService;
        private readonly UserManager<IdentityUser> _userManager;

        public CompanyController(ICompanyService companyService, UserManager<IdentityUser> userManager)
        {
            _companyService = companyService;
            _userManager = userManager;
        }

        [HttpGet]
        public IActionResult GetAllCompanies()
        {
            try
            {
                var companies =  _companyService.GetAllCompanies();
                return Ok(companies);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetCompany(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("companyId is missing");
                }
                var companyPage = await _companyService.GetCompanyPageAsync(id);
                return Ok(companyPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        //[AllowAnonymous]
        // [IgnoreAntiforgeryToken]
        public async Task<IActionResult> CreateCompany([FromForm] CreateCompanyVM company)
        {
            try
            {
                if (company == null)
                {
                    throw new Exception("company is missing");
                }

                company.LogoFile = HttpContext.Request.Form.Files[0]; 
                var companyPage =
                    await _companyService.CreateCompanyAsync(company, User.Claims.First(c => c.Type == "UserID").Value);
                return Ok(companyPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteCompany(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("companyId is missing");
                }
                await _companyService.DeleteCompanyAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}