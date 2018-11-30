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
    /// Interaction logic for TabelaTipova.xaml
    /// </summary>
    public partial class TabelaTipova : Window, INotifyPropertyChanged
    {
        public TabelaTipova()
        {
            InitializeComponent();
            DataContext = this;
            listaTipovaSpomenika = BazaPodataka.GetInstance().ListaTipovaSpomenika;
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

        private ObservableCollection<TipSpomenika> listaTipovaSpomenika;

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
        

        private TipSpomenika modelTipSpomenika = new TipSpomenika();
        public TipSpomenika ModelTipSpomenika
        {
            get
            {
                return modelTipSpomenika;
            }
            set
            {
                if(value != modelTipSpomenika)
                {
                    modelTipSpomenika = value;
                    OnPropertyChanged("ModelTipSpomenika");
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

        private void TipSpomenikaIzmeniPrikaz_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();
            int i = ((DataGrid)sender).Columns.Count;
            DataGridColumn column = ((DataGrid)sender).Columns[i - 1];
            //column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            

            switch (headername)
            {
                case "PutanjaIkone":
                    e.Cancel = true;
                    break;
                case "IdTipa":
                    e.Column.Header = "Id tipa";
                    break;
                case "ImeTipa":
                    e.Column.Header = "Ime tipa";
                    break;
                case "OpisTipa":
                    e.Column.Header = "Opis tipa";
                    break;
                case "ListaSpomenikaOvogTipa":
                    e.Cancel = true;
                    break;
            }
        }

        private void IzmeniTip_Click(object sender, RoutedEventArgs e)
        {
            if (TabelaTipa.SelectedItem != null) 
            {
                TipSpomenika tipSpomenika= TabelaTipa.SelectedItem as TipSpomenika;
                //da se izbegne no type
                if (tipSpomenika.IdTipa.Equals("") == false)
                {
                    EditTipSpomenika editTipSpomenika = new EditTipSpomenika(tipSpomenika);

                    editTipSpomenika.ShowDialog();
                }
            }
        }

        private void Obrisi_Click(object sender, RoutedEventArgs e)
        {
            if(TabelaTipa.SelectedItem != null)
            {
                TipSpomenika tipSpomenika = TabelaTipa.SelectedItem as TipSpomenika;
                //provera da li se pokusava beztipski obrisati
                if (!(tipSpomenika == null || tipSpomenika.IdTipa.Equals("")))
                {
                    //MessageBoxResult result = CustomMessageBox.ShowOKCancel("Da li si siguran da želiš da obrišeš tip? \n To povlači i prebacivanje spomenika u beztipne", "Upozorenje", "Da", "Odustani");
                    MessageBoxResult result = CustomMessageBox.ShowYesNoCancel("\r Da li si siguran da želiš da obrišeš tip? \n Možeš da ostaviš spomenike ovog tipa ili da ih prebaciš u beztipne. ", "Upozorenje", "Ostavi spomenike", "Obriši i spomenike", "Odustani");
                    if (result == MessageBoxResult.Yes) //(result == MessageBoxResult.OK)
                    {



                        BazaPodataka bazaPodataka = BazaPodataka.GetInstance();
                        //ako se brise tip, onda se uklanja i spomenik koji ga sadrzi
                        //i tip iz baze tipova spomenika


                        //pokupi iz baze spomenike koji koriste tip
                        ObservableCollection<Spomenik> listaZaBrisanje = new ObservableCollection<Spomenik>();

                        //trazi notype u bazi
                        TipSpomenika noType = null;
                        for (int i = 0; i < bazaPodataka.ListaTipovaSpomenika.Count; i++)
                        {
                            //glup nacin
                            if (bazaPodataka.ListaTipovaSpomenika[i].IdTipa.Equals(BazaPodataka.noTypeTip.IdTipa))
                            {
                                noType = bazaPodataka.ListaTipovaSpomenika[i];
                            }
                        }

                        foreach (Spomenik spomenik in BazaPodataka.GetInstance().ListaSpomenika/*bazaPodataka.ListaSpomenika*/)
                        {
                            if (spomenik.Tip.IdTipa.Equals(tipSpomenika.IdTipa))
                            {
                                //otkomentarisi ako zaseres
                                //listaZaBrisanje.Add(spomenik);

                                //ako nadjes tip za "brisanje", dodeli mu notype
                                spomenik.Tip = noType;
                                if (!noType.ListaSpomenikaOvogTipa.Contains(spomenik))
                                {
                                    noType.ListaSpomenikaOvogTipa.Add(spomenik);
                                }
                            }
                        }

                        

                        //sad sledi brisanje iz baze samo tipa
                        if (bazaPodataka.ListaTipovaSpomenika.Remove(tipSpomenika))
                        {
                            //MessageBox.Show("Uspešno obrisan tip");
                        }
                        else
                        {
                            throw new Exception("Neuspelo brisanje tipa iz baze tipova");
                        }

                        foreach (Spomenik spomenik in BazaPodataka.GetInstance().ListaSpomenika)
                        {
                            if (spomenik.Tip == null)
                            {
                                spomenik.Tip = noType;
                            }
                        }

                    }
                    else if(result == MessageBoxResult.No)
                    {
                        //brisi i spomenike
                        //nadji od tipSpomenik, sve njegove tipove, pa njih prvo obrisi iz liste spomenika baze podataka
                        //pa ih obrisi iz tipa spomenika


                        ObservableCollection<Spomenik> listaSpomenika = BazaPodataka.GetInstance().ListaSpomenika;
                        ObservableCollection<Spomenik> listaZaBrisanje = new ObservableCollection<Spomenik>();
                        //za svaki spomenik tipa, nadji u listi spomenika
                        foreach (Spomenik spomenikTipa in listaSpomenika)
                        {
                            foreach (Spomenik spomenik in tipSpomenika.ListaSpomenikaOvogTipa)
                            {
                                if (spomenik.IdSpomenika.Equals(spomenikTipa.IdSpomenika))
                                {
                                    listaZaBrisanje.Add(spomenik);
                                }
                            }
                        }

                        //ukloni ih sad
                        foreach (Spomenik spomenikZaBrisanje in listaZaBrisanje)
                        {
                            if (BazaPodataka.GetInstance().ListaSpomenika.Remove(spomenikZaBrisanje) == false)
                            {
                                MessageBox.Show("Ne radi");
                            }
                        }


                        //sad pocisti i listutipa
                        tipSpomenika.ListaSpomenikaOvogTipa.Clear();

                        //sad i tip
                        //sad sledi brisanje iz baze samo tipa
                        if (BazaPodataka.GetInstance().ListaTipovaSpomenika.Remove(tipSpomenika))
                        {
                            //MessageBox.Show("Uspešno obrisan tip");
                        }
                        else
                        {
                            throw new Exception("Neuspelo brisanje tipa iz baze tipova");
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Ne postoji etiketa pod ovim imenom!");
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }

        private void AddTipSpomenika_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AddTipSpomenika addTipSpomenika = new AddTipSpomenika();
            addTipSpomenika.ShowDialog();
        }

        private void TabelaTipa_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (TabelaTipa.Items.Count > 0)
            {
                if (TabelaTipa.SelectedItem != null)
                {
                    TipSpomenika tipSpomenika = TabelaTipa.SelectedItem as TipSpomenika;
                    if (tipSpomenika.IdTipa.Equals("null") == false) // ako je difolt tip
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
            }
            else
            {
                Izmeni.IsEnabled = false;
                Obrisi.IsEnabled = false;
            }


        }

        private void Filtriraj_Click(object sender, RoutedEventArgs e)
        {
            /*
            ObservableCollection<TipSpomenika> filterTipSpomenika= new ObservableCollection<TipSpomenika>();
            ObservableCollection<TipSpomenika> listaTipovaSpomenika = BazaPodataka.GetInstance().ListaTipovaSpomenika;
            foreach (TipSpomenika tipSpomenika in listaTipovaSpomenika)
            {
                if (ModelTipSpomenika.IdTipa == null || ModelTipSpomenika.IdTipa.Equals(tipSpomenika.IdTipa) || ModelTipSpomenika.IdTipa.Equals(""))
                {
                    if (ModelTipSpomenika.ImeTipa == null || ModelTipSpomenika.ImeTipa.Equals(tipSpomenika.ImeTipa) || modelTipSpomenika.ImeTipa.Equals(""))
                    {
                        filterTipSpomenika.Add(tipSpomenika);
                    }
                }
            }
            
            ListaTipovaSpomenika = filterTipSpomenika;
            */


            ObservableCollection<TipSpomenika> filterTipSpomenika = new ObservableCollection<TipSpomenika>();
            ObservableCollection<TipSpomenika> trenutnalistaTipova = BazaPodataka.GetInstance().ListaTipovaSpomenika;


            Regex regex;
            if (CaseInSensitive.IsChecked.Value == true)
            {
                regex = new Regex("(" + SearchBox.Text + ")", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            }
            else
            {
                regex = new Regex("(" + SearchBox.Text + ")", RegexOptions.CultureInvariant);
            }

            foreach (TipSpomenika tipSpomenika in trenutnalistaTipova)
            {
                if(tipSpomenika.IdTipa != null && regex.Match(tipSpomenika.IdTipa).Success)
                {
                    filterTipSpomenika.Add(tipSpomenika);
                }
                else if (tipSpomenika.ImeTipa != null && regex.Match(tipSpomenika.ImeTipa).Success)
                {
                    filterTipSpomenika.Add(tipSpomenika);
                }
                else if (tipSpomenika.OpisTipa != null && regex.Match(tipSpomenika.OpisTipa).Success)
                {
                    filterTipSpomenika.Add(tipSpomenika);
                }
            }

            Obelezi.IsChecked = false;
            ListaTipovaSpomenika = filterTipSpomenika;
        }

        private void OtkaziFilter_Click(object sender, RoutedEventArgs e)
        {
            ListaTipovaSpomenika = BazaPodataka.GetInstance().ListaTipovaSpomenika;
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
            FindControlItem(this.TabelaTipa);

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
                FindControlItem(this.TabelaTipa);
            }
            else
            {
                FindControlItem(this.TabelaTipa);
            }
        }

        private void CaseInSensitive_Click(object sender, RoutedEventArgs e)
        {
            FindControlItem(this.TabelaTipa);
        }

        private void Help_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShowHelp help = new ShowHelp("TabelaTipova", this);
            help.Show();
        }

        private void Pomoc_Click(object sender, RoutedEventArgs e)
        {
            ShowHelp help = new ShowHelp("TabelaTipova", this);
            help.Show();
        }
    }
}
