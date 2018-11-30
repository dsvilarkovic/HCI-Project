using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Projekat33BW.Validation
{
    class ComboBoxValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            String comboBox = value as String;

            if(comboBox == null || comboBox.Equals(""))
            {
                return new ValidationResult(false, "Molim vas, odaberite tip spomenika,\n ako postoji.");
            }
            return new ValidationResult(true, null);
        }
    }
}
