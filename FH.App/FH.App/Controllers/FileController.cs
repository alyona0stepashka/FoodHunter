using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FH.BLL.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FH.App.Controllers
{

    [Route("api/file")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FileController : ControllerBase
    {
        private readonly IFileService _fileService;
        private readonly ILocationService _locationService;

        public FileController(IFileService fileService,ILocationService locationService)
        {
            _fileService = fileService;
            _locationService = locationService;
        }

        /// <summary>
        /// Upload photo to location album by locationId
        /// </summary>
        [HttpPost]
        [Route("location/album/{id}")]
        public async Task<IActionResult> UploadLocationAlbumPhoto(/*[FromForm]*/ int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("LocationId is missing");
                }

                var locationPhoto =
                    await _fileService.CreateFileDbAsync(HttpContext.Request.Form.Files[0], locationId: id);
                return Ok(locationPhoto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        /// <summary>
        /// Delete photo from location album by photoId
        /// </summary>
        [HttpDelete]
        [Route("location/album/{id}")]
        public async Task<IActionResult> DeleteLocationAlbumPhoto(int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("FileId is missing");
                }

                await _fileService.DeleteFile(id);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("location/top/{id}")]
        public async Task<IActionResult> UploadLocationTopPhoto(/*[FromForm]*/ int id)
        {
            try
            {
                if (id == 0)
                {
                    throw new Exception("LocationId is missing");
                }

                var locationPhoto =
                    await _locationService.UploadLocationTopPhoto(HttpContext.Request.Form.Files[0], id);
                return Ok(locationPhoto);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}