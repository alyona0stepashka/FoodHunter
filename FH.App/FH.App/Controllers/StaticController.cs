using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FH.BLL.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FH.App.Controllers
{
    [Route("api/static")]
    [ApiController]
    public class StaticController : ControllerBase
    {
        private readonly IStaticService _staticService;

        public StaticController(IStaticService staticService)
        {
            _staticService = staticService;
        }

        [HttpGet]
        [Route("sex")]
        public IActionResult GetSexes()
        {
            try
            {
                var sexes = _staticService.GetSexes();
                return Ok(sexes);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}