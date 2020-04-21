using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace Auth.Service.Models.DB
{
    public class Member
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

        // system properties
        [JsonProperty("_ts")]
        [JsonConverter(typeof(UnixDateTimeConverter))]
        public DateTime  LastModifiedTime { get; internal set; }
        [JsonProperty("_rid")]
        public string Rid { get; internal set; }
        [JsonProperty("_self")]
        public string Self { get; internal set; }
        [JsonProperty("_etag")]
        public string ETag { get; internal set; }
        [JsonProperty("_attachments")]        
        public string Attachments { get; internal set; }
    }
}