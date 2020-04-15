using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Service.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController: ControllerBase 
    {
        [HttpGet]
        public async Task<IActionResult> Index() {
            return Ok("Auth.Service working");
        }
    }
}