﻿#pragma checksum "..\..\HostConnectionWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "F5D47F984DC532F3EB3264A624F54BE1EE21CE82"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using PlayerApp;
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


namespace PlayerApp {
    
    
    /// <summary>
    /// HostConnectionWindow
    /// </summary>
    public partial class HostConnectionWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 29 "..\..\HostConnectionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox IPInput;
        
        #line default
        #line hidden
        
        
        #line 30 "..\..\HostConnectionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameInput;
        
        #line default
        #line hidden
        
        
        #line 38 "..\..\HostConnectionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ConnectButton;
        
        #line default
        #line hidden
        
        
        #line 46 "..\..\HostConnectionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button BuzzButton;
        
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
            System.Uri resourceLocater = new System.Uri("/PlayerApp;component/hostconnectionwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\HostConnectionWindow.xaml"
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
            
            #line 3 "..\..\HostConnectionWindow.xaml"
            ((PlayerApp.HostConnectionWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.IPInput = ((System.Windows.Controls.TextBox)(target));
            
            #line 29 "..\..\HostConnectionWindow.xaml"
            this.IPInput.GotFocus += new System.Windows.RoutedEventHandler(this.Input_GotFocus);
            
            #line default
            #line hidden
            
            #line 29 "..\..\HostConnectionWindow.xaml"
            this.IPInput.LostFocus += new System.Windows.RoutedEventHandler(this.Input_LostFocus);
            
            #line default
            #line hidden
            return;
            case 3:
            this.NameInput = ((System.Windows.Controls.TextBox)(target));
            
            #line 30 "..\..\HostConnectionWindow.xaml"
            this.NameInput.GotFocus += new System.Windows.RoutedEventHandler(this.Input_GotFocus);
            
            #line default
            #line hidden
            
            #line 30 "..\..\HostConnectionWindow.xaml"
            this.NameInput.LostFocus += new System.Windows.RoutedEventHandler(this.Input_LostFocus);
            
            #line default
            #line hidden
            return;
            case 4:
            this.ConnectButton = ((System.Windows.Controls.Button)(target));
            
            #line 38 "..\..\HostConnectionWindow.xaml"
            this.ConnectButton.Click += new System.Windows.RoutedEventHandler(this.ConnectButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.BuzzButton = ((System.Windows.Controls.Button)(target));
            
            #line 46 "..\..\HostConnectionWindow.xaml"
            this.BuzzButton.Click += new System.Windows.RoutedEventHandler(this.BuzzButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

