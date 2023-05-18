using MicroserviceTest.Api.Email.Models.Email;
using MicroserviceTest.Common.Services;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceTest.Api.Email.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService emailService;
        public EmailController(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        [HttpPost("send")]
        public async Task<IActionResult> SendEmail(EmailSendRequest emailSendRequest)
        {
            var result = await emailService.SendEmailAsync(
                emailSendRequest.From,
                emailSendRequest.To,
                emailSendRequest.Subject,
                emailSendRequest.Content
                );


            return result
                ? StatusCode(StatusCodes.Status200OK)
                : StatusCode(StatusCodes.Status500InternalServerError);
        }
    }
}
