using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Projekat33BW.Validation
{
    class NameValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            Console.WriteLine("Jednom se aktiviro2");
            String name = value as String;
            if(name == null || name == "")
            {
                return new ValidationResult(false, "Molim vas, unesite ime");
            }
            if (string.IsNullOrWhiteSpace(name))
            {
                return new ValidationResult(false, "Ime se ne sme sastojati samo od razmaka");
            }
            return new ValidationResult(true, null);
        }
    }
}
