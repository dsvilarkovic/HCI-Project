using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Projekat33BW.Help;
using Projekat33BW.OsnovniPodaci;
using Projekat33BW.RadSaPodacima;
using WPFCustomMessageBox;

namespace Projekat33BW
{
    /// <summary>
    /// Interaction logic for PrikazSpomenika.xaml
    /// </summary>
    public partial class PrikazSpomenika : Window, INotifyPropertyChanged
    {
        private ObservableCollection<TipSpomenika> listaTipovaSpomenika = BazaPodataka.GetInstance().ListaTipovaSpomenika;
        public ObservableCollection<TipSpomenika> ListaTipovaSpomenika
        {
            get
            {
                return listaTipovaSpomenika;
            }
            set
            {
                if(value != listaTipovaSpomenika)
                {
                    listaTipovaSpomenika = value;
                    OnPropertyChanged("ListaTipovaSpomenika");
                }
            }
        }


        private String searchWord = "";

        public String SearchWord
        {
            get
            {
                return searchWord;
            }
            set
            {
                if (value != searchWord)
                {
                    searchWord = value;
                    OnPropertyChanged("SearchWord");
                }
            }
        }

        private ObservableCollection<String> propertyNameList = new ObservableCollection<String>();
        public ObservableCollection<String> PropertyNameList => propertyNameList;
        //TODO MORA PABLIK
        public ObservableCollection<Spomenik> listaSpomenika = BazaPodataka.GetInstance().ListaSpomenika;
        public ObservableCollection<Spomenik> ListaSpomenika
        {
            get
            {
                return listaSpomenika;
            }

            set
            {
                if(value != listaSpomenika)
                {
                    listaSpomenika = value;
                    OnPropertyChanged("ListaSpomenika");
                }
            }

        }



