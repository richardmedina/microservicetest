using MicroserviceTest.Common.Core.Messaging;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MicroserviceTest.CoreServices.Messaging.HostedServices
{
    public class MessageConsumerBackgroundService : BackgroundService
    {
        private readonly ILogger<MessageConsumerBackgroundService> _logger;
        private readonly IMessageConsumerCoreService _messageConsumer;
        public MessageConsumerBackgroundService(IMessageConsumerCoreService messageConsumer, ILogger<MessageConsumerBackgroundService> logger)
        {
            _messageConsumer = messageConsumer;
            _logger = logger;
        }
        protected override async Task ExecuteAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("StartAsync Start");
            await Task.Yield();
            await DoWork(cancellationToken);

            _logger.LogInformation("StartAsync End");
        }

        private async Task DoWork(CancellationToken cancellationToken)
        {
            _logger.LogInformation("_messageConsumer.StartConsuming(cancellationToken)...");
            await _messageConsumer.StartConsumingAsync(cancellationToken);
            _logger.LogInformation("Closing Consumer...");
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping MessageConsumerBackgroundService");
            await base.StopAsync(cancellationToken);
        }
    }
}
