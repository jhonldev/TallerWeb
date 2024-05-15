using System.ComponentModel.DataAnnotations;

namespace TallerWeb.Src.DTOs.Product
{
        public class ProductDto
        {
                public int id { get; set; } ///SOlo es para hacer pruebasss

                [Required(ErrorMessage = "El campo nombre es obligatorio")][StringLength(64, MinimumLength = 10, ErrorMessage = "El nombre debe tener entre 10 y 64 caracteres")]
                public string ? Name { get; set; }

                [Required]
                [RegularExpression("^(Tecnología|Electrohogar|Juguetería|Ropa|Muebles|Comida|Libros)$", ErrorMessage = "El tipo de producto es inválido")]
                public string ? Type { get; set; }

                [Required]
                [Range(0, 100000000, ErrorMessage = "El precio debe estar entre 0 y 100000000")]
                public int Price { get; set; }

                [Required]
                [Range(0, 100000, ErrorMessage = "La cantidad en stock debe estar entre 0 y 100000")]
                public int QuantityStock { get; set; }
                
                [Required]
                public string ? Image { get; set; }

        }

}