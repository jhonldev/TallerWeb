namespace TallerWeb.Src.Models
{
    public class Product
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Tipo { get; set; } = string.Empty;
        public int Precio { get; set; }
        public int CantidadEnStock { get; set; }
        public string Image { get; set; } = string.Empty;
    }
}