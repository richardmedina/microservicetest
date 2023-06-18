using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.Contract.Core.Messaging
{
    public class MessageConsumerOptions
    {
        public IEnumerable<string> Topics = null!;
    }
}
