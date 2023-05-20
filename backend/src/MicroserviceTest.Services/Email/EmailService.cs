using MicroserviceTest.Common.Services;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.Services.Email
{
    public class EmailService : IEmailService
    {
        private readonly ILogger<EmailService> logger;
        public EmailService(ILogger<EmailService> logger)
        {
            this.logger = logger;
        }
        public async Task<bool> SendEmailAsync(string from, string to, string subject, string content)
        {
            await Task.CompletedTask;
            logger.LogInformation($"Sending Email [from: {from}, to: ${to}, subject: {subject}, content={content}]...");

            return true;
        }
    }
}
