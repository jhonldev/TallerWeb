using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using TallerWeb.Src.Validators;

namespace TallerWeb.Src.DTOs.User
{
    public class RegisterUserDto
    {
        [Required(ErrorMessage = "El campo Rut es obligatorio")]
        [RutValidation(ErrorMessage = "El Rut no es correcto")]
        public string ? Rut {get; set; }

        [Required(ErrorMessage = "El campo Nombre es obligatorio")]
        [MinLength(8, ErrorMessage ="El nombre debe tener al menos 8 caracteres.")]
        [MaxLength(255, ErrorMessage = "El nombre debe tener a lo más 255 caracteres.")]
        public string ? Nombre {get; set; } 

        [Required(ErrorMessage = "El campo Fecha de Nacimiento es obligatorio")]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime FechaNacimiento {get; set;}

        [Required(ErrorMessage = "El campo Email es obligatorio")]
        [EmailAddress(ErrorMessage = "El Email no tiene formato valido")]
        public string ? Email {get; set;}

        [Required(ErrorMessage = "El campo Genero es obligatorio")]
        public int GenderId {get; set; }

        [Required(ErrorMessage = "El campo Contraseña es obligatorio")]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-zA-Z])[a-zA-Z0-9]+$", ErrorMessage = "La Contraseña debe ser alfanumérica.")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        [MaxLength(20, ErrorMessage = "La contraseña debe tener a lo más 20 caracteres.")]
        public string ? Password {get; set;}

        [Required(ErrorMessage = "El campo Confirmar Contraseña es obligatorio")]
        [Compare("Password", ErrorMessage = "Las contraseñas no coinciden.")]
        public string ? ConfirmPassword {get; set;}
    }
}