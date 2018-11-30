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
using Projekat33BW.OsnovniPodaci;
using Projekat33BW.RadSaPodacima;
using WPFCustomMessageBox;
using ToastNotifications;
using Notifications.Wpf;
using System.Windows.Controls.Primitives;
using ToastNotifications.Position;
using ToastNotifications.Lifetime;
using ToastNotifications.Messages;
using ToastNotifications.Core;
using Projekat33BW.Help;
using System.IO;
using System.Windows.Markup;
using System.Xml;

namespace Projekat33BW
{
    /// <summary>
    /// Interaction logic for TabelaEtiketa.xaml
    /// </summary>
    public partial class TabelaEtiketa : Window, INotifyPropertyChanged
    {
        /// <summary>
        /// Za cuvanje da li je ovde otvoren tutorial ikad
        /// </summary>
        public static Boolean isTutorialEverOpened = false;
        private ObservableCollection<Etiketa> listaEtiketa = BazaPodataka.GetInstance().ListaEtiketa;
        public ObservableCollection<Etiketa> ListaEtiketa
        {
            get
            {
                return listaEtiketa;
            }
            set
            {
                if (value != listaEtiketa)
                {
                    listaEtiketa = value;
                    OnPropertyChanged("ListaEtiketa");
                    OnPropertyChanged("IsNonEmpty");
                }
            }
        }

        /// <summary>
        /// Rec po kojoj se trazi
        /// </summary>
        public String searchWord = "";
        public String SearchWord
        {
            get
            {
                return searchWord;
            }
            set
            {
                if(value != searchWord)
                {
                    searchWord = value;
                    OnPropertyChanged("SearchWord");
                }
            }
        }

        public Boolean isNonEmpty = true;
        public Boolean IsNonEmpty
        {
            get
            {
                return (listaEtiketa.Count != 0);
            }

            set
            {
                if (isNonEmpty != (listaEtiketa.Count != 0))
                {
                    isNonEmpty = (listaEtiketa.Count != 0);
                }
            }


        }

