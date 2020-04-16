using System;
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

        [HttpGet("Test")]
        public async Task<IActionResult> Test() {
            return Ok(
                new {
                    other_server = Environment.GetEnvironmentVariable("other_server"),
                    another_server = Environment.GetEnvironmentVariable("another_server")
                }
            );
        }
    }
}