using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Auth.Service.Models.DB
{
    public class AddMember
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty("userType")]
        public int UserType { get; set; }
        [JsonProperty("password")]
        public string Password { get; set; }
        [JsonProperty("balance")]
        public string Balance { get; set; }
        [JsonProperty("memo")]        
        public string Memo { get; set; }

  
    }
}