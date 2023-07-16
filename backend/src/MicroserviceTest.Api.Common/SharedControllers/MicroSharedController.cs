using MicroserviceTest.Api.Common.Models.MicroShared;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MicroserviceTest.Api.SharedControllers.Controllers
{
    public abstract class MicroSharedController : ControllerBase
    {
        [HttpGet("healthcheck")]
        public async Task<IActionResult> HealthCheck()
        {
            await Task.CompletedTask;
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpGet("info")]
        public async Task<IActionResult> Info()
        {
            await Task.CompletedTask;

            return StatusCode(200, await GetClientAndServerInfoAsync(HttpContext));
        }

        [HttpGet("diagnostics/{statusCodeToReturn}")]
        public async Task<IActionResult> Test(int statusCodeToReturn = 200)
        {
            await Task.CompletedTask;
            return StatusCode(statusCodeToReturn);
        }

        public static async Task<InfoResponse> GetClientAndServerInfoAsync(HttpContext httpContext)
        {
            await Task.CompletedTask;

            var serverHostName = Dns.GetHostName();
            var clientHostname = httpContext.Connection.RemoteIpAddress;

            return new InfoResponse
            {
                Timestamp = DateTime.Now,
                ServerHostName = serverHostName,
                ServerIP = string.Join(",", Dns.GetHostAddresses(serverHostName).Select(ip => ip.ToString())),
                ClientHostName = clientHostname.ToString(),
                ClientIP = clientHostname.ToString(),
            };
        }
    }
}
