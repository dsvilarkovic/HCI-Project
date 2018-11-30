using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace Projekat33BW.Validation
{
    public class DateValidationParser : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            String format = "dd/mm/yyyy";
            CultureInfo provider = CultureInfo.InvariantCulture;

            if(value == null)
            {
                //nema potrebe za daljom proverom
                return new ValidationResult(true, null);
            }
            try
            {
                String datumZaProveru = value as string;
                DateTime result = DateTime.ParseExact(datumZaProveru.Trim(), format, provider);

                



                //prebaci u tokene da vidis adekvatne dane i mesece, posto ovaj sam resava overload
                String[]  tokens = datumZaProveru.Split('/');

                int day, month, year;

                day = Convert.ToInt32(tokens[0]);
                month = Convert.ToInt32(tokens[1]);
                year = Convert.ToInt32(tokens[2]); // provera zbog prestupnih godina

                
                //provera prvo meseca, zbog februara i parnih neparnih meseci
                if(month < 1 || month > 12)
                {
                    return new ValidationResult(false, "Ne postoji toliko meseci u godini");
                }

                //pa za dane
                if(day < 1 || day > DateTime.DaysInMonth(year, month))
                {
                    WPFCustomMessageBox.CustomMessageBox.Show("da");
                    return new ValidationResult(false, "Ne postoji toliko dana u ovom mesecu");
                }


                //WPFCustomMessageBox.CustomMessageBox.Show(result.Day + " " + result.Month + " " + result.Year);
                return new ValidationResult(true, "Datum uspešno unesen");
                
            }
            catch (FormatException)
            {
                return new ValidationResult(false, "Datum nije validan, tačan dd/mm/yyyy");
            }
            catch (ArgumentNullException)
            {
                //return new ValidationResult(false, "Molim Vas, unesite datum");
            }
            return new ValidationResult(true, null);
        }
    }
}
