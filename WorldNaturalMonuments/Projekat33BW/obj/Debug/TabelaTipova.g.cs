﻿#pragma checksum "..\..\TabelaTipova.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "10D4A0F3DD2810529BB054224C5B9FCB1FFA8409"
//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

using Projekat33BW;
using Projekat33BW.Komande;
using Projekat33BW.Validation;
using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Markup;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Effects;
using System.Windows.Media.Imaging;
using System.Windows.Media.Media3D;
using System.Windows.Media.TextFormatting;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Shell;


namespace Projekat33BW {
    
    
    /// <summary>
    /// TabelaTipova
    /// </summary>
    public partial class TabelaTipova : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 15 "..\..\TabelaTipova.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Input.CommandBinding Help;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\TabelaTipova.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid TabelaTipa;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\TabelaTipova.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Izmeni;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\TabelaTipova.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Obrisi;
        
        #line default
        #line hidden
        
        
        #line 63 "..\..\TabelaTipova.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Cancel;
        
        #line default
        #line hidden
        
        
        #line 84 "..\..\TabelaTipova.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox SearchBox;
        
        #line default
        #line hidden
        
        
        #line 93 "..\..\TabelaTipova.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Filtriraj;
        
        #line default
        #line hidden
        
        
        #line 101 "..\..\TabelaTipova.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button OtkaziFilter;
        
        #line default
        #line hidden
        
        
        #line 110 "..\..\TabelaTipova.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox CaseInSensitive;
        
        #line default
        #line hidden
        
        
        #line 119 "..\..\TabelaTipova.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.CheckBox Obelezi;
        
        #line default
        #line hidden
        
        
        #line 132 "..\..\TabelaTipova.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Pomoc;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/Projekat33BW;component/tabelatipova.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\TabelaTipova.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 11 "..\..\TabelaTipova.xaml"
            ((Projekat33BW.TabelaTipova)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            
            #line 14 "..\..\TabelaTipova.xaml"
            ((System.Windows.Input.CommandBinding)(target)).Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.AddTipSpomenika_Executed);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Help = ((System.Windows.Input.CommandBinding)(target));
            
            #line 15 "..\..\TabelaTipova.xaml"
            this.Help.Executed += new System.Windows.Input.ExecutedRoutedEventHandler(this.Help_Executed);
            
            #line default
            #line hidden
            return;
            case 4:
            this.TabelaTipa = ((System.Windows.Controls.DataGrid)(target));
            
            #line 19 "..\..\TabelaTipova.xaml"
            this.TabelaTipa.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.TabelaTipa_MouseUp);
            
            #line default
            #line hidden
            
            #line 19 "..\..\TabelaTipova.xaml"
            this.TabelaTipa.AutoGeneratingColumn += new System.EventHandler<System.Windows.Controls.DataGridAutoGeneratingColumnEventArgs>(this.TipSpomenikaIzmeniPrikaz_AutoGeneratingColumn);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Izmeni = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\TabelaTipova.xaml"
            this.Izmeni.Click += new System.Windows.RoutedEventHandler(this.IzmeniTip_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.Obrisi = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\TabelaTipova.xaml"
            this.Obrisi.Click += new System.Windows.RoutedEventHandler(this.Obrisi_Click);
            
            #line default
            #line hidden
            return;
            case 7:
            this.Cancel = ((System.Windows.Controls.Button)(target));
            
            #line 64 "..\..\TabelaTipova.xaml"
            this.Cancel.Click += new System.Windows.RoutedEventHandler(this.Cancel_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.SearchBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 85 "..\..\TabelaTipova.xaml"
            this.SearchBox.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.SearchBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 9:
            this.Filtriraj = ((System.Windows.Controls.Button)(target));
            
            #line 94 "..\..\TabelaTipova.xaml"
            this.Filtriraj.Click += new System.Windows.RoutedEventHandler(this.Filtriraj_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.OtkaziFilter = ((System.Windows.Controls.Button)(target));
            
            #line 102 "..\..\TabelaTipova.xaml"
            this.OtkaziFilter.Click += new System.Windows.RoutedEventHandler(this.OtkaziFilter_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            this.CaseInSensitive = ((System.Windows.Controls.CheckBox)(target));
            
            #line 111 "..\..\TabelaTipova.xaml"
            this.CaseInSensitive.Click += new System.Windows.RoutedEventHandler(this.CaseInSensitive_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            this.Obelezi = ((System.Windows.Controls.CheckBox)(target));
            
            #line 121 "..\..\TabelaTipova.xaml"
            this.Obelezi.Click += new System.Windows.RoutedEventHandler(this.CheckBox_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            this.Pomoc = ((System.Windows.Controls.Button)(target));
            
            #line 132 "..\..\TabelaTipova.xaml"
            this.Pomoc.Click += new System.Windows.RoutedEventHandler(this.Pomoc_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

