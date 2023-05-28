using MicroserviceTest.Api.Email.Models.Email;
using MicroserviceTest.Common.Core.Messaging;
using MicroserviceTest.Common.Services;
using MicroserviceTest.Contract.Events;
using Microsoft.AspNetCore.Mvc;

namespace MicroserviceTest.Api.Email.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmailController : ControllerBase
    {
        private readonly IEmailService emailService;
        private readonly IEventBus _eventBus;
        public EmailController(IEmailService emailService, IEventBus eventBus)
        {
            this.emailService = emailService;
            this._eventBus = eventBus;
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

        [HttpPost("kafkapublish")]
        public void KafkaPublish()
        {
            var userCreatedEvent = new UserCreatedEvent(
                "Ricardo",
                "Medina",
                "richard"
            );

            _eventBus.Publish(userCreatedEvent);
        }
    }
}
