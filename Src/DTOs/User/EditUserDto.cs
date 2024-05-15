using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TallerWeb.Src.DTOs.User
{
    public class EditUserDto
    {
        public string Nombre {get; set;} = string.Empty;
        public DateTime FechaNacimiento {get; set;}
        public int GenderId {get; set;}
    }
}