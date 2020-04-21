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
            // validate user and obtain data for openTheDoor request
            var member = await _memberService.GetMemberWithCredentialsAsync(request.UserId, request.Password);
            if (member == null)
            {
                Console.WriteLine($"Member ID: {request.UserId} cannot be verified");
                return BadRequest("user cannot be verified");
            }

            // send request to pickshop to open the door
            var url = "http://" + 
                Environment.GetEnvironmentVariable("pickshop_server") + "/" +
                Environment.GetEnvironmentVariable("api_openTheDoor");
                
            var requestMessage = new HttpRequestMessage(
                HttpMethod.Post,
                url
            );

            var otdRequest = new OpenTheDoorRequest {
                UserId = member.Id,
                UserType = member.UserType.ToString()
            };

            requestMessage.Headers.Add("Accept", "application/json");
            requestMessage.Content = new StringContent(
                JsonConvert.SerializeObject(otdRequest),
                Encoding.UTF8,
                "application/json"
            );
            var client = _clientFactory.CreateClient();
            var response = await client.SendAsync(requestMessage);

            // return result to test driver
            if (response.IsSuccessStatusCode)
            {
                var responseString = await response.Content.ReadAsStringAsync();
                var result = JsonConvert.DeserializeObject<OpenTheDoorResponse>(responseString);
                Console.WriteLine($"Door Status: {result.Status}");
                return Ok(result);
            }
            else {
                Console.WriteLine("Unable to communicate with PickShop");
                return BadRequest("having problem communication with pickshop");       
            }
        }

        [HttpPost("addMember")]
        public async Task<IActionResult> AddMember([FromBody]AddMember member)
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

        [HttpGet]
        public async Task<IActionResult> GetMember(string id)
        {
            return Ok(await _memberService.GetMemberAsync(id));
        }
    }
}