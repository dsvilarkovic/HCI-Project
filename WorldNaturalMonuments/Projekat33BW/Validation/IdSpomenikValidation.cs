using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using Projekat33BW.OsnovniPodaci;

namespace Projekat33BW.Validation
{
    public class IdSpomenikValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                String IdSpomenika = value as String;

                ObservableCollection<Spomenik> listaSpomenika = BazaPodataka.GetInstance().ListaSpomenika;
                
                
                if(IdSpomenika == null || IdSpomenika.Equals(""))
                {
                    return new ValidationResult(false, "Molim vas, unesite id spomenika");
                }
                if (string.IsNullOrWhiteSpace(IdSpomenika))
                {
                    return new ValidationResult(false, "Id se ne sme sastojati samo od razmaka");
                }
                if (IdSpomenika.Any(ch => char.IsPunctuation(ch)))
                {
                    return new ValidationResult(false, "Id ne sme sadržati znakove interpunkcije");
                }

                foreach (Spomenik spomenik in listaSpomenika)
                {
                    if (spomenik.IdSpomenika.Equals(IdSpomenika))
                    {
                        return new ValidationResult(false, "Id spomenika nije jedinstven"); //ako nadjes neki, zaustavi ovde
                    }
                }

                return new ValidationResult(true, null);

            }
            catch(Exception e)
            {
                return new ValidationResult(false, "Desila se greška u validaciji Vašeg unosa.");
            }


            
        }

        
    }
}
