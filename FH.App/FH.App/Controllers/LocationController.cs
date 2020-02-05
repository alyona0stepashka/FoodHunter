using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FH.BLL.Interfaces;
using FH.BLL.VMs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FH.App.Controllers
{
    [Route("api/locations")]
    [ApiController]
    public class LocationController : ControllerBase
    {
        private readonly ILocationService _locationService;

        public LocationController(ILocationService locationService)
        {
            _locationService = locationService;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetLocation(int id) 
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("LocationId is missing");
                }
                var locationPage = await _locationService.GetLocationPageAsync(id);
                return Ok(locationPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("{lon}x{lat}")]
        public IActionResult GetLocationsNearPoint(decimal lon, decimal lat)
        {
            try
            {
                if (lon == 0 || lat==0)
                {
                    throw new Exception("Point is missing");
                }
                var locations = _locationService.GetLocationsNearPoint(lon, lat);
                return Ok(locations);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("Company/{CompanyId}")]
        public IActionResult GetAllLocationByCompany(int CompanyId)
        {
            try
            {
                if (CompanyId == 0)
                {
                    throw new Exception("CompanyId is missing");
                }
                var locationPage = _locationService.GetLocationsByCompany(CompanyId);
                return Ok(locationPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateLocation(CreateLocationVM location)
        {
            try
            {
                if (location == null)
                {
                    throw new Exception("Location is missing");
                }
                var locationPage = await _locationService.CreateLocationAsync(location, User.Claims.First(c => c.Type == "UserID").Value);
                return Ok(locationPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteLocation(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("LocationId is missing");
                }
                await _locationService.DeleteLocationAsync(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}