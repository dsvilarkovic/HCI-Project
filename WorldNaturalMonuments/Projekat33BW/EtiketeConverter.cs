using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using Projekat33BW.OsnovniPodaci;

namespace Projekat33BW
{
    class EtiketeConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            String listaString = "";
            try
            {
                ObservableCollection<Etiketa> listaEtiketa = value as ObservableCollection<Etiketa>;


                int i = 1;
                foreach (Etiketa etiketa in listaEtiketa)
                {
                    //WPFCustomMessageBox.CustomMessageBox.Show(etiketa.BojaEtikete);
                    listaString += (i++) + ". " + etiketa.IdEtikete + " (Boja: " + etiketa.BojaEtikete +")" +
                        " (Opis: " + etiketa.OpisEtikete + ")" + "\n";
                }
            } 
            catch (Exception e)
            {

            }

            return listaString;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
