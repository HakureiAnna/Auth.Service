namespace Auth.Service.Models 
{
    public class Good {
        public int Id { get; set; }
        public int ShopCartId { get; set; }
        public string SKUId { get; set; }
        public int Amount { get; set; }
        public string TotalPrice { get; set; }
        public string SPTM { get; set; }
        public string Name { get; set; }
        public string Price { get; set; }
        public string Img { get; set; }
        public string Memo { get; set; }
    }
}