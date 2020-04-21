using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Auth.Service.Models.Communication;
using Auth.Service.Models.DB;
using Auth.Service.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Cosmos.Fluent;
using Newtonsoft.Json;

namespace Auth.Service.Controllers 
{
    [ApiController]
    [Route("api/[controller]")]
    public class AuthController: ControllerBase 
    {
        private readonly IHttpClientFactory _clientFactory;
        private readonly IMemberService _memberService;

        public AuthController(
            IHttpClientFactory clientFactory,
            IMemberService memberService
        ) 
        {
            _clientFactory = clientFactory;
            _memberService = memberService;
        }

        [HttpGet("status")]
        public IActionResult Status() 
        {
            return Ok("Auth.Service working");
        }

        [HttpPost("authorizeUser")]
        public async Task<IActionResult> AuthorizeUser([FromBody]AuthorizeUseRequest request) 
        {         
            var url = "http://" + 
                Environment.GetEnvironmentVariable("pickshop_server") + "/" +
                Environment.GetEnvironmentVariable("api_openTheDoor");
                
            var requestMessage = new HttpRequestMessage(
                HttpMethod.Post,
                url
            );

            requestMessage.Headers.Add("Accept", "application/json");
            requestMessage.Content = new StringContent(
                JsonConvert.SerializeObject(request),
                Encoding.UTF8,
                "application/json"
            );
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(requestMessage);

            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OpenTheDoorResponse>(responseString);
                return Ok(result);
            }
            else {
                return StatusCode(500);       
            }
        }

        [HttpPost("addMember")]
        public async Task<IActionResult> AddMember([FromBody]Member member)
        {
            var validInsertion = await _memberService.AddMemberAsync(member);
            if (validInsertion) 
            {
                return Ok("Member added successfully");
            } else 
            {
                return BadRequest("Member already exists");
            }
        }
    }
}