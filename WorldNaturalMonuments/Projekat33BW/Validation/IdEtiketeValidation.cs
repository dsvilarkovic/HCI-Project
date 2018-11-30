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
    public class IdEtiketeValidation : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            try
            {
                String IdEtikete = value as String;


                ObservableCollection<Etiketa> listaEtiketa= BazaPodataka.GetInstance().ListaEtiketa;

                if (IdEtikete == null || IdEtikete.Equals(""))
                {
                    return new ValidationResult(false, "Molim vas, unesite id etikete");
                }

                if (string.IsNullOrWhiteSpace(IdEtikete))
                {
                    return new ValidationResult(false, "Id se ne sme sastojati samo od razmaka");
                }
                if (IdEtikete.Any(ch => char.IsPunctuation(ch)))
                {
                    return new ValidationResult(false, "Id ne sme sadržati znakove interpunkcije");
                }

                foreach (Etiketa etiketa in listaEtiketa)
                {
                    if (etiketa.IdEtikete.Equals(IdEtikete))
                    {
                        return new ValidationResult(false, "Id etikete nije jedinstveno"); //ako nadjes neki, zaustavi ovde
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
