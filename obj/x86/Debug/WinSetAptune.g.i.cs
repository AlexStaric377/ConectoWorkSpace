﻿#pragma checksum "..\..\..\WinSetAptune.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "B0F332597D310C5F25FCD0E645D644197D4F9DCA33D6A82CB3C69CA5C4A6ABA5"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using ConectoWorkSpace.Administrator;
using System;
using System.Collections;
using System.Diagnostics;
using System.Windows;
using System.Windows.Automation;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms.Integration;
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


namespace ConectoWorkSpace {
    
    
    /// <summary>
    /// WinSetAptune
    /// </summary>
    public partial class WinSetAptune : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 10 "..\..\..\WinSetAptune.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ConectoWorkSpace.WinSetAptune WinSetAptuneW;
        
        #line default
        #line hidden
        
        
        #line 17 "..\..\..\WinSetAptune.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid WinGrid;
        
        #line default
        #line hidden
        
        
        #line 18 "..\..\..\WinSetAptune.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label NameTable;
        
        #line default
        #line hidden
        
        
        #line 19 "..\..\..\WinSetAptune.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid TablAptune;
        
        #line default
        #line hidden
        
        
        #line 28 "..\..\..\WinSetAptune.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Close_Grid;
        
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
            System.Uri resourceLocater = new System.Uri("/Conecto® WorkSpace;component/winsetaptune.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\WinSetAptune.xaml"
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
            this.WinSetAptuneW = ((ConectoWorkSpace.WinSetAptune)(target));
            
            #line 11 "..\..\..\WinSetAptune.xaml"
            this.WinSetAptuneW.KeyDown += new System.Windows.Input.KeyEventHandler(this.ConectoW_KeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.WinGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.NameTable = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.TablAptune = ((System.Windows.Controls.DataGrid)(target));
            
            #line 19 "..\..\..\WinSetAptune.xaml"
            this.TablAptune.Loaded += new System.Windows.RoutedEventHandler(this.DateGrid25_Loaded);
            
            #line default
            #line hidden
            
            #line 19 "..\..\..\WinSetAptune.xaml"
            this.TablAptune.MouseUp += new System.Windows.Input.MouseButtonEventHandler(this.grid25_MouseUp);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Close_Grid = ((System.Windows.Controls.Image)(target));
            
            #line 28 "..\..\..\WinSetAptune.xaml"
            this.Close_Grid.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Close_Grid_MouseLeftButtonDown);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