        private Spomenik modelSpomenik = new Spomenik();
        public Spomenik ModelSpomenik
        {
            get
            {
                return modelSpomenik;
            }
            set
            {
                if(value != modelSpomenik)
                {
                    modelSpomenik = value;
                    OnPropertyChanged("ModelSpomenik");
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

        public PrikazSpomenika()
        {
            InitPropertyFilter();
            InitializeComponent();
            DataContext = this;
        }

        private void InitPropertyFilter()
        {
            Spomenik spomenik = new Spomenik();
            foreach (var prop in spomenik.GetType().GetProperties())
            {
                if (!(prop.Name.Equals("TipoviStatusa") || prop.Name.Equals("TipoviKlime")))
                {
                    propertyNameList.Add(prop.Name);
                }
            }
            
        }

        /*Sluzi da sakrije kolone koje se ruzno prikazuju*/
        private void TabelaSpomenikaIzmene(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            //MessageBox.Show("opet?");
            
            string headername = e.Column.Header.ToString();
            
            /*
            int i = ((DataGrid)sender).Columns.Count;
            DataGridColumn column = ((DataGrid)sender).Columns[i - 1];
            
            
            column.Width = new DataGridLength(0.5, DataGridLengthUnitType.Star);
            */
            switch (headername)
            {
                case "TipoviKlime":
                    e.Cancel = true;
                    break;
                case "TipoviKlimeFilter":
                    e.Cancel = true;
                    break;
                case "TipoviStatusa":
                    e.Cancel = true;
                    break;
                case "TipoviStatusaFilter":
                    e.Cancel = true;
                    break;
                case "ListaEtiketa":
                    e.Cancel = true;
                    break;
                case "IdSpomenika":
                    e.Column.Header = "Id";
                    break;
                case "ImeSpomenika":
                    e.Column.Header = "Ime";
                    break;
                case "OpisSpomenika":
                    {
                        e.Column.Header = "Opis";
                        e.Column.MinWidth = 200;
                        
                        
                        break;
                    }
                case "TipKlimeSpomenika":
                    e.Column.Header = "Klima";
                    break;
                case "PutanjaIkonice":
                    e.Cancel = true; //ugasi i prikazi odvojeno
                    break;
                case "IsEkoloskiUgrozen":
                    e.Column.Header = "Ekoloski ugrozen";
                    break;
                case "IsNaseljen":
                    e.Column.Header = "Naseljen";
                    break;
                case "IsStanisteUgrozenih":
                    e.Column.Header = "Stanište ugroženih";
                    break;
                case "TuristickiStatus":
                    e.Column.Header = "Turisticki status";
                    break;
                case "DatumOtkrivanja":
                    e.Column.Header = "Datum Otkrivanja";
                    break;
                case "PrihodTurizma":
                    e.Column.Header = "Prihod";
                    break;
                case "Position":
                    e.Cancel = true;
                    break;
            }
        }

        private void ComboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //MessageBox.Show(((ComboBox)sender).SelectedItem.ToString());
            
        }

        private void TabelaSpomenika_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (TabelaSpomenika.Items.Count > 0)
            {
                Izmeni.IsEnabled = true;
                Obrisi.IsEnabled = true;
            }
            else
            {
                Izmeni.IsEnabled = false;
                Obrisi.IsEnabled = false;
            }
        }

        private void Izmeni_Click(object sender, RoutedEventArgs e)
        {
            if(TabelaSpomenika.SelectedItem != null)
            {
                Spomenik spomenik = TabelaSpomenika.SelectedItem as Spomenik;
                EditSpomenik editSpomenik = new EditSpomenik(spomenik);

                editSpomenik.ShowDialog();
            }
            //pozivanje refresh-a canvasa
            MainWindow wnd = (MainWindow)Application.Current.MainWindow;
            wnd.initCanvas();
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            if (TabelaSpomenika.SelectedItem != null)
            {
                MessageBoxResult result = CustomMessageBox.ShowOKCancel("Da li si siguran da želiš da obrišeš spomenik?","Upozorenje", "Da", "Odustani");
                if (result == MessageBoxResult.OK)
                {
                    Spomenik spomenikZaBrisanje = TabelaSpomenika.SelectedItem as Spomenik;
                    //prvo naci tip u kome se nalazio 
                    ObservableCollection<TipSpomenika> listaTipovaSpomenika = BazaPodataka.GetInstance().ListaTipovaSpomenika;
                    foreach (TipSpomenika tipSpomenika in listaTipovaSpomenika)
                    {
                        if (tipSpomenika.ListaSpomenikaOvogTipa.Contains(spomenikZaBrisanje))
                        {
                            tipSpomenika.ListaSpomenikaOvogTipa.Remove(spomenikZaBrisanje);
                        }
                    }


                    BazaPodataka.GetInstance().ListaSpomenika.Remove(spomenikZaBrisanje);

                    
                }
                //MessageBox.Show("Uspešno obrisan spomenik!");
            }

            if (TabelaSpomenika.Items.Count > 0)
            {
                Izmeni.IsEnabled = true;
                Obrisi.IsEnabled = true;
            }
            else
            {
                Izmeni.IsEnabled = false;
                Obrisi.IsEnabled = false;
            }

            //refresh ovog datagrida
            TabelaSpomenika.Items.Refresh();
            //pozivanje refresh-a canvasa
            MainWindow wnd = (MainWindow)Application.Current.MainWindow;
            wnd.initCanvas();

        }

        private void AddSpomenik_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AddSpomenik addSpomenik = new AddSpomenik();
            addSpomenik.ShowDialog();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void Filtriraj_Click(object sender, RoutedEventArgs e)
        {

            ObservableCollection<Spomenik> filterSpomenika = new ObservableCollection<Spomenik>();
            ObservableCollection<Spomenik> TrenutnaListaSpomenika = BazaPodataka.GetInstance().ListaSpomenika;

            Regex regex;
            if (CaseInSensitive.IsChecked.Value == true)
            {
                regex = new Regex("(" + SearchBox.Text + ")", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            }
            else
            {
                regex = new Regex("(" + SearchBox.Text + ")", RegexOptions.CultureInvariant);
            }

            foreach (Spomenik spomenik in TrenutnaListaSpomenika)
            {

                if (spomenik.IdSpomenika != null && regex.Match(spomenik.IdSpomenika).Success)
                {
                    filterSpomenika.Add(spomenik);
                }
                else if (spomenik.ImeSpomenika != null && regex.Match(spomenik.ImeSpomenika).Success)
                {
                    filterSpomenika.Add(spomenik);
                }
                else if (spomenik.OpisSpomenika != null && regex.Match(spomenik.OpisSpomenika).Success)
                {
                    filterSpomenika.Add(spomenik);
                }
                else if (spomenik.Tip != null && spomenik.Tip.ImeTipa != null && regex.Match(spomenik.Tip.ImeTipa).Success)
                {
                    filterSpomenika.Add(spomenik);
                }
                else if (spomenik.TipKlimeSpomenika != null && regex.Match(spomenik.TipKlimeSpomenika).Success)
                {
                    filterSpomenika.Add(spomenik);
                }
                else if (spomenik.TuristickiStatus != null && regex.Match(spomenik.TuristickiStatus).Success)
                {
                    filterSpomenika.Add(spomenik);
                }
                else if (spomenik.PrihodTurizma.ToString() != null && regex.Match(spomenik.PrihodTurizma.ToString()).Success)
                {
                    filterSpomenika.Add(spomenik);
                }
                else if (spomenik.DatumOtkrivanja != null && regex.Match(spomenik.DatumOtkrivanja).Success)
                {
                    filterSpomenika.Add(spomenik);
                }
                
                else
                {
                    //*** provera po etiketama
                    foreach (Etiketa etiketa in spomenik.ListaEtiketa)
                    {
                        if (etiketa.IdEtikete != null && regex.Match(etiketa.IdEtikete).Success)
                        {
                            filterSpomenika.Add(spomenik);
                            break;
                        }
                    }
                }


                if (DodatniParametarCheckbox.IsChecked.Value == true)
                {
                    //provera da li su checkbox-ovi overeni
                    if (IsNaseljen.IsChecked.Value.Equals(spomenik.IsNaseljen) &&
                        IsStanisteUgrozenih.IsChecked.Value.Equals(spomenik.IsStanisteUgrozenih) &&
                        IsEkoloskiUgrozen.IsChecked.Value.Equals(spomenik.IsEkoloskiUgrozen))
                    {

                    }
                    else
                    {
                        filterSpomenika.Remove(spomenik);
                    }
                }
            }



            Obelezi.IsChecked = false;
            ListaSpomenika = filterSpomenika;
        }

        private bool proveraVazenja(String prviPoredbeniStringSpomenika, String drugiPoredbeniStringSpomenika, String prazna)
        {
            if(prviPoredbeniStringSpomenika == null || prviPoredbeniStringSpomenika.Equals(drugiPoredbeniStringSpomenika) || prviPoredbeniStringSpomenika.Equals(prazna))
            {
                return true;
            }
            return false;
        }

        private void Ponisti_filter_Click(object sender, RoutedEventArgs e)
        {
            ListaSpomenika = BazaPodataka.GetInstance().ListaSpomenika;
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            //pozivanje refresh-a canvasa
            MainWindow wnd = (MainWindow)Application.Current.MainWindow;
            wnd.initCanvas();
        }
        

        private void SearchBox_TextChanged(object sender, TextChangedEventArgs e)
        {
            //Filter_Click(null, null);
            FindControlItem(this.TabelaSpomenika);

        }


        public void FindControlItem(DependencyObject obj)
        {
            for (int i = 0; i < VisualTreeHelper.GetChildrenCount(obj); i++)
            {
                DataGridCell dg = obj as DataGridCell;
                if (dg != null)
                {
                    HighlightText(dg);
                }
                FindControlItem(VisualTreeHelper.GetChild(obj as DependencyObject, i));
            }
        }

        private void HighlightText(Object itx)
        {
            if (itx != null)
            {
                if (itx is TextBlock)
                {
                    String text = SearchBox.Text;

                    Regex regex;
                    if (CaseInSensitive.IsChecked.Value == true)
                    {
                        regex = new Regex("(" + SearchBox.Text + ")", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
                    }
                    else
                    {
                        regex = new Regex("(" + SearchBox.Text + ")", RegexOptions.CultureInvariant);
                    }
                    TextBlock tb = itx as TextBlock;
                    if (SearchBox.Text.Length == 0)
                    {
                        string str = tb.Text;
                        tb.Inlines.Clear();
                        tb.Inlines.Add(str);
                        return;
                    }
                    string[] substrings = regex.Split(tb.Text);
                    tb.Inlines.Clear();
                    foreach (var item in substrings)
                    {
                        if (regex.Match(item).Success)
                        {
                            Run runx = new Run(item);
                            if (Obelezi.IsChecked.Value == true)
                            {
                                runx.Background = Brushes.LightBlue;
                            }
                            else
                            {
                                runx.Background = null;
                            }

                            tb.Inlines.Add(runx);
                        }
                        else
                        {
                            tb.Inlines.Add(item);
                        }
                    }
                    return;
                }
                else
                {
                    for (int i = 0; i < VisualTreeHelper.GetChildrenCount(itx as DependencyObject); i++)
                    {
                        HighlightText(VisualTreeHelper.GetChild(itx as DependencyObject, i));
                    }
                }
            }
        }

        private void CheckBox_Click(object sender, RoutedEventArgs e)
        {
            if (Obelezi.IsChecked == true)
            {
                FindControlItem(this.TabelaSpomenika);
            }
            else
            {
                FindControlItem(this.TabelaSpomenika);
            }
        }

        private void CaseInSensitive_Click(object sender, RoutedEventArgs e)
        {
            FindControlItem(this.TabelaSpomenika);
        }

        private void Help_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShowHelp help = new ShowHelp("PrikazSpomenika", this);
            help.Show();
        }

        private void Pomoc_Click(object sender, RoutedEventArgs e)
        {
            ShowHelp help = new ShowHelp("PrikazSpomenika", this);
            help.Show();
        }
    }
}
