﻿#pragma checksum "..\..\..\..\..\View\User_Menu\Oplatit.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "1EC9F873FAFBEFFEF676CE40C3DBB97B4F9F4C46"
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


namespace KafeProject.User_Menu {
    
    
    /// <summary>
    /// Oplatit
    /// </summary>
    public partial class Oplatit : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 17 "..\..\..\..\..\View\User_Menu\Oplatit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBlock K_Oplate;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\..\..\..\View\User_Menu\Oplatit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel Oplatit1;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\..\..\..\View\User_Menu\Oplatit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NalichText;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\..\..\..\View\User_Menu\Oplatit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox CardText;
        
        #line default
        #line hidden
        
        
        #line 31 "..\..\..\..\..\View\User_Menu\Oplatit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox Sdacha;
        
        #line default
        #line hidden
        
        
        #line 34 "..\..\..\..\..\View\User_Menu\Oplatit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.StackPanel Bez_Oplatit;
        
        #line default
        #line hidden
        
        
        #line 47 "..\..\..\..\..\View\User_Menu\Oplatit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Primitives.ToggleButton Zakryt_Oplaty;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\..\..\..\View\User_Menu\Oplatit.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button Otmena;
        
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
            System.Uri resourceLocater = new System.Uri("/KafeProject;component/view/user_menu/oplatit.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\..\..\..\View\User_Menu\Oplatit.xaml"
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
            this.K_Oplate = ((System.Windows.Controls.TextBlock)(target));
            return;
            case 2:
            this.Oplatit1 = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 3:
            this.NalichText = ((System.Windows.Controls.TextBox)(target));
            
            #line 23 "..\..\..\..\..\View\User_Menu\Oplatit.xaml"
            this.NalichText.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.TextBox_MouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 23 "..\..\..\..\..\View\User_Menu\Oplatit.xaml"
            this.NalichText.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 4:
            this.CardText = ((System.Windows.Controls.TextBox)(target));
            
            #line 27 "..\..\..\..\..\View\User_Menu\Oplatit.xaml"
            this.CardText.PreviewMouseDown += new System.Windows.Input.MouseButtonEventHandler(this.TextBox_MouseDoubleClick);
            
            #line default
            #line hidden
            
            #line 27 "..\..\..\..\..\View\User_Menu\Oplatit.xaml"
            this.CardText.TextChanged += new System.Windows.Controls.TextChangedEventHandler(this.TextBox_TextChanged);
            
            #line default
            #line hidden
            return;
            case 5:
            this.Sdacha = ((System.Windows.Controls.TextBox)(target));
            return;
            case 6:
            this.Bez_Oplatit = ((System.Windows.Controls.StackPanel)(target));
            return;
            case 7:
            this.Zakryt_Oplaty = ((System.Windows.Controls.Primitives.ToggleButton)(target));
            
            #line 47 "..\..\..\..\..\View\User_Menu\Oplatit.xaml"
            this.Zakryt_Oplaty.Click += new System.Windows.RoutedEventHandler(this.Zakryt_Oplaty_Click);
            
            #line default
            #line hidden
            return;
            case 8:
            this.Otmena = ((System.Windows.Controls.Button)(target));
            
            #line 55 "..\..\..\..\..\View\User_Menu\Oplatit.xaml"
            this.Otmena.Click += new System.Windows.RoutedEventHandler(this.Otmena_Click);
            
            #line default
            #line hidden
            return;
            case 9:
            
            #line 56 "..\..\..\..\..\View\User_Menu\Oplatit.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Zakryt_Oplaty_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            
            #line 64 "..\..\..\..\..\View\User_Menu\Oplatit.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 11:
            
            #line 65 "..\..\..\..\..\View\User_Menu\Oplatit.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 12:
            
            #line 66 "..\..\..\..\..\View\User_Menu\Oplatit.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 13:
            
            #line 67 "..\..\..\..\..\View\User_Menu\Oplatit.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 14:
            
            #line 68 "..\..\..\..\..\View\User_Menu\Oplatit.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 15:
            
            #line 69 "..\..\..\..\..\View\User_Menu\Oplatit.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 16:
            
            #line 70 "..\..\..\..\..\View\User_Menu\Oplatit.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 17:
            
            #line 71 "..\..\..\..\..\View\User_Menu\Oplatit.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 18:
            
            #line 72 "..\..\..\..\..\View\User_Menu\Oplatit.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 19:
            
            #line 73 "..\..\..\..\..\View\User_Menu\Oplatit.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_1);
            
            #line default
            #line hidden
            return;
            case 20:
            
            #line 74 "..\..\..\..\..\View\User_Menu\Oplatit.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click);
            
            #line default
            #line hidden
            return;
            case 21:
            
            #line 75 "..\..\..\..\..\View\User_Menu\Oplatit.xaml"
            ((System.Windows.Controls.Button)(target)).Click += new System.Windows.RoutedEventHandler(this.Button_Click_2);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}
