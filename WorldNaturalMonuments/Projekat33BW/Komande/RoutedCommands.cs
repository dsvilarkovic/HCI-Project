using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Projekat33BW.Komande
{
    public static class RoutedCommands
    {
        public static readonly RoutedUICommand AddEtiketa = new RoutedUICommand(
            "Dodaj etiketu",
            "AddEtiketa",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.E, ModifierKeys.Control)
            }
            );

        public static readonly RoutedUICommand AddTipSpomenika = new RoutedUICommand(
            "Dodaj tip spomenika",
            "AddTipSpomenika",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.T, ModifierKeys.Control)
            }
            );

        public static readonly RoutedUICommand AddSpomenik= new RoutedUICommand(
            "Dodaj spomenik",
            "AddSpomenik",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.S, ModifierKeys.Control)
            }
            );

        public static readonly RoutedUICommand PrikazSpomenika = new RoutedUICommand(
            "Prikaz spomenika",
            "PrikazSpomenika",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                //new KeyGesture(Key.P, ModifierKeys.Control)
                new KeyGesture(Key.S, ModifierKeys.Alt)
            }
            );

        public static readonly RoutedUICommand TabelaTipova = new RoutedUICommand(
            "Tabela tipova",
            "TabelaTipova",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                //new KeyGesture(Key.P, ModifierKeys.Control)
                new KeyGesture(Key.T, ModifierKeys.Alt)
            }
            );

        public static readonly RoutedUICommand TabelaEtiketa = new RoutedUICommand(
            "Tabela etiketa",
            "TabelaEtiketa",
            typeof(RoutedCommands),
            new InputGestureCollection()
            {
                new KeyGesture(Key.E, ModifierKeys.Alt)
            }
            );
        
    }

}
