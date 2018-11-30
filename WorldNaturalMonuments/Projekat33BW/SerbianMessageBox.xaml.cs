using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Projekat33BW
{
    /// <summary>
    /// Interaction logic for SerbianMessageBox.xaml
    /// </summary>
    public partial class SerbianMessageBox : Window
    {
        private String sadrzaj = "Pozdrav";
        private String potvrdi = "Potvrdi";
        private String odustani = "Odustani";
        private String naslov = "Poruka";

        public SerbianMessageBox(String sadrzaj, String potvrdi, String odustani, String naslov)
        {
            
            Sadrzaj.Content = sadrzaj;
            Potvrdi.Content = potvrdi;
            Odustani.Content = odustani;
            Title = naslov;
        }

        public SerbianMessageBox()
        {
            InitializeComponent();
            Sadrzaj.Content = sadrzaj;
            Potvrdi.Content = potvrdi;
            Odustani.Content = odustani;
            Title = naslov;
        }

        
        public static void Pokazi()
        {
            
        }

        private void Potvrdi_Click(object sender, RoutedEventArgs e)
        {
                
        }

        private void Odustani_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
