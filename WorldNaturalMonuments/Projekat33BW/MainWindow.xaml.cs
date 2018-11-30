using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Microsoft.Win32;
using System.ComponentModel;
using Projekat33BW.RadSaPodacima;
using Projekat33BW.OsnovniPodaci;
using System.Collections.ObjectModel;
using System.IO;
using WPFCustomMessageBox;
using ToastNotifications;
using ToastNotifications.Lifetime;
using ToastNotifications.Position;
using ToastNotifications.Messages;
using Projekat33BW.Help;
using Projekat33BW.Utils;
using System.Windows.Controls.Primitives;
using ToastNotifications.Core;
using System.Windows.Markup;
using System.Xml;

namespace Projekat33BW
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window, INotifyPropertyChanged
    {

        //ObservableCollection<TipSpomenika> listaTipovaSpomenika = BazaPodataka.GetInstance().ListaTipovaSpomenika;
        ObservableCollection<TipSpomenika> ListaTipovaSpomenika
        {
            get => BazaPodataka.GetInstance().ListaTipovaSpomenika;
            /*
            set
            {
                if(value != listaTipovaSpomenika)
                {
                    listaTipovaSpomenika = value;
                    OnPropertyChanged("ListaTipovaSpomenika");
                }
            }
            */
        }
        public MainWindow()
        {          
            BazaPodataka.GetInstance().TryLoad();
            InitializeComponent();
            //listaTipovaSpomenika = BazaPodataka.GetInstance().ListaTipovaSpomenika;
            DataContext = BazaPodataka.GetInstance();        
           

            initCanvas();
            ToolTipService.ShowDurationProperty.OverrideMetadata(
    typeof(DependencyObject), new FrameworkPropertyMetadata(Int32.MaxValue));
        }

        public static readonly int ICON_SIZE = 40;
        public static Boolean isTutorialEverOpened = false;

        public void initCanvas()
        {
            ObservableCollection<Spomenik> listaSpomenika = BazaPodataka.GetInstance().ListaSpomenika;
            
            Canvas.Children.Clear();
            
            
            foreach (Spomenik spomenik in listaSpomenika)
            {
                if (spomenik.Position != null && (spomenik.Position.X != -1 && spomenik.Position.Y != -1) && spomenik.PutanjaIkonice != null)
                {
                    BitmapImage spomenikBitmap = new BitmapImage(new Uri(spomenik.PutanjaIkonice, UriKind.RelativeOrAbsolute));
                    Image spomenikImage = new Image();
                    spomenikImage.Width = ICON_SIZE;
                    spomenikImage.Height = ICON_SIZE;
                    spomenikImage.Source = spomenikBitmap;
                    spomenikImage.AllowDrop = false;

                    StackPanel stackPanel = new StackPanel();
                    TextBlock imeSpomenikaOsnovni = new TextBlock();
                    TextBox imeSpomenika = new TextBox();
                    TextBlock opisSpomenikOsnovni = new TextBlock();
                    TextBox opisSpomenika = new TextBox();
                    TextBlock tipSpomenikaOsnovni = new TextBlock();
                    TextBox tipSpomenika = new TextBox();
                    TextBlock moreInformation = new TextBlock();
                    imeSpomenikaOsnovni.Text = "Ime spomenika: ";
                    imeSpomenika.Text =  spomenik.ImeSpomenika;
                    opisSpomenikOsnovni.Text = "Opis:";
                    opisSpomenika.Text = spomenik.OpisSpomenika;
                    opisSpomenika.Background = Brushes.White;
                    tipSpomenikaOsnovni.Text = "Tip spomenika: ";
                    tipSpomenika.Text = spomenik.Tip.IdTipa;
                    moreInformation.Text = "Za više informacija o ovom spomeniku klikni dva puta levim \n tasterom miša na sliku da uđeš u režim za menjanje spomenika.";
                    moreInformation.Foreground = Brushes.Blue;

                    //StackPanel etiketaStack = new StackPanel();
                    //etiketaStack.Orientation = Orientation.Horizontal;

                    TextBlock tekstOsnovni = new TextBlock();
                    TextBox tekst = new TextBox();                    
                    EtiketeConverter etiketeConverter = new EtiketeConverter();
                    tekstOsnovni.Text = "Etikete:";
                    tekst.Text =  (String)etiketeConverter.Convert(spomenik.ListaEtiketa, null,null,null);

                    Image copy =(Image) cloneElement(spomenikImage);
                    copy.Width = 100;
                    copy.Height = 100;
                    stackPanel.Children.Add(copy);
                    stackPanel.Children.Add(moreInformation);
                    stackPanel.Children.Add(imeSpomenikaOsnovni);
                    stackPanel.Children.Add(imeSpomenika);
                    stackPanel.Children.Add(tipSpomenikaOsnovni);
                    stackPanel.Children.Add(tipSpomenika);
                    stackPanel.Children.Add(opisSpomenikOsnovni);
                    stackPanel.Children.Add(opisSpomenika);
                    stackPanel.Children.Add(tekstOsnovni);
                    stackPanel.Children.Add(tekst);

                    

                    spomenikImage.ToolTip = stackPanel;

                    Canvas.SetTop(spomenikImage, spomenik.Position.Y);
                    Canvas.SetLeft(spomenikImage, spomenik.Position.X);
                    Canvas.Children.Add(spomenikImage);
                }
            }
        }

        private void AddEtiketa_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AddEtiketa addEtiketa = new AddEtiketa();
            addEtiketa.ShowDialog();
        }
        private void AddTipSpomenika_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AddTipSpomenika addTipSpomenika = new AddTipSpomenika();
            addTipSpomenika.ShowDialog();
        }
        private void AddSpomenik_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            AddSpomenik addSpomenik = new AddSpomenik();
            addSpomenik.ShowDialog();
        }

        private void PrikazSpomenika_Executed(object sender, ExecutedRoutedEventArgs e)

        {
            PrikazSpomenika prikazSpomenika = new PrikazSpomenika();
            prikazSpomenika.ShowDialog();
        }

        private void StabloSpomenika_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TreeView treeView = new TreeView();
            treeView.ShowDialog();
        }

        //prikaz tipova spomenika
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            TabelaTipova tabelaTipova = new TabelaTipova();
            tabelaTipova.ShowDialog();
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            TabelaEtiketa tabelaEtiketa = new TabelaEtiketa();
            tabelaEtiketa.ShowDialog();
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            //TODO pazi imas isto i za gasenje prozora
            //BazaPodataka.GetInstance().SnimiBazu();
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            BazaPodataka.GetInstance().SnimiBazu();
        }


        private Point startPoint;
        private void StabloSpomenika_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            //startno polje koje ce biti ubaceno u spomenik
            startPoint = e.GetPosition(null);
        }

        private void StabloSpomenika_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(null);
            Vector diff = startPoint - mousePos;
            Spomenik spomenik;

            if (e.LeftButton == MouseButtonState.Pressed &&
            (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
            Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                TreeView treeView = sender as TreeView;
                TreeViewItem treeViewItem = FindAncestor<TreeViewItem>((DependencyObject)e.OriginalSource);

                if (treeViewItem != null && treeViewItem.DataContext is Spomenik)
                {
                    spomenik = treeViewItem.DataContext as Spomenik;
                    currentSpomenik = spomenik;
                    DataObject dragData = new DataObject("myFormat", spomenik);
                    DragDrop.DoDragDrop(treeViewItem, dragData, DragDropEffects.Move);
                }                
            }

        }

        // Helper to search up the VisualTree
        private static T FindAncestor<T>(DependencyObject current)
            where T : DependencyObject
        {
            do
            {
                if (current is T)
                {
                    return (T)current;
                }
                current = VisualTreeHelper.GetParent(current);
            }
            while (current != null);
            return null;
        }

        private void Image_DragEnter(object sender, DragEventArgs e)
        {
            if(!e.Data.GetDataPresent("myFormat") || sender == e.Source)
            {
                e.Effects = DragDropEffects.None;
            }
        }


        public Spomenik currentSpomenik;
        private void Image_Drop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("myFormat"))
            {
                Spomenik spomenik = e.Data.GetData("myFormat") as Spomenik;
                //spomenik.Position = e.GetPosition(Canvas);
                Spomenik modelSpomenik = new Spomenik(spomenik);
                modelSpomenik.Position = e.GetPosition(Canvas); 

                    if (spomenikOverlap(spomenik.Position, modelSpomenik.Position) == false)
                    {
                    BitmapImage spomenikBitmap = new BitmapImage(new Uri(spomenik.PutanjaIkonice, UriKind.RelativeOrAbsolute));
                    Image spomenikImage = new Image();
                    spomenikImage.Width = ICON_SIZE;
                    spomenikImage.Height = ICON_SIZE;
                    spomenikImage.Source = spomenikBitmap;
                    
                    spomenik.Position = e.GetPosition(Canvas);
                    Canvas.SetTop(spomenikImage, spomenik.Position.Y);
                    Canvas.SetLeft(spomenikImage, spomenik.Position.X);
                    //((UIElement)spomenikImage).AllowDrop = false;
                    spomenikImage.AllowDrop = false;
                    Canvas.Children.Add(spomenikImage);
                    }
                initCanvas();
            }
            else
            {
                //MessageBox.Show("Pokazi se nekad");
            }
        }

        /// <summary>
        /// Provera da li se preklapa spomenik (newPosition) sa nekim drugim spomenikom, iskljucujuci njega samog (oldPosition)
        /// </summary>
        /// <param name="oldPosition"></param>
        /// <param name="newPosition"></param>
        /// <returns></returns>
        private Boolean spomenikOverlap(Point oldPosition, Point newPosition)
        {
            Rect mySpomenikRectangle = new Rect(newPosition, new Size(ICON_SIZE, ICON_SIZE));
            // mySpomenikRectangle.Location = spomenik.Position;
            //mySpomenikRectangle.Size = new Size(ICON_SIZE, ICON_SIZE);
            foreach (UIElement uiElement in Canvas.Children)
            {

                Rect comparingRectangle = new Rect();

                double top1 = Canvas.GetTop(uiElement);
                double left1 = Canvas.GetLeft(uiElement);

                comparingRectangle.Location = new Point(left1, top1);
                comparingRectangle.Size = new Size(ICON_SIZE, ICON_SIZE);

                //ako se dva pravougaonika presecaju, a da taj sto preseca nije ustv on sam na staroj poziciji
                if (mySpomenikRectangle.IntersectsWith(comparingRectangle) == true && oldPosition != comparingRectangle.Location)
                {
                    //WPFCustomMessageBox.CustomMessageBox.ShowOK("Pokušavate preklopiti dva spomenika, što nije moguće uraditi", "Greška", "U redu");
                    notifier.ShowError("Greška: Pokušavate preklopiti dva spomenika, što nije moguće uraditi");
                    return true;
                }
            }   

            return false;
        }


        //provera da li na kliknutoj poziciji postoji spomenik
        private void Canvas_MouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            Point position = e.GetPosition(Canvas);

            ObservableCollection<Spomenik> listaSpomenika = BazaPodataka.GetInstance().ListaSpomenika;
            if (e.ChangedButton == MouseButton.Left && e.ClickCount == 2)
            {
                UIElement uIElement = (UIElement) e.OriginalSource;
                
                

                double top1 = Canvas.GetTop(uIElement);
                double left1 = Canvas.GetLeft(uIElement);
                foreach (Spomenik spomenik in listaSpomenika)
                {
                    if (spomenik.Position.Y == top1 && spomenik.Position.X == left1)
                    {
                        EditSpomenik editSpomenik = new EditSpomenik(spomenik);
                        editSpomenik.ShowDialog();
                        break;
                    }
                }
            }
        }


        private void Canvas_PreviewMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            startPoint = e.GetPosition(null);
        }

        private void Canvas_PreviewMouseMove(object sender, MouseEventArgs e)
        {
            Point mousePos = e.GetPosition(Canvas);
            Vector diff = startPoint - mousePos;

            if (e.LeftButton == MouseButtonState.Pressed &&
            (Math.Abs(diff.X) > SystemParameters.MinimumHorizontalDragDistance ||
            Math.Abs(diff.Y) > SystemParameters.MinimumVerticalDragDistance))
            {
                UIElement uIElement = (UIElement)e.OriginalSource;
                double top1 = Canvas.GetTop(uIElement);
                double left1 = Canvas.GetLeft(uIElement);
                ObservableCollection<Spomenik> listaSpomenika = BazaPodataka.GetInstance().ListaSpomenika;
                foreach (Spomenik spomenik in listaSpomenika)
                {
                    if (spomenik.Position.Y == top1 && spomenik.Position.X == left1)
                    {
                        currentSpomenik = spomenik; //za ikonicu cursor-a
                        DataObject dragData = new DataObject("myFormat", spomenik);
                        DragDrop.DoDragDrop(Canvas, dragData, DragDropEffects.Move);
                        break;
                    }
                }

            }
        }


        private void Canvas_GiveFeedback(object sender, GiveFeedbackEventArgs e)
        {
            
            if (e.Effects == DragDropEffects.Move) {
                //MessageBox.Show("Alo");

                BitmapImage spomenikBitmap = new BitmapImage();
                spomenikBitmap.BeginInit();
                spomenikBitmap.UriSource = new Uri(currentSpomenik.PutanjaIkonice, UriKind.RelativeOrAbsolute);
                spomenikBitmap.DecodePixelWidth = ICON_SIZE;
                //spomenikBitmap.DecodePixelHeight = ICON_SIZE;
                spomenikBitmap.EndInit();

                //System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(spomenikBitmap.StreamSource);
                System.Drawing.Bitmap bitmap = BitmapImage2Bitmap(spomenikBitmap);
                Cursor cursor = CursorHelper.CreateCursor(bitmap, 0,0 );
                Mouse.SetCursor(cursor);
            }
           
            else
            {
               // MessageBox.Show("A ovde?");
                e.UseDefaultCursors = true;
            }

            e.Handled = true;
        }

        private System.Drawing.Bitmap BitmapImage2Bitmap(BitmapImage bitmapImage)
        {
            // BitmapImage bitmapImage = new BitmapImage(new Uri("../Images/test.png", UriKind.Relative));

            using (MemoryStream outStream = new MemoryStream())
            {
                BitmapEncoder enc = new BmpBitmapEncoder();
                enc.Frames.Add(BitmapFrame.Create(bitmapImage));
                enc.Save(outStream);
                System.Drawing.Bitmap bitmap = new System.Drawing.Bitmap(outStream);

                return new System.Drawing.Bitmap(bitmap);
            }
        }


        private Spomenik odabraniSpomenikStabla;
        private TipSpomenika odabraniTipSpomenikaStabla;
        private void StabloSpomenika_SelectedItemChanged(object sender, RoutedPropertyChangedEventArgs<object> e)
        {

            Object SelectedItem = StabloSpomenika.SelectedItem;
            

            if(SelectedItem is Spomenik){
                odabraniSpomenikStabla = SelectedItem as Spomenik;
                StabloSpomenika.ContextMenu = StabloSpomenika.Resources["SpomenikContext"] as System.Windows.Controls.ContextMenu;
            }
            else if(SelectedItem is TipSpomenika)
            {
                odabraniTipSpomenikaStabla = SelectedItem as TipSpomenika;
                //ispisiSpomenikeTipa(odabraniTipSpomenikaStabla);
                if (odabraniTipSpomenikaStabla.IdTipa.Equals("null"))
                {
                    StabloSpomenika.ContextMenu = StabloSpomenika.Resources["NoTypeContext"] as System.Windows.Controls.ContextMenu;
                    //StabloSpomenika.ContextMenu = null;
                }
                else
                {
                    StabloSpomenika.ContextMenu = StabloSpomenika.Resources["TipSpomenikaContext"] as System.Windows.Controls.ContextMenu;
                }
            }
            else
            {
                //StabloSpomenika.ContextMenu = null;
            }
        }

        private void IzmeniSpomenik_Click(object sender, RoutedEventArgs e)
        {
            if (odabraniSpomenikStabla != null)
            {
                EditSpomenik editSpomenik = new EditSpomenik(odabraniSpomenikStabla);
                editSpomenik.ShowDialog();
            }
        }

        private void ObrisiSpomenik_Click(object sender, RoutedEventArgs e)
        {
            //
            if (odabraniSpomenikStabla != null)
            {
                MessageBoxResult result = CustomMessageBox.ShowOKCancel("Da li si siguran da želiš da obrišeš spomenik?", "Upozorenje", "Da", "Odustani");
                if (result == MessageBoxResult.OK)
                {
                    Spomenik spomenikZaBrisanje = odabraniSpomenikStabla;
                    odabraniSpomenikStabla = null;
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
            }
            initCanvas();
        }

        private void IzmeniTipSpomenika_Click(object sender, RoutedEventArgs e)
        {
            if (odabraniTipSpomenikaStabla != null)
            {
                EditTipSpomenika editTipSpomenika = new EditTipSpomenika(odabraniTipSpomenikaStabla);

                editTipSpomenika.ShowDialog();
            }
        }


        public TipSpomenika OdabraniTipSpomenikaStabla
        {
            get
            {
                return odabraniTipSpomenikaStabla;
            }
            set
            {
                if(value != odabraniTipSpomenikaStabla)
                {
                    odabraniTipSpomenikaStabla = value;
                }
            }
        }
        private void DodajSpomenik_Click(object sender, RoutedEventArgs e)
        {
            if (odabraniTipSpomenikaStabla != null)
            {
                Spomenik noviSpomenik = new Spomenik();

                //nadji taj tip spomenika u listi tipova spomenika
                foreach(TipSpomenika trazeniTip in BazaPodataka.GetInstance().ListaTipovaSpomenika)
                {
                    if (trazeniTip.IdTipa.Equals(odabraniTipSpomenikaStabla.IdTipa)){
                        noviSpomenik.Tip = trazeniTip;
                        break;
                    }
                }
                //AddSpomenik addSpomenik = new AddSpomenik(noviSpomenik);
                //addSpomenik.TipSpomenika.SelectedIndex = addSpomenik.TipSpomenika.Items.IndexOf(odabraniTipSpomenikaStabla);

                //addSpomenik.ShowDialog();
                //addSpomenik.TipSpomenika.SelectedIndex = addSpomenik.TipSpomenika.Items.IndexOf(odabraniTipSpomenikaStabla);

                //EditSpomenik editSpomenik = new EditSpomenik(noviSpomenik);
                //editSpomenik.NaslovEditSpomenik.Title = "Novi spomenik";
                //editSpomenik.ShowDialog();
                AddSpomenik addSpomenik = new AddSpomenik(noviSpomenik);
                addSpomenik.ShowDialog();
            }
        }

        private void ObrisiTipSpomenika_Click(object sender, RoutedEventArgs e)
        {
            if (odabraniTipSpomenikaStabla != null)
            {
                TipSpomenika tipSpomenika = odabraniTipSpomenikaStabla;
                //provera da li se pokusava beztipski obrisati
                if (!(tipSpomenika == null || tipSpomenika.IdTipa.Equals("")))
                {
                    //MessageBoxResult result = CustomMessageBox.ShowOKCancel("Da li si siguran da želiš da obrišeš tip? \n To povlači i prebacivanje spomenika u beztipne", "Upozorenje", "Da", "Odustani");
                    MessageBoxResult result = CustomMessageBox.ShowYesNoCancel("\r Da li si siguran da želiš da obrišeš tip? \n Možeš da ostaviš spomenike ovog tipa ili da ih prebaciš u beztipne.", "Upozorenje", "Ostavi spomenike", "Obriši i spomenike", "Odustani");
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
                    else if (result == MessageBoxResult.No)
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
        }

        private int indexTipaSpomenika(TipSpomenika tipSpomenika)
        {
            //MessageBox.Show(tipSpomenika.IdTipa);
            ObservableCollection<TipSpomenika> lista = BazaPodataka.GetInstance().ListaTipovaSpomenika;
            for (int i = 0; i < lista.Count; i++)
            {
                //MessageBox.Show(lista[i].IdTipa);
                if (lista[i].IdTipa.Equals(tipSpomenika.IdTipa))
                {
                    return i;
                }
            }
            return -1;
        }

        private void Window_GotFocus(object sender, RoutedEventArgs e)
        {
            initCanvas();
        }

        /// <summary>
        /// Za otvaranje konteksnog menija na canvasu, postavlja se kao kliknuti
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Canvas_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            UIElement uIElement = (UIElement)e.OriginalSource;
            
            double top1 = Canvas.GetTop(uIElement);
            double left1 = Canvas.GetLeft(uIElement);
            ObservableCollection<Spomenik> listaSpomenika = BazaPodataka.GetInstance().ListaSpomenika;
            foreach (Spomenik spomenik in listaSpomenika)
            {
                if (spomenik.Position.Y == top1 && spomenik.Position.X == left1)
                {
                    //CustomMessageBox.Show("Alo");
                    odabraniSpomenikCanvasa = spomenik;
                    Canvas.ContextMenu = Canvas.Resources["CanvasSpomenikContext"] as System.Windows.Controls.ContextMenu;
                    return;
                }
            }
            //ako nije nasao nista, postavi ovako
            Canvas.ContextMenu = null;
            
            
        }

        private void CommandBinding_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TabelaTipova tabelaTipova = new TabelaTipova();
            tabelaTipova.ShowDialog();
        }

        private void TabelaEtiketa_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            TabelaEtiketa tabelaEtiketa = new TabelaEtiketa();
            tabelaEtiketa.ShowDialog();
        }

        private void Snimi_datoteku_Click(object sender, RoutedEventArgs e)
        {
            BazaPodataka.GetInstance().SnimiBazu();
        }

        private void Otvori_datoteku_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                String putanja = openFileDialog.FileName;
                BazaPodataka.GetInstance().tryLoad(putanja);

                initCanvas();

                //this.OnPropertyChanged("");
                //Application.Current.Upda
            }
                
        }

        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string v)
        {
            if (PropertyChanged != null)
            {
                //MessageBox.Show("Menjo bracal");
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        public void ispisiSpomenikeTipa(TipSpomenika tipSpomenika)
        {
            ObservableCollection<Spomenik> lista = tipSpomenika.ListaSpomenikaOvogTipa;
            String listaString = "";
            foreach (Spomenik spomenik in lista)
            {
                listaString += " | " + spomenik.ImeSpomenika; 
            }

            MessageBox.Show(listaString);
        }


        private Spomenik odabraniSpomenikCanvasa = null;
        private void IzmeniSpomenikCanvasa_Click(object sender, RoutedEventArgs e)
        {
            if (odabraniSpomenikCanvasa != null)
            {
                EditSpomenik editSpomenik = new EditSpomenik(odabraniSpomenikCanvasa);
                editSpomenik.ShowDialog();
            }

        }

        private void ObrisiSpomenikCanvasa_Click(object sender, RoutedEventArgs e)
        {
            if (odabraniSpomenikCanvasa != null)
            {
                MessageBoxResult result = CustomMessageBox.ShowOKCancel("Da li si siguran da želiš da obrišeš spomenik?", "Upozorenje", "Da", "Odustani");
                if (result == MessageBoxResult.OK)
                {
                    Spomenik spomenikZaBrisanje = odabraniSpomenikCanvasa;
                    odabraniSpomenikCanvasa = null;
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
            }
            initCanvas();
        }

        private void StabloSpomenika_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (StabloSpomenika.SelectedItem != null)
            {
                Object SelectedItem = StabloSpomenika.SelectedItem;


                if (SelectedItem is Spomenik)
                {
                    odabraniSpomenikStabla = SelectedItem as Spomenik;
                    IzmeniSpomenik_Click(null, null);
                }
                else if (SelectedItem is TipSpomenika)
                {
                    odabraniTipSpomenikaStabla = SelectedItem as TipSpomenika;
                    //ispisiSpomenikeTipa(odabraniTipSpomenikaStabla);
                    if (odabraniTipSpomenikaStabla.IdTipa.Equals("null"))
                    {
                    }
                    else
                    {
                        IzmeniTipSpomenika_Click(null, null);
                    }
                }
            }
        }

        private void UkloniSaMape_Click(object sender, RoutedEventArgs e)
        {
            odabraniSpomenikCanvasa.Position = new Point(-1, -1);
            initCanvas();
        }

        


        private void Help_Executed(object sender, ExecutedRoutedEventArgs e)
        {
            IInputElement focusedControl = FocusManager.GetFocusedElement(this);
            if (focusedControl is DependencyObject)
            {
                string str = HelpProvider.GetHelpKey((DependencyObject)focusedControl);
                HelpProvider.ShowHelp(str, this);
            }
            else
            {
                HelpProvider.ShowHelp(GetType().Name, this);
            }
        }

        private void Help_Click(object sender, RoutedEventArgs e)
        {
            ShowHelp help = new ShowHelp("MainWindow", this);
            help.Show();
        }

        private void Pomoc_Click(object sender, RoutedEventArgs e)
        {
            ShowHelp help = new ShowHelp("MainWindow", this);
            help.Show();
        }

        #region tutorial
        public List<Object> listaObjekataZaTutorial = new List<object>();
        public void initTutorial()
        {
            listaObjekataZaTutorial.Add(Dodaj_kartica);
            listaObjekataZaTutorial.Add(PrikaziKartica);
            listaObjekataZaTutorial.Add(Pomoc_kartica);
            listaObjekataZaTutorial.Add(DodajSpomenikToolbar);
            listaObjekataZaTutorial.Add(DodajTipSpomenikaToolbar);
            listaObjekataZaTutorial.Add(DodajEtiketuToolbar);
            listaObjekataZaTutorial.Add(PrikazSpomenikaToolbar);
            listaObjekataZaTutorial.Add(TabelaSpomenikaToolbar);
            listaObjekataZaTutorial.Add(TabelaEtiketaToolbar);
            listaObjekataZaTutorial.Add(Pomoc);
            listaObjekataZaTutorial.Add(TutorialButton);
            listaObjekataZaTutorial.Add(GroupBoxStablo);

            defaultBrush = Dodaj_kartica.BorderBrush;
            defaultThickness = Dodaj_kartica.BorderThickness;
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
                UIElement uIElement = cloneElement((UIElement)Dodaj_kartica.ToolTip);
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

                Dodaj_kartica.BorderBrush = System.Windows.Media.Brushes.Blue;
                Dodaj_kartica.BorderThickness = new Thickness(3.0);
                codePopup.PlacementTarget = Dodaj_kartica;
                codePopup.Placement = PlacementMode.Top;
                codePopup.IsOpen = true;
                Dodaj_kartica.Focus();
            }
            else
            {

                odustani_Click(null, null);
                //popupText.Text = (String)Dodaj_kartica.ToolTip;
                popupContent.Children.Clear();
                popupContent.Width = 250;
                //popupContent.Background = defaultBrush;
                UIElement uIElement = cloneElement((UIElement)Dodaj_kartica.ToolTip);
                popupContent.Children.Add(uIElement);
                
                tutorialSlideCounter = 0;
                Dodaj_kartica.BorderBrush = System.Windows.Media.Brushes.Blue;
                Dodaj_kartica.BorderThickness = new Thickness(3.0);
                codePopup.PlacementTarget = Dodaj_kartica;
                codePopup.Placement = PlacementMode.Top;
                codePopup.IsOpen = true;
                Dodaj_kartica.Focus();
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
        #endregion
        private void Window_Deactivated(object sender, EventArgs e)
        {
            notifier.ClearMessages();
            TutorialButton.BorderThickness = defaultThickness;
            TutorialButton.BorderBrush = defaultBrush;
        }
    }
    
}
