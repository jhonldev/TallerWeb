using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace TallerWeb.Src.DTOs.User
{
    public class ChangePasswordDto
    {
        [Required(ErrorMessage = "El campo Contraseña Actual es obligatorio")]
        public required string ActualPassword { get; set; }

        [Required(ErrorMessage = "El campo Nueva Contraseña es obligatorio")]
        [RegularExpression(@"^(?=.*[0-9])(?=.*[a-zA-Z])[a-zA-Z0-9]+$", ErrorMessage = "La Contraseña debe ser alfanumérica.")]
        [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres.")]
        [MaxLength(20, ErrorMessage = "La contraseña debe tener a lo más 20 caracteres.")]
        public required string NewPassword { get; set; }

        [Required(ErrorMessage = "El campo Confirmar Contraseña es obligatorio")]
        public required string ConfirmPassword { get; set; }
    }
}