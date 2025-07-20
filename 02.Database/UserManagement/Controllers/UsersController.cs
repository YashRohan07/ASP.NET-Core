using Microsoft.AspNetCore.Mvc;

namespace UserManagement.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UsersController : ControllerBase
    {
        [HttpGet("test")]
        public IActionResult Test()
        {
            return Ok("API is working!");
        }
    }
}
