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
        public IActionResult Index() 
        {
            return Ok("Auth.Service working");
        }

        [HttpPost]
        public IActionResult AuthorizeUse([FromBody]AuthorizeUseRequest request) 
        {
            return Ok(request);
        }
    }
}