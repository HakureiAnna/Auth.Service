using System;
using System.Threading.Tasks;
using Auth.Service.Models;
using Microsoft.AspNetCore.Mvc;

namespace Auth.Service.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController: ControllerBase 
    {
        [HttpGet]
        public async Task<IActionResult> Index() 
        {
            return Ok("Auth.Service working");
        }

        [HttpPost("receiveOrder")]
        [Produces("json")]
        public async Task<IActionResult> ReceiveOrder([FromBody]Order order) 
        {
            return Ok(order);            
        }
    }
}