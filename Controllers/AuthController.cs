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
        [HttpGet("status")]
        public IActionResult Status() 
        {
            return Ok("Auth.Service working");
        }

        [HttpPost("authorizeUse")]
        public IActionResult AuthorizeUse([FromBody]AuthorizeUseRequest request) 
        {
            return Ok(request);
        }
    }
}