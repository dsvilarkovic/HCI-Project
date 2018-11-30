using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;
using System.Windows.Media;

namespace Projekat33BW
{
    public class ColorConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //"Crvena", "Zelena", "Plava", "Zuta", "Ljubicasta", "Bela"
            if (value is String)
            {
                String boja = value as String;

                switch (boja)
                {
                    case "Crvena":
                        //return "Red";
                        return Brushes.Red;
                    case "Zelena":
                        // return "Green";
                        return Brushes.Green;
                    case "Plava":
                        //return "Blue";
                        return Brushes.Blue;
                    case "Zuta":
                        //return "Yellow";
                        return Brushes.Yellow;
                    case "Ljubicasta":
                        //return "Violet";
                        return Brushes.Violet;
                    case "Bela":
                        //return "White";
                        return Brushes.White;
                    case "":
                        return Brushes.White;
                    default:
                        //ako nesto ne radi
                        //return "Black";
                        return Brushes.Black;
                }
            }

            return new NotImplementedException();
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is String)
            {
                String boja = value as String;

                switch (boja)
                {
                    case "Red":
                        return "Crvena";
                    case "Green":
                        return "Zelena";
                    case "Blue":
                        return "Plava";
                    case "Yellow":
                        return "Zuta";
                    case "Violet":
                        return "Ljubicasta";
                    case "White":
                        return "Bela";
                    default:
                        //ako nesto ne radi

                        return "Crna";
                      
                }
            }

            return new NotImplementedException();

        }

        internal static Color ConvertFromString(string v)
        {
            throw new NotImplementedException();
        }
    }
}
