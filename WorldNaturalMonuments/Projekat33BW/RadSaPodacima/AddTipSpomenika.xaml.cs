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
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Xml;
using Microsoft.Win32;
using Projekat33BW.Help;
using Projekat33BW.OsnovniPodaci;
using ToastNotifications;
using ToastNotifications.Core;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Position;

namespace Projekat33BW.RadSaPodacima
{
    /// <summary>
    /// Interaction logic for AddTipSpomenika.xaml
    /// </summary>
    public partial class AddTipSpomenika : Window, INotifyPropertyChanged
    {
        private TipSpomenika tipspomenika= new TipSpomenika();

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

        public AddTipSpomenika()
        {
            InitializeComponent();
            DataContext = TipSpomenika;

            PotvrdiDugme.IsEnabled = false;
        }

        private void Close(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void PotvrdiSpomenik_Click(object sender, RoutedEventArgs e)
        {
            IdTipa.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            ImeTipa.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            if (err_count <= 0)
            {
                if (BazaPodataka.GetInstance().ListaTipovaSpomenika.All(x => x.IdTipa != TipSpomenika.IdTipa))
                {

                    BazaPodataka.GetInstance().ListaTipovaSpomenika.Add(TipSpomenika);

                    //MessageBox.Show("Ubačen novi tip spomenika!");
                }
                else
                {
                    MessageBox.Show("Uneseni tip spomenika postoji pod ovim id-em");
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
                bitmap.UriSource  = new Uri(openFileDialog.FileName);
                bitmap.EndInit();
                PutanjaIkone.Source = bitmap;
            }

        }

        private void ProveriIdTipaSpomenika(object sender, RoutedEventArgs e)
        {
            /*
            ObservableCollection<TipSpomenika> listaTipovaSpomenika = BazaPodataka.GetInstance().ListaTipovaSpomenika;
            //ako nisi nasao, potvrdi
            PotvrdiDugme.IsEnabled = true;

            if (IdTipa.Text == null || IdTipa.Text.Equals(""))
            {
                PotvrdiDugme.IsEnabled = false;
                return;
            }


            foreach (TipSpomenika tipSpomenika in listaTipovaSpomenika)
            {
                //TODO pazi da idspomenika bude idspomenika.text kad uvezujes odatle
                if (tipSpomenika.IdTipa.Equals(IdTipa.Text))
                {
                    PotvrdiDugme.IsEnabled = false; //ako nadjes neki, zaustavi ovde
                    return;
                }
            }
            */

        }

        private void IdTipa_KeyUp(object sender, KeyEventArgs e)
        {
            /*
            ObservableCollection<TipSpomenika> listaTipovaSpomenika = BazaPodataka.GetInstance().ListaTipovaSpomenika;
            //ako nisi nasao, potvrdi
            PotvrdiDugme.IsEnabled = true;

            if (IdTipa.Text == null || IdTipa.Text.Equals(""))
            {
                PotvrdiDugme.IsEnabled = false;
                return;
            }


            foreach (TipSpomenika tipSpomenika in listaTipovaSpomenika)
            {
                //TODO pazi da idspomenika bude idspomenika.text kad uvezujes odatle
                if (tipSpomenika.IdTipa.Equals(IdTipa.Text))
                {
                    PotvrdiDugme.IsEnabled = false; //ako nadjes neki, zaustavi ovde
                    return;
                }
            }
            */

        }



        private int err_count = 0;
        private void IdTipa_Error(object sender, ValidationErrorEventArgs e)
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

        private void ImeTipa_Error(object sender, ValidationErrorEventArgs e)
        {
            Console.WriteLine("Jednom se aktiviro1");
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
            if(err_count > 0 || string.IsNullOrEmpty(IdTipa.Text) || string.IsNullOrEmpty(ImeTipa.Text))
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
            listaObjekataZaTutorial.Add(IdTipa);
            listaObjekataZaTutorial.Add(ImeTipa);
            listaObjekataZaTutorial.Add(ImageFinder);
            listaObjekataZaTutorial.Add(OkvirSlike);
            listaObjekataZaTutorial.Add(OpisTipa);
            listaObjekataZaTutorial.Add(PotvrdiDugme);
            listaObjekataZaTutorial.Add(Otkazi);
            listaObjekataZaTutorial.Add(Pomoc);
            listaObjekataZaTutorial.Add(TutorialButton);

            defaultBrush = IdTipa.BorderBrush;
            defaultThickness = IdTipa.BorderThickness;
        }


        private Button sledeci = new Button();
        private Popup codePopup = new Popup();
        //private TextBlock popupText = new TextBlock();
        private StackPanel popupContent = new StackPanel();
        private StackPanel stackPanel = new StackPanel();
        private Brush defaultBrush;
        private Thickness defaultThickness;
        /// <summary>
        /// Broji na kom smo koraku tutoriala
        /// </summary>
        private int tutorialSlideCounter = 0;
        private Boolean tutorialStarted = false;

        private void TutorialButton_Click(object sender, RoutedEventArgs e)
        {
            System.Drawing.Color myColor = System.Drawing.ColorTranslator.FromHtml("#E1DFDA");
            stackPanel.Background = Brushes.LightBlue;//new SolidColorBrush(System.Windows.Media.Color.FromArgb(myColor.A, myColor.R, myColor.G, myColor.B));
            if (tutorialStarted == false)
            {
                TutorialButton.BorderBrush = defaultBrush;
                TutorialButton.BorderThickness = defaultThickness;
                notifier.ClearMessages();

                tutorialStarted = true;
                initTutorial();
                //dugmad potrebna
                Button sledeci = new Button();
                sledeci.Content = "Sledeći korak tutoriala";
                sledeci.IsDefault = true;
                sledeci.Background = Brushes.LightCyan;
                sledeci.AddHandler(Button.ClickEvent, new RoutedEventHandler(sledeci_Click));
                Button odustani = new Button();
                odustani.Content = "Odustani od tutoriala";
                odustani.AddHandler(Button.ClickEvent, new RoutedEventHandler(odustani_Click));
                odustani.Background = Brushes.LightCyan;
                //Sadrzaj teksta i pizzkanje
                //TextBlock popupText = new TextBlock();
                //popupText.TextWrapping = TextWrapping.Wrap;
                // popupText.Text = (String)Dodaj_kartica.ToolTip;
                // popupText.Background = Brushes.LightBlue;
                //popupText.Foreground = Brushes.Blue;

                popupContent.Width = 400;
                //popupContent.Background = defaultBrush;
                UIElement uIElement = cloneElement((UIElement)IdTipa.ToolTip);
                popupContent.Children.Add(uIElement);


                //stackPanel.Width = TabelaEtikete.Width;
                stackPanel.Width = 400;
                //stackPanel.Children.Add(popupText);
                stackPanel.Children.Add(popupContent);
                UniformGrid buttonStack = new UniformGrid();
                buttonStack.Rows = 1;
                buttonStack.Children.Add(sledeci);
                buttonStack.Children.Add(odustani);

                stackPanel.Children.Add(buttonStack);

                codePopup.Child = stackPanel;

                IdTipa.BorderBrush = System.Windows.Media.Brushes.Blue;
                IdTipa.BorderThickness = new Thickness(3.0);
                codePopup.PlacementTarget = IdTipa;
                codePopup.Placement = PlacementMode.Top;
                codePopup.IsOpen = true;
                IdTipa.Focus();
            }
            else
            {

                odustani_Click(null, null);
                //popupText.Text = (String)Dodaj_kartica.ToolTip;
                popupContent.Children.Clear();
                popupContent.Width = 250;
                //popupContent.Background = defaultBrush;
                UIElement uIElement = cloneElement((UIElement)IdTipa.ToolTip);
                popupContent.Children.Add(uIElement);

                tutorialSlideCounter = 0;
                IdTipa.BorderBrush = System.Windows.Media.Brushes.Blue;
                IdTipa.BorderThickness = new Thickness(3.0);
                codePopup.PlacementTarget = IdTipa;
                codePopup.Placement = PlacementMode.Top;
                codePopup.IsOpen = true;
                IdTipa.Focus();
            }
        }

        void sledeci_Click(object sender, RoutedEventArgs e)
        {
            codePopup.IsOpen = false;
            ((Control)listaObjekataZaTutorial[tutorialSlideCounter]).BorderBrush = defaultBrush;
            ((Control)listaObjekataZaTutorial[tutorialSlideCounter]).BorderThickness = defaultThickness;

            tutorialSlideCounter = (tutorialSlideCounter + 1) % listaObjekataZaTutorial.Count;

            if (tutorialSlideCounter == 0)
            {
                odustani_Click(null, null);
                return;
            }
            popupContent.Children.Clear();
            popupContent.Width = 400;
            //popupContent.Background = defaultBrush;
            UIElement uIElement = cloneElement((UIElement)((Control)listaObjekataZaTutorial[tutorialSlideCounter]).ToolTip);
            popupContent.Children.Add(uIElement);
            //popupContent.Children.Add((UIElement)((Control)listaObjekataZaTutorial[tutorialSlideCounter]).ToolTip);
            //popupText.Text = (String)((Control)listaObjekataZaTutorial[tutorialSlideCounter]).ToolTip;  //ListaBoja.ToolTip;
            codePopup.PlacementTarget = (Control)listaObjekataZaTutorial[tutorialSlideCounter]; //ListaBoja;
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
            popupContent.Children.Clear();
        }

        /* * */
        private Notifier notifier;

        private void Window_ContentRendered(object sender, EventArgs e)
        {
            notifier = new Notifier(cfg =>
            {
                cfg.PositionProvider = new WindowPositionProvider(
                    parentWindow: this,
                    corner: Corner.TopRight,
                    offsetX: 10,
                    offsetY: 10);

                cfg.LifetimeSupervisor = new TimeAndCountBasedLifetimeSupervisor(
                    notificationLifetime: TimeSpan.FromSeconds(30),
                    maximumNotificationCount: MaximumNotificationCount.FromCount(5));

                cfg.Dispatcher = Application.Current.Dispatcher;
            });

            MessageOptions opts = new MessageOptions
            {
                CloseClickAction = CloseAction
            };
            if (isTutorialEverOpened == false)
            {
                isTutorialEverOpened = true;
                TutorialButton.BorderBrush = Brushes.Blue;
                TutorialButton.BorderThickness = new Thickness(3.0);
                notifier.ShowInformation("Ovaj prozor ima korak-po-korak tutorial koji prolazi kroz sva dugmad i forme ovog prozora" +
                    ". Možete ga pokrenuti pritiskom na dugme \"Tutorial\"", opts);

            }
        }

        public static Boolean isTutorialEverOpened = false;

        private void CloseAction(NotificationBase obj)
        {
            TutorialButton.BorderThickness = defaultThickness;
            TutorialButton.BorderBrush = defaultBrush;
        }

        public static UIElement cloneElement(UIElement orig)

        {

            if (orig == null)

                return (null);

            string s = XamlWriter.Save(orig);

            StringReader stringReader = new StringReader(s);

            XmlReader xmlReader = XmlTextReader.Create(stringReader, new XmlReaderSettings());

            return (UIElement)XamlReader.Load(xmlReader);

        }
        private void Window_Deactivated(object sender, EventArgs e)
        {
            notifier.ClearMessages();
            TutorialButton.BorderThickness = defaultThickness;
            TutorialButton.BorderBrush = defaultBrush;
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
