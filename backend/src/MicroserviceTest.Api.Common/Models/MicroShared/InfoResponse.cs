using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.Api.Common.Models.MicroShared
{
    public class InfoResponse
    {
        public DateTime Timestamp { get; set; }
        public string ServerHostName { get; set; } = null!;
        public string ServerIP { get; set; } = null!;
        public string ClientHostName { get; set; } = null!;
        public string ClientIP { get; set; } = null!;

    }
}
