using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
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
using Microsoft.Win32;
using Projekat33BW.Help;
using Projekat33BW.OsnovniPodaci;

namespace Projekat33BW.RadSaPodacima
{
    /// <summary>
    /// Interaction logic for AddTipSpomenika.xaml
    /// </summary>
    public partial class EditTipSpomenika : Window, INotifyPropertyChanged
    {
        private TipSpomenika tipspomenika = new TipSpomenika();

        public event PropertyChangedEventHandler PropertyChanged;

        public TipSpomenika TipSpomenika
        {
            get
            {
                return tipspomenika;
            }
            set
            {
                if (tipspomenika != value)
                {
                    tipspomenika = value;
                    OnPropertyChanged("TipSpomenika");
                }

            }
        }

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }
        

        private String staraPutanjaIkonice;
        public EditTipSpomenika(TipSpomenika tipSpomenika)
        {
            TipSpomenika = tipSpomenika;//new TipSpomenika(tipSpomenika);
            staraPutanjaIkonice =  (String) tipSpomenika.PutanjaIkone.Clone();
            InitializeComponent();
            DataContext = TipSpomenika;
            
        }
        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PotvrdiSpomenik_Click(object sender, RoutedEventArgs e)
        {
            ImeTipa.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            if (err_count <= 0)
            {
                ObservableCollection<TipSpomenika> ListaTipovaSpomenika= BazaPodataka.GetInstance().ListaTipovaSpomenika;

                for (int i = 0; i < ListaTipovaSpomenika.Count; i++)
                {
                    if (ListaTipovaSpomenika[i].IdTipa == TipSpomenika.IdTipa)
                    {
                        BazaPodataka.GetInstance().ListaTipovaSpomenika[i] = tipspomenika;
                        tipspomenika = BazaPodataka.GetInstance().ListaTipovaSpomenika[i];
                        break;
                    }
                }
                //sad dodaj svim njegovim spomenicima koji imaju istu putanju novu difoltnu ikonicu
                foreach (Spomenik spomenik in tipspomenika.ListaSpomenikaOvogTipa)
                {
                    if (spomenik.PutanjaIkonice.Equals(staraPutanjaIkonice))
                    {
                        int i =  BazaPodataka.GetInstance().ListaSpomenika.IndexOf(spomenik);
                        BazaPodataka.GetInstance().ListaSpomenika[i].PutanjaIkonice = tipspomenika.PutanjaIkone;
                    }
                }
                
                this.Close();
            }
        }

        private void NadjiIkonu_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg;*.jpeg)|*.png;*.jpg;*.jpeg|All files (*.*)|*.*";
            openFileDialog.InitialDirectory = @"c:\temp\";

            if (openFileDialog.ShowDialog() == true)
            {
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(openFileDialog.FileName);
                bitmap.EndInit();
                PutanjaIkone.Source = bitmap;
            }

        }

        private int err_count = 0;
        private void ImeTipa_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                err_count++;
            else if (e.Action == ValidationErrorEventAction.Removed)
                err_count--;

            if (err_count <= 0)
                PotvrdiDugme.IsEnabled = true;
            else
                PotvrdiDugme.IsEnabled = false;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {
            if(err_count > 0 || string.IsNullOrEmpty(ImeTipa.Text))
            {
                PotvrdiDugme.IsEnabled = false;
                return;
            }
            PotvrdiDugme.IsEnabled = true;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            //pozivanje refresh-a canvasa
            MainWindow wnd = (MainWindow)Application.Current.MainWindow;
            wnd.initCanvas();
        }

        private void Help_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShowHelp help = new ShowHelp("AddTipSpomenika", this);
            help.Show();
        }

        private void Pomoc_Click(object sender, RoutedEventArgs e)
        {
            ShowHelp help = new ShowHelp("AddTipSpomenika", this);
            help.Show();
        }
    }
}
