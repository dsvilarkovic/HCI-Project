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
    public class TipSpomenika : INotifyPropertyChanged
    {
        public TipSpomenika()
        {
            PutanjaIkone = "../../Images/qmark.png";
        }
        public TipSpomenika(string idTipa, string imeTipa, string putanjaIkone, string opisTipa, ObservableCollection<Spomenik> listaSpomenikaOvogTipa)
        {
            IdTipa = idTipa;
            ImeTipa = imeTipa;
            PutanjaIkone = putanjaIkone;
            OpisTipa = opisTipa;
            ListaSpomenikaOvogTipa = listaSpomenikaOvogTipa;
        }

        public TipSpomenika(TipSpomenika tipSpomenika)
        {
            IdTipa = tipSpomenika.idTipa;
            ImeTipa = tipSpomenika.imeTipa;
            PutanjaIkone = tipSpomenika.putanjaIkone;
            OpisTipa = tipSpomenika.opisTipa;
            ListaSpomenikaOvogTipa = tipSpomenika.listaSpomenikaOvogTipa;
        }

        #region treeViewIspis
        //koristi se za treeview
        public override String ToString()
        {
            return ImeTipa;
        }
        #endregion

        private String idTipa;

        public String IdTipa
        {
            get { return idTipa; }
            set {
                if (idTipa != value)
                {
                    idTipa = value;
                    OnPropertyChanged("IdTipa");
                }
            }
        }

        private String imeTipa;

        public String ImeTipa
        {
            get { return imeTipa; }
            set
            {
                if (imeTipa != value)
                {
                    imeTipa = value;
                    OnPropertyChanged("ImeTipa");
                }
            }
        }

        private String putanjaIkone;

        public String PutanjaIkone
        {
            get
            {
                return putanjaIkone;
            }
            set
            {
                if (putanjaIkone != value)
                {
                    putanjaIkone = value;
                    OnPropertyChanged("PutanjaIkone");
                }
            }
        }

        private String opisTipa;

        

        public String OpisTipa
        {
            get { return opisTipa; }
            set
            {
                if (opisTipa != value)
                {
                    opisTipa = value;
                    OnPropertyChanged("OpisTipa");
                }
            }
        }

        private ObservableCollection<Spomenik> listaSpomenikaOvogTipa = new ObservableCollection<Spomenik>();

        public ObservableCollection<Spomenik> ListaSpomenikaOvogTipa {

            get
            {
                return listaSpomenikaOvogTipa;
            }

            set
            {
                if(value != listaSpomenikaOvogTipa)
                {
                    value = listaSpomenikaOvogTipa;
                    OnPropertyChanged("ListaSpomenikaOvogTipa");
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
        [field: NonSerialized]
        public event PropertyChangedEventHandler PropertyChanged;
    }
}
