using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading;
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
using Projekat33BW.OsnovniPodaci;
using Projekat33BW.Utils;

namespace Projekat33BW.RadSaPodacima
{
    /// <summary>
    /// Interaction logic for AddSpomenik.xaml
    /// </summary>
    public partial class AddSpomenik : Window, INotifyPropertyChanged
    {
        private Spomenik spomenik;
        private ObservableCollection<TipSpomenika> listaTipova = BazaPodataka.GetInstance().ListaTipovaSpomenika;
        private ObservableCollection<Etiketa> listaEtiketa = new ObservableCollection<Etiketa>(BazaPodataka.GetInstance().ListaEtiketa);

        public ObservableCollection<TipSpomenika> ListaTipova => listaTipova;

        public ObservableCollection<Etiketa> ListaEtiketa {
            get
            {
                return listaEtiketa;
            }// new ObservableCollection<Etiketa>(BazaPodataka.GetInstance().ListaEtiketa);

            set
            {
                if(value != listaEtiketa)
                {
                    listaEtiketa = value;
                    OnPropertyChanged("ListaEtiketa");
                }
            }
        }
        public Spomenik Spomenik
        {
            get { return spomenik; }
            set
            {
                if(spomenik != value)
                {
                    spomenik = value;
                    OnPropertyChanged("Spomenik");
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        public AddSpomenik()
        {
            spomenik = new Spomenik();
            
            InitializeComponent();

            DataContext = this;
            

            //TODO zabraniti dugme dodaj dok sen e unese id spomenika
            PotvrdiDugme.IsEnabled = false;
            
        }

        /// <summary>
        /// Sluzi za azuriranje spomenika
        /// </summary>
        /// <param name="spomenik"></param>
        public AddSpomenik(Spomenik spomenik)
        {
            Spomenik = spomenik;
            //MessageBox.Show(Spomenik.Tip.ImeTipa);
            InitializeComponent();
            DataContext = this;
            

        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {

            Close();
        }

        private void NadjiIkonu_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            openFileDialog.Filter = "Image files (*.png;*.jpg)|*.png;*.jpg|All files (*.*)|*.*";
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

        private void DodajEtiketu_Click(object sender, RoutedEventArgs e)
        {
            TabelaEtiketa tabela = new TabelaEtiketa();
            tabela.ShowDialog();
        }

        private void PotvrdiSpomenik_Click(object sender, RoutedEventArgs e)
        {
            IdSpomenika.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            ImeSpomenika.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            Prihod.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            Date.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            TipSpomenika.GetBindingExpression(ComboBox.TextProperty).UpdateSource();

            if (err_count <= 0)
            {
                String lista = "";
                TipSpomenika tipSpomenika = Spomenik.Tip;



                foreach (var item in Spomenik.ListaEtiketa)
                {
                    lista += item.IdEtikete;
                }
                //ako spomenik nema ikonu odabranu, dodeli mu onu od tipa spomenika
                if (Spomenik.PutanjaIkonice == null)
                {

                    //ako tip spomenika uopste ima ikonicu
                    //TODO izbaciti posle ako se stavi pod obavezno da tip spomenika mora imati ikonu
                    //TODO WARN: moguce reference i problemi
                    if (tipSpomenika != null)
                    {
                        if (tipSpomenika.PutanjaIkone != null)
                        {
                            Spomenik.PutanjaIkonice = tipSpomenika.PutanjaIkone;
                        }
                    }
                }
                if (BazaPodataka.GetInstance().ListaSpomenika.All(x => x.IdSpomenika != Spomenik.IdSpomenika))
                {
                    //MessageBox.Show("Ubačen novi spomenik!");
                    BazaPodataka.GetInstance().ListaSpomenika.Add(Spomenik);
                }
                else
                {
                    MessageBox.Show("Uneseni spomenik postoji pod ovim id-em");
                }


                //dodavanje spomenika u njegov tip
                //nadji njegov tip spomenika
                ObservableCollection<TipSpomenika> listaTipovaSpomenika = BazaPodataka.GetInstance().ListaTipovaSpomenika;

                //ako nema spomenika u tom tipu, dodaj ga u listu spomenika ovog tipa
                ObservableCollection<Spomenik> listaSpomenikaOvogTipa =
                    listaTipovaSpomenika[listaTipovaSpomenika.IndexOf(tipSpomenika)].ListaSpomenikaOvogTipa;
                if (listaSpomenikaOvogTipa.Contains(spomenik) == false)
                {
                    listaSpomenikaOvogTipa.Add(spomenik);
                }
                
                this.Close();

            }
        }

        /// <summary>
        /// Ubacivanje u listu spomenika koji se dodaje
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UbaciEtiketu_Click(object sender, RoutedEventArgs e)
        {
            if(ListBoxPostojecihEtiketa.SelectedItem != null)
            {
                Etiketa etiketa = ListBoxPostojecihEtiketa.SelectedItem as Etiketa;
                
                //nema potrebe dodavati ako vec jeste
                if (!Spomenik.ListaEtiketa.Contains(etiketa))
                {
                    //ListBoxNovoubacenihEtiketa.Items.Add(etiketa);
                    Spomenik.ListaEtiketa.Add(etiketa);
                    ListaEtiketa.Remove(etiketa);
                    //ListBoxPostojecihEtiketa.Items.Remove(etiketa);
                    
                }
            }
        }

        /// <summary>
        /// Izbacivanje iz liste spomenika koji se dodaje
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void IzbaciEtiketu_Click(object sender, RoutedEventArgs e)
        {
            if(ListBoxNovoubacenihEtiketa.SelectedItem != null)
            {
                Etiketa etiketa = ListBoxNovoubacenihEtiketa.SelectedItem as Etiketa;
                //nema potrebe dodavati ako vec nije
                if (!ListBoxPostojecihEtiketa.Items.Contains(etiketa))
                {
                    Spomenik.ListaEtiketa.Remove(etiketa);
                    ListaEtiketa.Add(etiketa);
                }
            }
        }

        
      


        private int err_count = 0;
        private void IdSpomenika_Error(object sender, ValidationErrorEventArgs e)
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

        private void ImeSpomenika_Error(object sender, ValidationErrorEventArgs e)
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

        private void Prihod_Error(object sender, ValidationErrorEventArgs e)
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

        private void Date_Error(object sender, ValidationErrorEventArgs e)
        {

           
            if (e.Action == ValidationErrorEventAction.Added)
                err_count++;
            else if (e.Action == ValidationErrorEventAction.Removed)
                err_count--;

            //WPFCustomMessageBox.CustomMessageBox.Show(err_count + "");

            if (err_count <= 0)
                PotvrdiDugme.IsEnabled = true;
            else
                PotvrdiDugme.IsEnabled = false;
        }

        private void TipSpomenika_Error(object sender, ValidationErrorEventArgs e)
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
            if (err_count > 0 || ImeSpomenika.Text.Equals("") || IdSpomenika.Text.Equals("") || string.IsNullOrEmpty(TipSpomenika.Text))
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


        private void AddTipSpomenika_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AddTipSpomenika addTipSpomenika = new AddTipSpomenika();
            addTipSpomenika.ShowDialog();
        }

        private void AddEtiketa_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AddEtiketa addEtiketa = new AddEtiketa(listaEtiketa);
            addEtiketa.ShowDialog();
        }

        private void Help_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            HelpProvider.ShowHelp("AddSpomenik", this);
        }

        private void Pomoc_Click(object sender, RoutedEventArgs e)
        {
            HelpProvider.ShowHelp("AddSpomenik", this);
        }
        

        private void TipSpomenika_DropDownClosed(object sender, EventArgs e)
        {
            if (err_count > 0 || ImeSpomenika.Text.Equals("") || IdSpomenika.Text.Equals("") || string.IsNullOrEmpty(TipSpomenika.Text))
            {
                PotvrdiDugme.IsEnabled = false;
                return;
            }
            PotvrdiDugme.IsEnabled = true;
        }
    }
}
