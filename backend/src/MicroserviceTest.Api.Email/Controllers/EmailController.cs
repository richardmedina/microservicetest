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
        private readonly IMessageProducerCoreService _messageProducer;
        public EmailController(IEmailService emailService, IMessageProducerCoreService messageProducer)
        {
            this.emailService = emailService;
            _messageProducer = messageProducer;
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
        public void KafkaPublish(string userName, string firstName, string lastName)
        {
            var userCreatedEvent = new UserCreatedEvent(
                userName,
                firstName,
                lastName
            );

            _messageProducer.PublishAsync("usercreated", userCreatedEvent);
        }
    }
}
