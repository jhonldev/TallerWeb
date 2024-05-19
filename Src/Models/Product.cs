namespace TallerWeb.Src.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Type { get; set; } = string.Empty;
        public int Price { get; set; }
        public int QuantityStock { get; set; }
        public string ? Image { get; set; }

        public string ? IdImage { get; set; }
    }
}