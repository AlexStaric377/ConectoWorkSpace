﻿#pragma checksum "..\..\..\WinMessage.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "1EDC789F9C859874979BB3397E6CEB62F7344AF7E0489AE339A5E26EF3406277"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using System;
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
    /// WinMessage
    /// </summary>
    public partial class WinMessage : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 4 "..\..\..\WinMessage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal ConectoWorkSpace.WinMessage WinMessageW;
        
        #line default
        #line hidden
        
        
        #line 5 "..\..\..\WinMessage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid WinGrid;
        
        #line default
        #line hidden
        
        
        #line 9 "..\..\..\WinMessage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image Close_F;
        
        #line default
        #line hidden
        
        
        #line 10 "..\..\..\WinMessage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grid2;
        
        #line default
        #line hidden
        
        
        #line 15 "..\..\..\WinMessage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid grid1;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\..\WinMessage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock MessageText;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\..\WinMessage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Border WaitGif;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\..\WinMessage.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Image WaitGifIm;
        
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
            System.Uri resourceLocater = new System.Uri("/Conecto® WorkSpace;component/winmessage.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\WinMessage.xaml"
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
            this.WinMessageW = ((ConectoWorkSpace.WinMessage)(target));
            
            #line 4 "..\..\..\WinMessage.xaml"
            this.WinMessageW.KeyDown += new System.Windows.Input.KeyEventHandler(this.ConectoW_KeyDown);
            
            #line default
            #line hidden
            return;
            case 2:
            this.WinGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.Close_F = ((System.Windows.Controls.Image)(target));
            
            #line 9 "..\..\..\WinMessage.xaml"
            this.Close_F.MouseLeftButtonDown += new System.Windows.Input.MouseButtonEventHandler(this.Close_F_MouseLeftButtonDown);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\WinMessage.xaml"
            this.Close_F.MouseMove += new System.Windows.Input.MouseEventHandler(this.Close_F_MouseMove);
            
            #line default
            #line hidden
            
            #line 9 "..\..\..\WinMessage.xaml"
            this.Close_F.MouseLeave += new System.Windows.Input.MouseEventHandler(this.Close_F_MouseLeave);
            
            #line default
            #line hidden
            return;
            case 4:
            this.grid2 = ((System.Windows.Controls.Grid)(target));
            return;
            case 5:
            this.grid1 = ((System.Windows.Controls.Grid)(target));
            return;
            case 6:
            this.MessageText = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 7:
            this.WaitGif = ((System.Windows.Controls.Border)(target));
            return;
            case 8:
            this.WaitGifIm = ((System.Windows.Controls.Image)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}

