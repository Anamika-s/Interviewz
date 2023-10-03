using InterviewService.Models;
using InterviewService.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace InterviewService.Controllers
{
    [Route("api/feedback")]
    [ApiController]
    public class FeedbackController : ControllerBase
    {
        private readonly IFeedbackService _feedbackService;

        public FeedbackController(IFeedbackService feedbackService)
        {
            _feedbackService = feedbackService;
        }

        [HttpGet]
        public IActionResult GetAllFeedback()
        {
            var feedbackList = _feedbackService.GetAllFeedback();
            return Ok(feedbackList);
        }

        [HttpPost]
        public IActionResult CreateFeedback([FromBody] Feedback feedback)
        {
            if (feedback == null)
            {
                return BadRequest();
            }

            _feedbackService.CreateFeedback(feedback);
            return CreatedAtAction(nameof(GetAllFeedback), feedback);
        }
    }
}
