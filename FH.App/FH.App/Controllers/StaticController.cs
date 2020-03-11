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
    [Route("api/static")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class StaticController : ControllerBase
    {
        private readonly IStaticService _staticService;

        public StaticController(IStaticService staticService)
        {
            _staticService = staticService;
        }

        [HttpPost]
        [Route("search")]
        public IActionResult GetSearchResult(SearchQueryVM search)
        {
            try
            {
                if (search == null)
                {
                    throw new Exception("search is missing");
                }
                var items = _staticService.GetSearchResult(search);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("client")]
        public IActionResult GetChartDataClient()
        {
            try
            {
                var items = _staticService.GetChartDataClient(User.Claims.First(c => c.Type == "UserID").Value);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("manager")]
        public IActionResult GetChartDataManager()
        {
            try
            {
                var items = _staticService.GetChartDataManager(User.Claims.First(c => c.Type == "UserID").Value);
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("sex")]
        public IActionResult GetSexes()
        {
            try
            {
                var items = _staticService.GetSexes();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("specification")]
        public IActionResult GetSpecifications()
        {
            try
            {
                var items = _staticService.GetSpecifications();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("icon")]
        public IActionResult GetIcons()
        {
            try
            {
                var items = _staticService.GetIcons();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}