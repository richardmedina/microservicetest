using MicroserviceTest.Common.Attributes;
using MicroserviceTest.Contract.Events;

namespace MicroserviceTest.Common.Events.Unknown
{
    [MessageConsumerTopic("unknowntopic")]
    public record UnknownEvent(string Message) : IEvent;
}
