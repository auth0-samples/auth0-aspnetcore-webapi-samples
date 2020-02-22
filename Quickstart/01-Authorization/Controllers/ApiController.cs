using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace WebAPIApplication.Controllers
{
    [Route("api")]
    [ApiController]
    public class ApiController : ControllerBase
    {
        [HttpGet("public")]
        public ActionResult Public()
        {
            return Ok(new
            {
                Message = "Hello from a public endpoint! You don't need to be authenticated to see this."
            });
        }

        [HttpGet("private")]
        [Authorize]
        public ActionResult Private()
        {
            return Ok(new
            {
                Message = "Hello from a private endpoint! You need to be authenticated to see this."
            });
        }

        [HttpGet("private-scoped")]
        [Authorize("read:messages")]
        public ActionResult Scoped()
        {
            return Ok(new
            {
                Message = "Hello from a private endpoint! You need to be authenticated and have a scope of read:messages to see this."
            });
        }

        // This is a helper action. It allows you to easily view all the claims of the token.
        [HttpGet("claims")]
        public ActionResult Claims()
        {
            return Ok(User.Claims.Select(c =>
                new
                {
                    c.Type,
                    c.Value
                }));
        }
    }
}
