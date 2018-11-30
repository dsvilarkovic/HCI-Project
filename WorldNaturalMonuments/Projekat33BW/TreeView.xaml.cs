using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Projekat33BW.OsnovniPodaci;

namespace Projekat33BW
{
    /// <summary>
    /// Interaction logic for TreeView.xaml
    /// </summary>
    public partial class TreeView : Window
    {
        //za data context
        //public BazaPodataka bazaPodataka = BazaPodataka.GetInstance();

        public ObservableCollection<Spomenik> ListaSpomenika => BazaPodataka.GetInstance().ListaSpomenika;
        
        public TreeView()
        {
            InitializeComponent();
            this.DataContext = this;
            
        }
    }
}
