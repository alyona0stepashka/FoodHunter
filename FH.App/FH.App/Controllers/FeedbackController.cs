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
    [Route("api/feedback")]
    [ApiController]
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService; 

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        /// <summary>
        /// Get all feedbacks by locationId
        /// </summary>
        [HttpGet]
        [Route("location/{locationId}")]
        public IActionResult GetAllFeedbacksByLocation(int locationId)
        {
            try
            {
                if (locationId == 0)
                {
                    throw new Exception("locationId is missing");
                }

                var locationFeedbacks = _feedbackService.GetFeedbacksByLocation(locationId);
                locationFeedbacks.Reverse();
                return Ok(locationFeedbacks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Get all feedbacks by menuItemId
        /// </summary>
        [HttpGet]
        [Route("item/{itemId}")]
        public IActionResult GetAllFeedbacksByMenuItem(int itemId)
        {
            try
            {
                if (itemId == 0)
                {
                    throw new Exception("itemId is missing");
                } 
                var itemFeedbacks = _feedbackService.GetFeedbacksByMenuItem(itemId);
                itemFeedbacks.Reverse();
                return Ok(itemFeedbacks);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Create new feedback
        /// </summary>
        [HttpPost]
        public async Task<IActionResult> CreateFeedback([FromForm] CreateFeedbackVM feedback)
        {
            try
            {
                if (feedback == null)
                {
                    throw new Exception("Feedback is missing");
                } 
                feedback.UploadPhoto = HttpContext.Request.Form.Files[0];
                var feedbackPage = await _feedbackService.CreateFeedbackAsync(feedback);
                return Ok(feedbackPage);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}