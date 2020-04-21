using Newtonsoft.Json;

namespace Auth.Service.Models.DB
{
    public class Member
    {
        [JsonProperty(PropertyName = "id")]
        public string Id { get; set; }
        [JsonProperty("USER_TYPE")]
        public int UserType { get; set; }
        [JsonProperty("PASS_CODE")]
        public string PassCode { get; set; }
        [JsonProperty("BALANCE")]
        public string   Balance { get; set; }

    }
}