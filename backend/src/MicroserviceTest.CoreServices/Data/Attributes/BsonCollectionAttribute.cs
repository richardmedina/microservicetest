using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.CoreServices.Data.Attributes
{
    public class BsonCollectionAttribute : Attribute
    {
        public string Collection { get; set; } = null!;
        public BsonCollectionAttribute(string Collection)
        {
            this.Collection = Collection;
        }
    }
}
