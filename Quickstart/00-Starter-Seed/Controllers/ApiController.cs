using Microsoft.AspNetCore.Mvc;

namespace WebAPIApplication.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        [HttpGet]
        [Route("public")]
        public IActionResult Public()
        {
            return Ok(new
            {
                Message = "Hello from a public endpoint! You don't need to be authenticated to see this."
            });
        }
    }
}