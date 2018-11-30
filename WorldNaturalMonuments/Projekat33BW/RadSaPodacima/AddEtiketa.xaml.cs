using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Projekat33BW.Help;
using Projekat33BW.OsnovniPodaci;

namespace Projekat33BW.RadSaPodacima
{
    //TODO konsultacije color: Color picker
    /// <summary>
    /// Interaction logic for AddEtiketa.xaml
    /// </summary>
    public partial class AddEtiketa : Window, INotifyPropertyChanged
    {
        private Etiketa etiketa = new Etiketa();

        public event PropertyChangedEventHandler PropertyChanged;

        public Etiketa Etiketa
        {
            get {
                return etiketa;
            }
            set
            {
                if(etiketa != value)
                {
                    etiketa = value;
                    OnPropertyChanged("Etiketa");
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

        /// <summary>
        /// Konstruktor AddEtiketa
        /// </summary>
        public AddEtiketa()
        {
            
            InitializeComponent();
            DataContext = Etiketa;

            PotvrdiDugme.IsEnabled = false;

        }

        private ObservableCollection<Etiketa> listaPostojecihEtiketaSpomenik = null;
        public AddEtiketa(ObservableCollection<Etiketa> listaPostojecihEtiketaSpomenik)
        {
            InitializeComponent();
            DataContext = Etiketa;
            this.listaPostojecihEtiketaSpomenik = listaPostojecihEtiketaSpomenik;
            
        }


        

        private void Close(object sender, RoutedEventArgs e)
        {
            Close();
        }

        /// <summary>
        /// Dodaje etiketu u bazu podataka
        /// </summary>
        private void PotvrdiEtiketu(object sender, RoutedEventArgs e)
        {
            IdEtikete.GetBindingExpression(TextBox.TextProperty).UpdateSource();

            if (err_count <= 0)
            {
                if (BazaPodataka.GetInstance().ListaEtiketa.All(x => x.IdEtikete != Etiketa.IdEtikete))
                {
                    //MessageBox.Show("Ubačena nova etiketa!");
                    BazaPodataka.GetInstance().ListaEtiketa.Add(etiketa);
                }

                //ako je pozvana iz add ili edit spomenik
                if (listaPostojecihEtiketaSpomenik != null)
                {
                    listaPostojecihEtiketaSpomenik.Add(etiketa);
                }
                this.Close();
            }


            
        }

        private void ProveriIdEtikete(object sender, RoutedEventArgs e)
        {
            /*
            ObservableCollection<Etiketa> listaPostojecihEtiketaSpomenik = BazaPodataka.GetInstance().ListaEtiketa;
            //ako nisi nasao, potvrdi
            PotvrdiDugme.IsEnabled = true;

            if (IdEtikete.Text == null || IdEtikete.Text.Equals(""))
            {
                PotvrdiDugme.IsEnabled = false;
                return;
            }


            foreach (Etiketa etiketa in listaPostojecihEtiketaSpomenik)
            {
                //TODO pazi da idspomenika bude idspomenika.text kad uvezujes odatle
                if (etiketa.IdEtikete.Equals(IdEtikete.Text))
                {
                    PotvrdiDugme.IsEnabled = false; //ako nadjes neki, zaustavi ovde
                    return;
                }
            }
            */
        }

        private void IdEtikete_KeyUp(object sender, KeyEventArgs e)
        {
            
            /*
            ObservableCollection<Etiketa> listaPostojecihEtiketaSpomenik = BazaPodataka.GetInstance().ListaEtiketa;
            //ako nisi nasao, potvrdi
            PotvrdiDugme.IsEnabled = true;

            if (IdEtikete.Text == null || IdEtikete.Text.Equals(""))
            {
                PotvrdiDugme.IsEnabled = false;
                return;
            }


            foreach (Etiketa etiketa in listaPostojecihEtiketaSpomenik)
            {
                //TODO pazi da idspomenika bude idspomenika.text kad uvezujes odatle
                if (etiketa.IdEtikete.Equals(IdEtikete.Text))
                {
                    PotvrdiDugme.IsEnabled = false; //ako nadjes neki, zaustavi ovde
                    return;
                }
            }
            */
        }

        private int err_count = 0;
        

        private void IdEtikete_Error(object sender, ValidationErrorEventArgs e)
        {
            if (e.Action == ValidationErrorEventAction.Added)
                err_count++;
            else if(e.Action == ValidationErrorEventAction.Removed)
                err_count--;
            
            if (err_count <= 0)
                PotvrdiDugme.IsEnabled = true;
            else
                PotvrdiDugme.IsEnabled = false;
        }

        private void Window_KeyUp(object sender, KeyEventArgs e)
        {

            if (err_count > 0 || string.IsNullOrEmpty(IdEtikete.Text))
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


        public List<Object> listaObjekataZaTutorial = new List<object>();
        public void initTutorial()
        {
            listaObjekataZaTutorial.Add(IdEtikete);
            listaObjekataZaTutorial.Add(ListaBoja);
            listaObjekataZaTutorial.Add(OpisEtikete);
            listaObjekataZaTutorial.Add(PotvrdiDugme);
            listaObjekataZaTutorial.Add(OtkaziDugme);
            listaObjekataZaTutorial.Add(Pomoc);
            listaObjekataZaTutorial.Add(Tutorial);
          
            defaultBrush = IdEtikete.BorderBrush;
            defaultThickness = IdEtikete.BorderThickness;
        }


        private Button sledeci = new Button();
        private Popup codePopup = new Popup();
        private TextBlock popupText = new TextBlock();
        private Brush defaultBrush;
        private Thickness defaultThickness;
        /// <summary>
        /// Broji na kom smo koraku tutoriala
        /// </summary>
        private int tutorialSlideCounter = 0;
        private Boolean tutorialStarted = false;

        private void Tutorial_Click(object sender, RoutedEventArgs e)
        {
            if (tutorialStarted == false)
            {
                tutorialStarted = true;
                initTutorial();
                //dugmad potrebna
                Button sledeci = new Button();
                sledeci.Content = "Sledeće";
                sledeci.AddHandler(Button.ClickEvent, new RoutedEventHandler(sledeci_Click));
                Button odustani = new Button();
                odustani.Content = "Odustani";
                odustani.AddHandler(Button.ClickEvent, new RoutedEventHandler(odustani_Click));

                //Sadrzaj teksta i pizzkanje
                //TextBlock popupText = new TextBlock();
                popupText.TextWrapping = TextWrapping.Wrap;
                popupText.Background = Brushes.LightBlue;
                popupText.Foreground = Brushes.Blue;


                StackPanel stackPanel = new StackPanel();
                stackPanel.Width = IdEtikete.Width;
                stackPanel.Children.Add(popupText);
                UniformGrid buttonStack = new UniformGrid();
                buttonStack.Rows = 1;
                buttonStack.Children.Add(sledeci);
                buttonStack.Children.Add(odustani);

                stackPanel.Children.Add(buttonStack);

                codePopup.Child = stackPanel;

                popupText.Text = (String)IdEtikete.ToolTip;
                IdEtikete.BorderBrush = System.Windows.Media.Brushes.Blue;
                IdEtikete.BorderThickness = new Thickness(3.0);
                codePopup.PlacementTarget = IdEtikete;
                codePopup.Placement = PlacementMode.Top;
                codePopup.IsOpen = true;
                IdEtikete.Focus();
            }
            else
            {
                odustani_Click(null, null);
                popupText.Text = (String)IdEtikete.ToolTip;
                tutorialSlideCounter = 0;
                IdEtikete.BorderBrush = System.Windows.Media.Brushes.Blue;
                IdEtikete.BorderThickness = new Thickness(3.0);
                codePopup.PlacementTarget = IdEtikete;
                codePopup.Placement = PlacementMode.Top;
                codePopup.IsOpen = true;
                IdEtikete.Focus();
            }
        }

        void sledeci_Click(object sender, RoutedEventArgs e)
        {
            codePopup.IsOpen = false;
            ((Control)listaObjekataZaTutorial[tutorialSlideCounter]).BorderBrush = defaultBrush;
            ((Control)listaObjekataZaTutorial[tutorialSlideCounter]).BorderThickness = defaultThickness;

            tutorialSlideCounter = (tutorialSlideCounter + 1) % listaObjekataZaTutorial.Count;

            popupText.Text = (String)((Control)listaObjekataZaTutorial[tutorialSlideCounter]).ToolTip;  //ListaBoja.ToolTip;
            codePopup.PlacementTarget = (Control) listaObjekataZaTutorial[tutorialSlideCounter]; //ListaBoja;
            //codePopup.Placement = PlacementMode.Top;
            ((Control)listaObjekataZaTutorial[tutorialSlideCounter]).BorderBrush = Brushes.Blue;
            ((Control)listaObjekataZaTutorial[tutorialSlideCounter]).BorderThickness = new Thickness(3.0);
            codePopup.IsOpen = true;
            ((Control)listaObjekataZaTutorial[tutorialSlideCounter]).Focus();
        }

        void odustani_Click(object sender, RoutedEventArgs e)
        {
            ((Control)listaObjekataZaTutorial[tutorialSlideCounter]).BorderBrush = defaultBrush;
            ((Control)listaObjekataZaTutorial[tutorialSlideCounter]).BorderThickness = defaultThickness;
            codePopup.IsOpen = false;
            codePopup.StaysOpen = false;
        }

        private void Help_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            ShowHelp help = new ShowHelp("AddEtiketa", this);
            help.Show();
        }

        private void Pomoc_Click(object sender, RoutedEventArgs e)
        {
            ShowHelp help = new ShowHelp("AddEtiketa", this);
            help.Show();
        }

        private void ListaBoja_DropDownClosed(object sender, EventArgs e)
        {
            Window_KeyUp(null, null);
        }
    }
}
