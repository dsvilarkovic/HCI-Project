﻿#pragma checksum "..\..\..\RadSaPodacima\AddSpomenik.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "360C2EAECFF8B64C2DC72255B3607E0D0530DFAF"
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


namespace Projekat33BW.RadSaPodacima {
    
    
    /// <summary>
    /// AddSpomenik
    /// </summary>
    public partial class AddSpomenik : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 16 "..\..\..\RadSaPodacima\AddSpomenik.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image PutanjaIkone;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\..\RadSaPodacima\AddSpomenik.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox IdOznaka;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\RadSaPodacima\AddSpomenik.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox ImeSpomenika;
        
        #line default
        #line hidden
        
        
        #line 32 "..\..\..\RadSaPodacima\AddSpomenik.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox OpisSpomenika;
        
        #line default
        #line hidden
        
        
        #line 33 "..\..\..\RadSaPodacima\AddSpomenik.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox TipSpomenika;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\RadSaPodacima\AddSpomenik.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox KlimaSpomenika;
        
        #line default
        #line hidden
        
        
        #line 39 "..\..\..\RadSaPodacima\AddSpomenik.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ComboBox TuristickiStatus;
        
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
            System.Uri resourceLocater = new System.Uri("/Projekat33BW;component/radsapodacima/addspomenik.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\RadSaPodacima\AddSpomenik.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "4.0.0.0")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal System.Delegate _CreateDelegate(System.Type delegateType, string handler) {
            return System.Delegate.CreateDelegate(delegateType, this, handler);
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
            this.PutanjaIkone = ((System.Windows.Controls.Image)(target));
            return;
            case 2:
            this.IdOznaka = ((System.Windows.Controls.TextBox)(target));
            return;
            case 3:
            this.ImeSpomenika = ((System.Windows.Controls.TextBox)(target));
            return;
            case 4:
            this.OpisSpomenika = ((System.Windows.Controls.TextBox)(target));
            return;
            case 5:
            this.TipSpomenika = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 6:
            this.KlimaSpomenika = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 7:
            this.TuristickiStatus = ((System.Windows.Controls.ComboBox)(target));
            return;
            case 8:
            
            #line 41 "..\..\..\RadSaPodacima\AddSpomenik.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.PotvrdiSpomenik_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 42 "..\..\..\RadSaPodacima\AddSpomenik.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Close_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 43 "..\..\..\RadSaPodacima\AddSpomenik.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.NadjiIkonu_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

