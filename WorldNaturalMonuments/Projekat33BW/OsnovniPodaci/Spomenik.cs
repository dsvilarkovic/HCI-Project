using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Projekat33BW.OsnovniPodaci
{
  
    [Serializable]
    public class Spomenik : INotifyPropertyChanged
    {
        #region ListaTipovaKlima

        public static readonly ObservableCollection<String> tipoviKlime = new ObservableCollection<String>(new String[]{ "Polarna", "Kontinentalna", "Umereno kontinentalna", "Pustinjska", "Suptropska", "Tropska" });
       

        public ObservableCollection<String> TipoviKlime
        {
            get {
                return tipoviKlime;
            }

        }
        #endregion

        public static readonly ObservableCollection<String> tipoviKlimeFilter = new ObservableCollection<String>(new String[] { "", "Polarna", "Kontinentalna", "Umereno kontinentalna", "Pustinjska", "Suptropska", "Tropska" });


        public ObservableCollection<String> TipoviKlimeFilter
        {
            get
            {
                return tipoviKlimeFilter;
            }
        }
        #region ListaStatusa

        public static readonly ObservableCollection<String> tipoviStatusa = new ObservableCollection<String>(new String[] {"Eksploatisan","Dostupan","Nedostupan" });

        public ObservableCollection<String> TipoviStatusa
        {
            get
            {
                return tipoviStatusa;
            }
        }
        #endregion

        public static readonly ObservableCollection<String> tipoviStatusaFilter = new ObservableCollection<String>(new String[] { "", "Eksploatisan", "Dostupan", "Nedostupan" });

        public ObservableCollection<String> TipoviStatusaFilter
        {
            get
            {
                return tipoviStatusaFilter;
            }
        }

        #region Konstruktor
        public Spomenik()
        {
        }
        public Spomenik(string idSpomenika, string imeSpomenika, string opisSpomenika,
            TipSpomenika tip, String tipKlimeSpomenika, string putanjaIkonice,
            bool isEkoloskiUgrozen, bool myProperty, bool isNaseljen,
            String turistickiStatus, double prihodTurizma, String datumOtkrivanja, Point position, ObservableCollection<Etiketa> listaEtiketa)
        {
            IdSpomenika = idSpomenika;
            ImeSpomenika = imeSpomenika;
            OpisSpomenika = opisSpomenika;
            Tip = tip;
            TipKlimeSpomenika = tipKlimeSpomenika;
            PutanjaIkonice = putanjaIkonice;
            IsEkoloskiUgrozen = isEkoloskiUgrozen;
            IsStanisteUgrozenih = isStanisteUgrozenih;
            IsNaseljen = isNaseljen;
            TuristickiStatus = turistickiStatus;
            PrihodTurizma = prihodTurizma;
            DatumOtkrivanja = datumOtkrivanja;
            Position = position;
            ListaEtiketa = listaEtiketa;
        }

        public Spomenik(Spomenik spomenik)
        {
            IdSpomenika = spomenik.idSpomenika;
            ImeSpomenika = spomenik.imeSpomenika;
            OpisSpomenika = spomenik.opisSpomenika;
            Tip = spomenik.tip;
            TipKlimeSpomenika = spomenik.tipKlimeSpomenika;
            PutanjaIkonice = spomenik.putanjaIkonice;
            IsEkoloskiUgrozen = spomenik.isEkoloskiUgrozen;
            IsStanisteUgrozenih = spomenik.isStanisteUgrozenih;
            IsNaseljen = spomenik.isNaseljen;
            TuristickiStatus = spomenik.turistickiStatus;
            PrihodTurizma = spomenik.prihodTurizma;
            DatumOtkrivanja = spomenik.datumOtkrivanja;
            Position = spomenik.position;
            ListaEtiketa = new ObservableCollection<Etiketa>(spomenik.listaEtiketa);
        }
        #endregion

        #region Atributi

        private Point position = new Point(-1,-1);

        public Point Position
        {
            get
            {
                return position;
            }

            set
            {
                if(value != position)
                {
                    position = value;
                }
            }

        }
        private String idSpomenika;

        public String IdSpomenika
        {
            get { return idSpomenika; }
            set {
                if (idSpomenika != value)
                {
                    idSpomenika = value;
                    OnPropertyChanged("IdSpomenika");
                    //MessageBox.Show(value);
                }

            }
        }

        private String imeSpomenika;

        public String ImeSpomenika
        {
            get { return imeSpomenika; }
            set
            {
                if (imeSpomenika != value)
                {
                    imeSpomenika = value;
                    OnPropertyChanged("ImeSpomenika");
                }
            }
        }

        private String opisSpomenika;

        public String OpisSpomenika
        {
            get { return opisSpomenika; }
            set
            {
                if(opisSpomenika != value)
                {
                    opisSpomenika = value;
                    OnPropertyChanged("OpisSpomenika");
                }
            }
        }

        private TipSpomenika tip = BazaPodataka.noTypeTip;

        public TipSpomenika Tip
        {
            get { return tip; }
            set
            {
                if(tip != value)
                {
                    tip = value;
                    OnPropertyChanged("Tip");
                }
                
            }
        }

        private String tipKlimeSpomenika;

        public String TipKlimeSpomenika
        {
            get { return tipKlimeSpomenika; }
            set
            {
                if(tipKlimeSpomenika != value)
                {
                    tipKlimeSpomenika = value;
                    OnPropertyChanged("TipKlimeSpomenika");
                }                
            }
        }

        private String putanjaIkonice;

        public String PutanjaIkonice
        {
            get { return putanjaIkonice; }
            set
            {
                if(putanjaIkonice != value)
                {
                    putanjaIkonice = value;
                    OnPropertyChanged("PutanjaIkonice");
                }
                
            }
        }

        private bool isEkoloskiUgrozen = false;

        public bool IsEkoloskiUgrozen
        {
            get { return isEkoloskiUgrozen; }
            set
            {
                if (isEkoloskiUgrozen != value)
                {
                    isEkoloskiUgrozen = value;
                    OnPropertyChanged("IsEkoloskiUgrozen");
                }

            }
        }

        private bool isStanisteUgrozenih = false;

        public bool IsStanisteUgrozenih
        {
            get { return isStanisteUgrozenih; }
            set
            {
                if (isStanisteUgrozenih != value)
                {
                    isStanisteUgrozenih = value;
                    OnPropertyChanged("IsStanisteUgrozenih");
                }

            }
        }

        private bool isNaseljen = false;

        public bool IsNaseljen
        {
            get { return isNaseljen; }
            set
            {
                if (isNaseljen != value)
                {
                    isNaseljen = value;
                    OnPropertyChanged("IsNaseljen");
                }

            }
        }

        private String turistickiStatus;

        public String TuristickiStatus
        {
            get { return turistickiStatus; }
            set
            {
                if (turistickiStatus != value)
                {
                    turistickiStatus = value;
                    OnPropertyChanged("TuristickiStatus");
                }

            }
        }


        private Double prihodTurizma;

        public Double PrihodTurizma
        {
            get { return prihodTurizma; }
            set
            {
                if (prihodTurizma != value)
                {
                    prihodTurizma = value;
                    OnPropertyChanged("PrihodTurizma");
                }

            }
        }

        private String datumOtkrivanja;

        

        public String DatumOtkrivanja
        {
            get { return datumOtkrivanja; }
            set
            {
                if (datumOtkrivanja != value)
                {
                    datumOtkrivanja = value;
                    OnPropertyChanged("DatumOtkrivanja");
                }

            }
        }

        private ObservableCollection<Etiketa> listaEtiketa = new ObservableCollection<Etiketa>();

        public ObservableCollection<Etiketa> ListaEtiketa
        {
            get { return listaEtiketa; }
            set
            {
                if (listaEtiketa != value)
                {
                    listaEtiketa = value;
                    OnPropertyChanged("ListaEtiketa");
                }

            }
        }
        #endregion


        #region treeViewIspis
        //koristi se za treeview
        public override String ToString()
        {
            return ImeSpomenika;
        }
        #endregion



        private void OnPropertyChanged(string v)
        {
            if(PropertyChanged != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(v));
            }
        }
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
