using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Projekat33BW.OsnovniPodaci
{
    /// <summary>
    /// Singleton klasa sa listama bitnim za spomenike, jednom se instancira
    /// </summary>
    [Serializable]
    public class BazaPodataka : INotifyPropertyChanged
    {

        //TODO Ovde je nesto radjeno
        private ObservableCollection<Spomenik> listaSpomenika = new ObservableCollection<Spomenik>();
        public static TipSpomenika noTypeTip;
        private ObservableCollection<TipSpomenika> listaTipovaSpomenika = new ObservableCollection<TipSpomenika>();
        private ObservableCollection<Etiketa> listaEtiketa = new ObservableCollection<Etiketa>();

        [field:NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
        private void OnPropertyChanged(string v)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }

        private static BazaPodataka instance;
        
        private BazaPodataka()
        {
            listaEtiketa = new ObservableCollection<Etiketa>();
            listaTipovaSpomenika = new ObservableCollection<TipSpomenika>();
            listaSpomenika = new ObservableCollection<Spomenik>();


            //Prazni, notype spomenik, ukoliko se spomenik ne odabere 
            noTypeTip = new TipSpomenika();
            noTypeTip.ImeTipa = "Bez tipa";
            noTypeTip.IdTipa = "null";
            noTypeTip.OpisTipa = "Netipski spomenik";
            noTypeTip.PutanjaIkone = @"C:\Users\Dusan\Documents\1FAKS\HCI\MojProjekat33BW\Projekat33BW\Projekat33BW\Images\qmark.png";

            listaTipovaSpomenika.Add(noTypeTip);
        }

        

        public static BazaPodataka GetInstance()
        {
            
            if (instance != null)
            {
                return instance;
            }
            instance = new BazaPodataka();


            return instance;
        }

        public ObservableCollection<Spomenik> ListaSpomenika
        {
            get => listaSpomenika;
            set
            {
                if (value != listaSpomenika)
                {
                    listaSpomenika = value;
                    OnPropertyChanged("ListaSpomenika");
                }
                    
            }
        }


        public ObservableCollection<TipSpomenika> ListaTipovaSpomenika
        {
          get
            {
                return listaTipovaSpomenika;
            }
          set
            {
                if(listaTipovaSpomenika != value)
                {
                    listaTipovaSpomenika = value;
                    OnPropertyChanged("ListaTipovaSpomenika");
                }
                
            }
        }

        public ObservableCollection<Etiketa> ListaEtiketa
        {
            get => listaEtiketa;
            set
            {
                if(listaEtiketa != value)
                {
                    listaEtiketa = value;
                    OnPropertyChanged("ListaEtiketa");
                }
            }
        }

        /// <summary>
        /// Pokusava da ucita binarnu datoteku kao svoju bazu
        /// </summary>
        public void TryLoad()
        {
            //String path = 
            FileStream fs = new FileStream("../../OsnovniPodaci/BazaPodataka.bin", FileMode.OpenOrCreate);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                if (fs.Length > 0)
                {
                    instance = (BazaPodataka)formatter.Deserialize(fs);
                }
                else
                    instance = null;

                
            }
            catch (SerializationException e)
            {
                //Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                MessageBox.Show("Failed to deserialize. File will be deleted. Reason: " + e.Message);
                //brisem fajl koji sam pokusao otvoriti
                File.Delete("../../OsnovniPodaci/BazaPodataka.bin");
                throw;
            }
            finally
            {
                fs.Close();
            }
            
        }

        public void tryLoad(String putanja)
        {

            //String path = 
            FileStream fs = new FileStream(putanja, FileMode.OpenOrCreate);
            try
            {
                BinaryFormatter formatter = new BinaryFormatter();
                if (fs.Length > 0)
                {
                    instance = (BazaPodataka)formatter.Deserialize(fs);
                }
                else
                    instance = null;


            }
            catch (SerializationException e)
            {
                //Console.WriteLine("Failed to deserialize. Reason: " + e.Message);
                MessageBox.Show("Failed to deserialize. File will be deleted. Reason: " + e.Message);
                //brisem fajl koji sam pokusao otvoriti
                File.Delete(putanja);
                throw;
            }
            finally
            {
                fs.Close();
            }
        }

        

        /// <summary>
        /// Snima instancu baze u binarnom obliku
        /// </summary>
        public void SnimiBazu()
        {
            FileStream fs = new FileStream("../../OsnovniPodaci/BazaPodataka.bin", FileMode.Create);

            // Construct a BinaryFormatter and use it to serialize the data to the stream.
            BinaryFormatter formatter = new BinaryFormatter();
            try
            {
                formatter.Serialize(fs, (BazaPodataka)instance);
            }
            catch (SerializationException e)
            {
                Console.WriteLine("Failed to serialize. Reason: " + e.Message);
                throw;
            }
            finally
            {
                fs.Close();
            }

        }
    }
}
