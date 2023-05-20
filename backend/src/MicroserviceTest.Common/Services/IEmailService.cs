using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.Common.Services
{
    public interface IEmailService
    {
        public Task<bool> SendEmailAsync(string from, string to, string subject, string content);
    }
}
