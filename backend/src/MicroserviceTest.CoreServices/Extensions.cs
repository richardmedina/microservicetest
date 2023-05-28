using MicroserviceTest.Common.Core.Messaging;
using MicroserviceTest.Common.Handlers;
using MicroserviceTest.Contract.Events;
using MicroserviceTest.CoreServices.Messaging;
using MicroserviceTest.CoreServices.Messaging.BackgroundServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.CoreServices
{
    public static class Extensions
    {
        public static void AddEventBusEvent<TEvent>(this IServiceCollection services) where TEvent : IEvent
        {
            services.AddScoped<IEventBus, EventBus>();
            //services.AddHostedService<KafkaConsumerBackgroundService<TEvent>>();
        }
    }
}
