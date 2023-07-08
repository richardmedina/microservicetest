using MicroserviceTest.Common.Services;
using MicroserviceTest.Services.Email;
using MicroserviceTest.Services.User;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.Services
{
    public static class Extensions
    {
        public static void RegisterBusinessServices(this IServiceCollection services)
        {
            services.AddScoped<IEmailService, EmailService>();
            services.AddScoped<IUserService, UserService>();
        }
    }
}
