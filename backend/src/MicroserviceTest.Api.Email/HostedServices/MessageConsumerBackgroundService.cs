using MicroserviceTest.Common.Core.Messaging;

namespace MicroserviceTest.Api.Email.HostedServices
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
            //await Task.Yield();
            //await DoWork(cancellationToken);
            await Task.Factory.StartNew(() => DoWork(cancellationToken));

            _logger.LogInformation("StartAsync End");
        }

        private async Task DoWork(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                _logger.LogInformation("_messageConsumer.ConsumeAsync(cancellationToken)...");
                var message = await _messageConsumer.ConsumeAsync(cancellationToken);
                _logger.LogInformation("Message Received");
            }
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation("Stopping MessageConsumerBackgroundService");
            await base.StopAsync(cancellationToken);
        }
    }
}
