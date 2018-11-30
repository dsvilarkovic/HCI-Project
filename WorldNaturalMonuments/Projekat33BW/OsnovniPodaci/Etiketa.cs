using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Projekat33BW.OsnovniPodaci
{
    [Serializable]
    public class Etiketa : INotifyPropertyChanged
    {
        /// <summary>
        /// Poziv praznog konstruktora
        /// </summary>
        public Etiketa()
        {
        }


        /// <summary>
        /// Poziv za editovanje ili parametrizovano namestanje konsrutkora
        /// </summary>
        /// <param name="idEtikete"></param>
        /// <param name="bojaEtikete"></param>
        /// <param name="opisEtikete"></param>
        public Etiketa(string idEtikete, string bojaEtikete, string opisEtikete)
        {
            IdEtikete = idEtikete;
            BojaEtikete = bojaEtikete;
            OpisEtikete = opisEtikete;
        }

        public Etiketa(Etiketa etiketa)
        {
            IdEtikete = etiketa.idEtikete;
            BojaEtikete = etiketa.bojaEtikete;
            OpisEtikete = etiketa.opisEtikete;
        }

        
        public static readonly ObservableCollection<String> paletaBoja = 
            new ObservableCollection<String>(new String[] { "Crvena", "Zelena", "Plava", "Zuta", "Ljubicasta", "Bela" });

        public ObservableCollection<String> PaletaBoja { get { return paletaBoja; } }

        public static readonly ObservableCollection<String> paletaBojaFilter =
            new ObservableCollection<String>(new String[] { "", "Crvena", "Zelena", "Plava", "Zuta", "Ljubicasta", "Bela" });

        public ObservableCollection<String> PaletaBojaFilter { get { return paletaBojaFilter; } }

        private String idEtikete;
   

        public String IdEtikete
        {
            get
            {
                return idEtikete;
            }
            set
            {
                if (value != idEtikete)
                {
                    idEtikete = value;
                    OnPropertyChanged("IdEtikete");
                    //Trace.WriteLine("Uneo sam] ispsississsn esto " + value);
                    //TODO provera ispisa, pitati na konsultacijama, ne radi Trace, Debug i Console strimovi
                    //MessageBox.Show(value);
                }
            }
        }

        #region treeViewIspis
        //koristi se za treeview
        public override String ToString()
        {
            return IdEtikete;
        }
        #endregion

        private String bojaEtikete;


        public String BojaEtikete
        {
            get
            {
                return bojaEtikete;
            }
            set
            {
                if (value != bojaEtikete)
                {
                    bojaEtikete = value;
                    OnPropertyChanged("BojaEtikete");
                }
            }
        }
        

        private String opisEtikete;

       

        public String OpisEtikete
        {
            get { return opisEtikete; }
            set
            {
                if (opisEtikete != value)
                {
                    opisEtikete = value;
                    OnPropertyChanged("OpisEtikete");
                }
            }
        }







        private void OnPropertyChanged(string name)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(name));
            }
        }
        [field:NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;

        
    }
}
