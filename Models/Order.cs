using System;
using System.Collections.Generic;

namespace Auth.Service.Models 
{
    public class Order 
    {
        public int Id { get; set; }
        public string Phone { get; set; }
        public string UserId { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime CloseTime { get; set; }
        public byte State { get; set; }
        public string DevId { get; set; }
        public GoodAction GoodAction { get; set; }
        public string Memo { get; set; }
        public string UserType { get; set; }
        public int PayChannel { get; set; }
        public List<Good> GoodsList { get; set; }
    }
}