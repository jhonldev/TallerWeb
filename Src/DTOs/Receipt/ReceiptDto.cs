using System.ComponentModel.DataAnnotations;

namespace TallerWeb.Src.DTOs.Receipt
{
        public class ReceiptDto
        {
            [Required]
            public int Id { get; set; }
            [Required]
            public int IdProduct { get; set; }
            [Required]
            public string ? NameProduct { get; set; }
            [Required]
            public string ? TypeProduct { get; set; }
            [Required]
            public int UnitPriceProduct { get; set; }
            [Required]
            public int QuantityPruchased { get; set; }
            [Required]
            public int ? PriceFinal { get; set; }
            [Required]
            public DateTime Date { get; set; }
        }
}