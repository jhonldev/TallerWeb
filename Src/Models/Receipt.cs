using Bogus.DataSets;

namespace TallerWeb.Src.Models
{
    public class Receipt
    {
        public int Id { get; set; }
        public string ? NameProduct { get; set; }
        public string ? TypeProduct { get; set; }
        public int UnitPriceProduct { get; set; }
        public int QuantityPruchased { get; set; }
        public int ? PriceFinal { get; set; }
        public DateTime Date { get; set; }


           //Relaciones
        public int IdProduct { get; set; }
        public int IdUser { get; set;}

    }
}