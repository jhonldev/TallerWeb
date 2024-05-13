namespace TallerWeb.Src.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string ? Name { get; set; }
        public string ? Type { get; set; }
        public int Price { get; set; }
        public int QuantityStock { get; set; }
        public string ? Image { get; set; }
    }
}