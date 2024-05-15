using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace TallerWeb.Src.Validators
{
    public partial class RutValidationAttribute : ValidationAttribute
    {
        public override bool IsValid(object? value)
        {
            if (value == null)
            {
                return false;
            }

            string? rut = value.ToString();

            if (string.IsNullOrEmpty(rut))
            {
                return false;
            }

            string rutAxuliar = rut.Replace("-", "").Trim();

            if (rutAxuliar.Length > 9) 
            {
                return false;
            }


            int index = rut.IndexOf('-');

            string rutAux = rut.Substring(0, index);

            string rutAux2 = rut.Substring(index + 1);

            int rutInt;
            bool isValid = int.TryParse(rutAux, out rutInt);
            if (!isValid)
            {
                return false;
            }

            int rutInt2;
            bool isValid2 = int.TryParse(rutAux2, out rutInt2);
            if (!isValid2)
            {
                if(rutAux2 != "K" && rutAux2 != "k")
                {
                    return false;
                }
        
            }

            return true;
        }
    }
}