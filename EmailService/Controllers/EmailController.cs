using System.Net;
using System.Net.Mail;
using EmailService.Service;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace EmailService.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmailController : ControllerBase
    {
        private readonly IEmailServe _emailServe;
        private readonly IConsumerService _consumerService;
        

        public EmailController(IEmailServe emailServe, IConsumerService consumerService)
        {
            _emailServe = emailServe;
            _consumerService = consumerService;
        }

        [HttpPost]
        public async Task<IActionResult> Post()
        {
            string message = _consumerService.ConsumeMessage();
            return  message != null ? Ok("message") : BadRequest("Unsuccessful");
        }

    }
}
