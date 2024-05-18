using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerWeb.Src.DTOs.User
{
    public class UserDto
    {
        public int Id { get; set; }
        public string Rut {get; set;} = string.Empty;
        public string Nombre { get; set; } = string.Empty;
        public DateTime FechaNacimiento {get;set;}
        public int GenderId {get;set;}
        public string Email { get; set; } = string.Empty;
    }
}