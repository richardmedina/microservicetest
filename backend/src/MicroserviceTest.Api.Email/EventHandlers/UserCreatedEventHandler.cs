using MicroserviceTest.Common.Handlers;
using MicroserviceTest.Contract.Events;

namespace MicroserviceTest.Api.Email.EventHandlers
{
    public class UserCreatedEventHandler : IEventHandler<UserCreatedEvent>
    {
        private readonly ILogger<UserCreatedEventHandler> _logger;

        public UserCreatedEventHandler(ILogger<UserCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task HandleAsync(UserCreatedEvent evt)
        {
            await Task.CompletedTask;

            _logger.LogInformation("Sending Email: Handling event for UserCreated event");
        }
    }
}
