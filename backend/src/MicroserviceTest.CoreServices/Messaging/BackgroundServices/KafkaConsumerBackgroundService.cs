using MicroserviceTest.Common.Core.Messaging;
using MicroserviceTest.Contract.Events;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.CoreServices.Messaging.BackgroundServices
{
    public class KafkaConsumerBackgroundService<TEvent> 
        : BackgroundService where TEvent : IEvent
    {
        private readonly ILogger<KafkaConsumerBackgroundService<TEvent>> _logger;
        private readonly IServiceProvider _serviceProvider;
       // private readonly IEventBus _eventBus;

        public KafkaConsumerBackgroundService(
            ILogger<KafkaConsumerBackgroundService<TEvent>> logger,
            IServiceProvider serviceProvider)
        {
            _logger = logger;
            _serviceProvider = serviceProvider;
        //    _eventBus = eventBus;
            _logger.LogInformation("Creating Background Service");
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            await Task.CompletedTask;

            using(IServiceScope scope = _serviceProvider.CreateScope())
            {
                _logger.LogInformation("ServiceProvider Scope was created");
                var eventBus = scope.ServiceProvider.GetService<IEventBus>();
                eventBus.Consume<TEvent>(stoppingToken);
            }

        }
    }
}
