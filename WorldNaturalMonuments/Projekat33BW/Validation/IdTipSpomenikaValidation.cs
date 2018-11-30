using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using Projekat33BW.OsnovniPodaci;

namespace Projekat33BW.Validation
{
    public class IdTipSpomenikaValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                String IdTipa = value as String;
                

                ObservableCollection<TipSpomenika> listaTipovaSpomenika = BazaPodataka.GetInstance().ListaTipovaSpomenika;


                if (IdTipa == null || IdTipa.Equals(""))
                {
                    return new ValidationResult(false, "Molim vas, unesite id tipa spomenika");
                }

                if (string.IsNullOrWhiteSpace(IdTipa))
                {
                    return new ValidationResult(false, "Id se ne sme sastojati samo od razmaka");
                }
                if (IdTipa.Any(ch => char.IsPunctuation(ch)))
                {
                    return new ValidationResult(false, "Id ne sme sadržati znakove interpunkcije");
                }
                foreach (TipSpomenika tipSpomenika in listaTipovaSpomenika)
                {
                    if (tipSpomenika.IdTipa.Equals(IdTipa))
                    {
                        return new ValidationResult(false, "Id tipa nije jedinstveno"); //ako nadjes neki, zaustavi ovde
                    }
                }

            }
            catch (Exception e)
            {
                return new ValidationResult(false, "Greška u unosu");
            }

            return new ValidationResult(true, null);
        }
    }
}
