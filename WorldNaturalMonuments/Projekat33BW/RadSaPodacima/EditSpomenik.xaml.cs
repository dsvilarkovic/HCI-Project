using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
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
using Projekat33BW.OsnovniPodaci;
using Projekat33BW.Utils;

namespace Projekat33BW.RadSaPodacima
{
    /// <summary>
    /// Interaction logic for AddSpomenik.xaml
    /// </summary>
    public partial class EditSpomenik : Window, INotifyPropertyChanged
    {
        private Spomenik spomenik;
        private ObservableCollection<TipSpomenika> listaTipova = BazaPodataka.GetInstance().ListaTipovaSpomenika;

        public ObservableCollection<TipSpomenika> ListaTipova => listaTipova;

        /// <summary>
        ///
        /// </summary>

        private ObservableCollection<Etiketa> listaEtiketa = new ObservableCollection<Etiketa>(BazaPodataka.GetInstance().ListaEtiketa);

        public ObservableCollection<Etiketa> ListaEtiketa
        {
            get
            {
                return listaEtiketa;
            }// new ObservableCollection<Etiketa>(BazaPodataka.GetInstance().ListaEtiketa);

            set
            {
                if (value != listaEtiketa)
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
                if (spomenik != value)
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

        private Spomenik stariSpomenik;
        public Spomenik StariSpomenik
        {
            get
            {
                return stariSpomenik;
            }
            set
            {
                if(value != stariSpomenik)
                {
                    stariSpomenik = value;
                    OnPropertyChanged("StariSpomenik");
                }
            }
        }

        /// <summary>
        /// Sluzi za azuriranje spomenika
        /// </summary>
        /// <param name="spomenik"></param>
        /// 
        public EditSpomenik(Spomenik spomenik)
        {
            StariSpomenik = spomenik;
            Spomenik = new Spomenik(spomenik);
            InitializeComponent();
            DataContext = this;


           

            //TODO izbaci iz liste sve koje vec jesu u spomeniku

            foreach(Etiketa etiketa in Spomenik.ListaEtiketa)
            {
                if (ListaEtiketa.Contains(etiketa))
                {
                    ListaEtiketa.Remove(etiketa);
                }
            }
            
        }

        private void Close_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void PotvrdiSpomenik_Click(object sender, RoutedEventArgs e)
        {
            ImeSpomenika.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            Prihod.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            Date.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            if (err_count <= 0)
            {
                //dodaj izmenjeni spomenik u listu
                ObservableCollection<Spomenik> ListaSpomenika = BazaPodataka.GetInstance().ListaSpomenika;
                
                for (int i = 0; i < ListaSpomenika.Count; i++)
                {
                    if(ListaSpomenika[i].IdSpomenika == Spomenik.IdSpomenika)
                    {
                        //BazaPodataka.GetInstance().ListaSpomenika[i] = Spomenik;//new Spomenik(Spomenik);
                        Spomenik stariSpomenik = BazaPodataka.GetInstance().ListaSpomenika[i];
                        stariSpomenik.IdSpomenika = Spomenik.IdSpomenika;
                        stariSpomenik.ImeSpomenika = Spomenik.ImeSpomenika;
                        stariSpomenik.OpisSpomenika = Spomenik.OpisSpomenika;
                        stariSpomenik.TipKlimeSpomenika = Spomenik.TipKlimeSpomenika;
                        stariSpomenik.PutanjaIkonice = Spomenik.PutanjaIkonice;

                        stariSpomenik.IsEkoloskiUgrozen = Spomenik.IsEkoloskiUgrozen;
                        stariSpomenik.IsStanisteUgrozenih = Spomenik.IsStanisteUgrozenih;
                        stariSpomenik.IsNaseljen = Spomenik.IsNaseljen;
                        stariSpomenik.TuristickiStatus = Spomenik.TuristickiStatus;
                        stariSpomenik.PrihodTurizma = Spomenik.PrihodTurizma;
                        stariSpomenik.DatumOtkrivanja = Spomenik.DatumOtkrivanja;
                        stariSpomenik.Position = Spomenik.Position;

                        TipSpomenika tipSpomenika = stariSpomenik.Tip;
                        TipSpomenika noviTipSpomenika = Spomenik.Tip;
                        //ako se menjao tip menjaj i tipovu kolekciju spomenika, inace sve po starom
                        //menjaj i novom i starom tipu kolekciju
                        if(tipSpomenika.IdTipa.Equals(Spomenik.Tip.IdTipa) == false)
                        {
                            //MessageBox.Show(tipSpomenika.IdTipa);
                            //MessageBox.Show(Spomenik.Tip.IdTipa);
                            //tipSpomenika.ListaSpomenikaOvogTipa.Remove(stariSpomenik);
                            BazaPodataka.GetInstance().ListaTipovaSpomenika[BazaPodataka.GetInstance().ListaTipovaSpomenika.IndexOf(tipSpomenika)].ListaSpomenikaOvogTipa.Remove(stariSpomenik);
                            BazaPodataka.GetInstance().ListaTipovaSpomenika[BazaPodataka.GetInstance().ListaTipovaSpomenika.IndexOf(noviTipSpomenika)].ListaSpomenikaOvogTipa.Add(stariSpomenik);
                        }
                        stariSpomenik.Tip = Spomenik.Tip;
                    
                      
                        stariSpomenik.ListaEtiketa = Spomenik.ListaEtiketa;


                        break;
                    }
                }
                

                Close();
            }
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

        /// <summary>
        /// Ubacivanje u listu spomenika koji se dodaje
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void UbaciEtiketu_Click(object sender, RoutedEventArgs e)
        {
            if (ListBoxPostojecihEtiketa.SelectedItem != null)
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
            if (ListBoxNovoubacenihEtiketa.SelectedItem != null)
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

        /// <summary>
        /// 
        /// Proverava se jedinstvenost id-a, spomenika
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void ProveriIdSpomenika(object sender, RoutedEventArgs e)
        {
            /*
            ObservableCollection<Spomenik> listaSpomenika = BazaPodataka.GetInstance().ListaSpomenika;
            //ako nisi nasao, potvrdi
            PotvrdiDugme.IsEnabled = true;

            if (IdSpomenika.Text == null || IdSpomenika.Text.Equals(""))
            {
                PotvrdiDugme.IsEnabled = false;
                return;
            }


            foreach (Spomenik spomenik in listaSpomenika)
            {
                //TODO pazi da idspomenika bude idspomenika.text kad uvezujes odatle
                if (spomenik.IdSpomenika.Equals(IdSpomenika.Text))
                {
                    PotvrdiDugme.IsEnabled = false; //ako nadjes neki, zaustavi ovde
                    return;
                }
            }
            */
        }


        private int err_count = 0;
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
