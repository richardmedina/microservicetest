using MicroserviceTest.Common.Events.Unknown;
using MicroserviceTest.Common.Handlers;

namespace MicroserviceTest.Api.Email.EventHandlers
{
    public class UnknownEventHandler : IEventHandler<UnknownEvent>
    {
        public async Task<bool> HandleAsync(UnknownEvent evt)
        {
            await Task.Yield();
            
            return true;
        }
    }
}
