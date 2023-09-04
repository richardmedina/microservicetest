using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.Common.Attributes
{
    [AttributeUsage(AttributeTargets.Class)]
    public class MessageConsumerTopicAttribute : Attribute
    {
        public string Topic { get; set; } = null!;

        public MessageConsumerTopicAttribute(string topic)
        {
            Topic = topic;
        }
    }
}
