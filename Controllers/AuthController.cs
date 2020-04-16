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
                    payment_server = Environment.GetEnvironmentVariable("payment_server"),
                    topup_server = Environment.GetEnvironmentVariable("topup_server")
                }
            );
        }
    }
}