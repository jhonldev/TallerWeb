using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TallerWeb.Src.DTOs.User
{
    public class LoginUserDto
    {
        [Required(ErrorMessage = "El campo Email es obligatorio")]
        public string Email {get; set; } = string.Empty;
        
        [Required(ErrorMessage = "El campo Contraseña es obligatorio")]
        public string Password {get; set;} = string.Empty;

        
    }
}