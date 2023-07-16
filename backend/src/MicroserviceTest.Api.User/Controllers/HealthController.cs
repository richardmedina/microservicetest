using Microsoft.AspNetCore.Mvc;

namespace MicroserviceTest.Api.User.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class HealthController : ControllerBase
    {
        [HttpGet]
        public async Task<IActionResult> Check()
        {
            await Task.CompletedTask;
            return StatusCode(StatusCodes.Status200OK);
        }

        [HttpGet("test/{statusCodeToReturn}")]
        public async Task<IActionResult> Test(int statusCodeToReturn = 200)
        {
            await Task.CompletedTask;
            return StatusCode(statusCodeToReturn);
        }
    }
}
