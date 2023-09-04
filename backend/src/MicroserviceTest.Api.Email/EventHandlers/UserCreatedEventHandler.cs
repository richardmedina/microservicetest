using MicroserviceTest.Common.Events.User;
using MicroserviceTest.Common.Handlers;

namespace MicroserviceTest.Api.Email.EventHandlers
{
    public class UserCreatedEventHandler : IEventHandler<UserCreatedEvent>
    {
        private readonly ILogger<UserCreatedEventHandler> _logger;

        public UserCreatedEventHandler(ILogger<UserCreatedEventHandler> logger)
        {
            _logger = logger;
        }

        public async Task<bool> HandleAsync(UserCreatedEvent evt)
        {
            await Task.CompletedTask;

            _logger.LogInformation("Sending Email: Handling event for UserCreated event");

            return true;
        }
    }
}
