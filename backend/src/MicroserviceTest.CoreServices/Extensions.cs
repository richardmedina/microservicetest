using MicroserviceTest.Common.Core.Messaging;
using MicroserviceTest.Contract.Core.Messaging;
using MicroserviceTest.CoreServices.Messaging;
using Microsoft.Extensions.DependencyInjection;

namespace MicroserviceTest.CoreServices
{
    public static class Extensions
    {
        public static void AddMessageProducer(this IServiceCollection services)
        {
            services.AddScoped<IMessageProducerCoreService, MessageProducerCoreService>();
        }

        public static void AddMessageConsumer(this IServiceCollection services, Action<MessageConsumerOptions> options)
        {
            services.Configure(options);
            services.AddSingleton<IMessageConsumerCoreService, MessageConsumerCoreService>();
        }
    }
}
