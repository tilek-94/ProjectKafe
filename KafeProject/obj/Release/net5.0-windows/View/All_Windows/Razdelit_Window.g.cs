﻿#pragma checksum "..\..\..\..\..\View\All_Windows\Razdelit_Window.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "ABE1F64C19408C7F893BE3BD9BC12063A0133E62"
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
using System.Windows.Controls.Ribbon;
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


namespace KafeProject.All_Windows {
    
    
    /// <summary>
    /// Razdelit_Window
    /// </summary>
    public partial class Razdelit_Window : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 50 "..\..\..\..\..\View\All_Windows\Razdelit_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid Datagrid_Spisok;
        
        #line default
        #line hidden
        
        
        #line 67 "..\..\..\..\..\View\All_Windows\Razdelit_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.DataGrid Datagrid_Razdelennyi;
        
        #line default
        #line hidden
        
        
        #line 73 "..\..\..\..\..\View\All_Windows\Razdelit_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Otpravka_Kuxne;
        
        #line default
        #line hidden
        
        
        #line 90 "..\..\..\..\..\View\All_Windows\Razdelit_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Count_Gost;
        
        #line default
        #line hidden
        
        
        #line 102 "..\..\..\..\..\View\All_Windows\Razdelit_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock Count_New_Stol;
        
        #line default
        #line hidden
        
        
        #line 105 "..\..\..\..\..\View\All_Windows\Razdelit_Window.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Button_Razdelit_Gost;
        
        #line default
        #line hidden
        
        private bool _contentLoaded;
        
        /// <summary>
        /// InitializeComponent
        /// </summary>
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.6.0")]
        public void InitializeComponent() {
            if (_contentLoaded) {
                return;
            }
            _contentLoaded = true;
            System.Uri resourceLocater = new System.Uri("/KafeProject;component/view/all_windows/razdelit_window.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\All_Windows\Razdelit_Window.xaml"
            System.Windows.Application.LoadComponent(this, resourceLocater);
            
            #line default
            #line hidden
        }
        
        [System.Diagnostics.DebuggerNonUserCodeAttribute()]
        [System.CodeDom.Compiler.GeneratedCodeAttribute("PresentationBuildTasks", "5.0.6.0")]
        [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Never)]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Design", "CA1033:InterfaceMethodsShouldBeCallableByChildTypes")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity")]
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
        void System.Windows.Markup.IComponentConnector.Connect(int connectionId, object target) {
            switch (connectionId)
            {
            case 1:
            
            #line 7 "..\..\..\..\..\View\All_Windows\Razdelit_Window.xaml"
            ((KafeProject.All_Windows.Razdelit_Window)(target)).Loaded += new System.Windows.RoutedEventHandler(this.Window_Loaded);
            
            #line default
            #line hidden
            return;
            case 2:
            this.Datagrid_Spisok = ((System.Windows.Controls.DataGrid)(target));
            
            #line 50 "..\..\..\..\..\View\All_Windows\Razdelit_Window.xaml"
            this.Datagrid_Spisok.SelectionChanged += new System.Windows.Controls.SelectionChangedEventHandler(this.Datagrid_Spisok_SelectionChanged);
            
            #line default
            #line hidden
            return;
            case 3:
            this.Datagrid_Razdelennyi = ((System.Windows.Controls.DataGrid)(target));
            return;
            case 4:
            this.Otpravka_Kuxne = ((System.Windows.Controls.Button)(target));
            return;
            case 5:
            this.Count_Gost = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 6:
            
            #line 93 "..\..\..\..\..\View\All_Windows\Razdelit_Window.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 7:
            
            #line 96 "..\..\..\..\..\View\All_Windows\Razdelit_Window.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_2);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Count_New_Stol = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 9:
            this.Button_Razdelit_Gost = ((System.Windows.Controls.Button)(target));
            return;
            case 10:
            
            #line 109 "..\..\..\..\..\View\All_Windows\Razdelit_Window.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