        //za pretragu po modeluEtikete 
        public Etiketa modelEtiketa = new Etiketa();
        public Etiketa ModelEtiketa
        {
            get
            {
                return modelEtiketa;
            }
            set
            {
                if(value != modelEtiketa)
                {
                    modelEtiketa = value;
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

        public TabelaEtiketa()
        {
            InitializeComponent();
            DataContext = this;//BazaPodataka.GetInstance();
        }

        /*Sluzi da sakrije kolone koje se ruzno prikazuju*/
        private void TabelaEtiketeKolone_AutoGeneratingColumn(object sender, DataGridAutoGeneratingColumnEventArgs e)
        {
            string headername = e.Column.Header.ToString();
            int i = ((DataGrid)sender).Columns.Count;

            
            if (i - 1 >= 0)
            {
                DataGridColumn column = ((DataGrid)sender).Columns[i - 1];
                //column.Width = new DataGridLength(1, DataGridLengthUnitType.Star);
            }
            
            if (e.PropertyName == "PaletaBoja" || e.PropertyName == "PaletaBojaFilter")
            {
                e.Column = null;
            }
        }

        private void IzmeniEtiketu_Click(object sender, RoutedEventArgs e)
        {
            if (TabelaEtikete.SelectedItem != null)
            {
                Etiketa etiketa= TabelaEtikete.SelectedItem as Etiketa;
                EditEtiketa editEtiketa = new EditEtiketa(etiketa);

                editEtiketa.ShowDialog();
            }
        }

        private void ObrisiEtiketu_Click(object sender, RoutedEventArgs e)
        {
            if (TabelaEtikete.SelectedItem != null)
            {
                MessageBoxResult result = CustomMessageBox.ShowOKCancel("Da li si siguran da želiš da obrišeš etiketu?", "Upozorenje", "Da", "Odustani");
                if (result == MessageBoxResult.OK)
                {

                    Etiketa etiketa = TabelaEtikete.SelectedItem as Etiketa;

                    //udji u bazu i unisti sve ikada pojave etiketa u spomenicima i u listi etiketa
                    BazaPodataka bazaPodataka = BazaPodataka.GetInstance();
                    //prvo u spomenicima
                    foreach (Spomenik spomenik in bazaPodataka.ListaSpomenika)
                    {
                        if (spomenik.ListaEtiketa.Contains(etiketa))
                        {
                            if (spomenik.ListaEtiketa.Remove(etiketa) == false)
                                throw new Exception("Neuspelo brisanje etikete iz spomenika, iako je nadjena");
                        }
                    }

                    //a zatim u listi etiketa baze podataka

                    if (bazaPodataka.ListaEtiketa.Contains(etiketa))
                    {
                        if (bazaPodataka.ListaEtiketa.Remove(etiketa) == false)
                            throw new Exception("Neuspelo brisanje etikete iz baze, iako je nadjena");
                    }

                    //MessageBox.Show("Etiketa uspešno obrisana!");
                }
            }
            //IdEtikete.GetBindingExpression(TextBox.TextProperty).UpdateSource();
            //Izmeni.GetBindingExpression(IsEnabled).UpdateSource();
            if (TabelaEtikete.Items.Count > 0)
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

        private void AddEtiketa_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AddEtiketa addEtiketa = new AddEtiketa();
            addEtiketa.ShowDialog();
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Close();
        }
        

        private void Filter_Click(object sender, RoutedEventArgs e)
        {
            /*
            //ObservableCollection<Etiketa> listaEtiketa = ((BazaPodataka) DataContext).ListaEtiketa;// BazaPodataka.GetInstance().ListaEtiketa;
            ObservableCollection<Etiketa> filterEtiketa = new ObservableCollection<Etiketa>();
            ObservableCollection<Etiketa> listaEtiketa = BazaPodataka.GetInstance().ListaEtiketa;
            foreach ( Etiketa etiketa in listaEtiketa)
            {
                String modelIdEtikete = modelEtiketa.IdEtikete.Trim();
                if (CaseInSensitive.IsChecked == true)
                {
                    //case insensitive
                    modelIdEtikete = modelIdEtikete.ToUpper();

                    //MessageBox.Show(etiketa.IdEtikete.ToUpper().Contains(modelIdEtikete) + "");


                    //MessageBox.Show((modelEtiketa.IdEtikete == null) + " " + etiketa.IdEtikete.ToUpper().Contains(modelIdEtikete) + " " + modelEtiketa.IdEtikete.Equals(""));
                    if (modelEtiketa.IdEtikete == null || etiketa.IdEtikete.ToUpper().Contains(modelIdEtikete) || modelEtiketa.IdEtikete.Equals(""))
                    {
                        //MessageBox.Show("upo");
                        if (modelEtiketa.BojaEtikete == null || modelEtiketa.BojaEtikete.Equals(etiketa.BojaEtikete) || modelEtiketa.BojaEtikete.Equals(""))
                        {
                            //MessageBox.Show("upo opet");
                            filterEtiketa.Add(etiketa);
                        }
                    }

                }
                else {
                    if (modelEtiketa.IdEtikete == null || etiketa.IdEtikete.Contains(modelEtiketa.IdEtikete) || modelEtiketa.IdEtikete.Equals(""))
                    {
                        if (modelEtiketa.BojaEtikete == null || modelEtiketa.BojaEtikete.Equals(etiketa.BojaEtikete) || modelEtiketa.BojaEtikete.Equals(""))
                        {
                            filterEtiketa.Add(etiketa);
                        }
                    }
                }
            }
            
            ListaEtiketa = filterEtiketa;
            Obelezi.IsChecked = false;
            FindControlItem(this.TabelaEtikete);
            //FindControlItem(this.TabelaEtikete);
            */


            //uzmi celu listu

            ObservableCollection<Etiketa> trenutnaListaEtiketa = BazaPodataka.GetInstance().ListaEtiketa;
            //etikea kandidat za novi itemssource
            ObservableCollection<Etiketa> filterEtiketa = new ObservableCollection<Etiketa>();

            Regex regex;
            if (CaseInSensitive.IsChecked.Value == true)
            {
                regex = new Regex("(" + SearchBox.Text + ")", RegexOptions.IgnoreCase | RegexOptions.CultureInvariant);
            }
            else
            {
                regex = new Regex("(" + SearchBox.Text + ")", RegexOptions.CultureInvariant);
            }

            foreach (Etiketa etiketa in trenutnaListaEtiketa)
            {
                if (etiketa.IdEtikete != null && regex.Match(etiketa.IdEtikete).Success)
                {
                    filterEtiketa.Add(etiketa);
                }
                else if (etiketa.BojaEtikete != null && regex.Match(etiketa.BojaEtikete).Success)
                {
                    filterEtiketa.Add(etiketa);
                }
                else if (etiketa.OpisEtikete != null && regex.Match(etiketa.OpisEtikete).Success)
                {
                    filterEtiketa.Add(etiketa);
                }
            }

            Obelezi.IsChecked = false;
            ListaEtiketa = filterEtiketa;

        }

        private void TabelaEtikete_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (TabelaEtikete.Items.Count > 0)
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
        
        private void proveriButtone()
        {
            if (TabelaEtikete.Items.Count > 0)
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

        private void PonistiFilter_Click(object sender, RoutedEventArgs e)
        {
            ListaEtiketa = BazaPodataka.GetInstance().ListaEtiketa;
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
            FindControlItem(this.TabelaEtikete);
            
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
                    if (CaseInSensitive.IsChecked.Value == true) {
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
                FindControlItem(this.TabelaEtikete);
            }
            else
            {
                FindControlItem(this.TabelaEtikete);
            }
        }

        private void CaseInSensitive_Click(object sender, RoutedEventArgs e)
        {
            FindControlItem(this.TabelaEtikete);
        }


        public List<Object> listaObjekataZaTutorial = new List<object>();
        public void initTutorial()
        {
            listaObjekataZaTutorial.Add(this.TabelaEtikete);
            listaObjekataZaTutorial.Add(Dodaj);
            listaObjekataZaTutorial.Add(Izmeni);
            listaObjekataZaTutorial.Add(Obrisi);
            listaObjekataZaTutorial.Add(Cancel);
            listaObjekataZaTutorial.Add(PretragaOkvir);
            listaObjekataZaTutorial.Add(SearchBox);
            listaObjekataZaTutorial.Add(Obelezi);
            listaObjekataZaTutorial.Add(CaseInSensitive);
            listaObjekataZaTutorial.Add(Filter);
            listaObjekataZaTutorial.Add(PonistiFilter);
            listaObjekataZaTutorial.Add(Pomoc);
            listaObjekataZaTutorial.Add(TutorialButton);

            defaultBrush = TabelaEtikete.BorderBrush;
            defaultThickness = TabelaEtikete.BorderThickness;
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

        private void Tutorial_Click(object sender, RoutedEventArgs e)
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
                UIElement uIElement = cloneElement((UIElement)TabelaEtikete.ToolTip);
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

                TabelaEtikete.BorderBrush = System.Windows.Media.Brushes.Blue;
                TabelaEtikete.BorderThickness = new Thickness(3.0);
                codePopup.PlacementTarget = TabelaEtikete;
                codePopup.Placement = PlacementMode.Top;
                codePopup.IsOpen = true;
                TabelaEtikete.Focus();
            }
            else
            {

                odustani_Click(null, null);
                //popupText.Text = (String)Dodaj_kartica.ToolTip;
                popupContent.Children.Clear();
                popupContent.Width = 250;
                //popupContent.Background = defaultBrush;
                UIElement uIElement = cloneElement((UIElement)TabelaEtikete.ToolTip);
                popupContent.Children.Add(uIElement);

                tutorialSlideCounter = 0;
                TabelaEtikete.BorderBrush = System.Windows.Media.Brushes.Blue;
                TabelaEtikete.BorderThickness = new Thickness(3.0);
                codePopup.PlacementTarget = TabelaEtikete;
                codePopup.Placement = PlacementMode.Top;
                codePopup.IsOpen = true;
                TabelaEtikete.Focus();
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
            ShowHelp help = new ShowHelp("TabelaEtiketa", this);
            help.Show();
        }
        
        private void Pomoc_Click(object sender, RoutedEventArgs e)
        {
            ShowHelp help = new ShowHelp("TabelaEtiketa", this);
            help.Show();
        }
    }
}
