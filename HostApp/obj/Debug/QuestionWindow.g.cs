﻿#pragma checksum "..\..\QuestionWindow.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "31CB99DCB86173C3FC5D5488BDE85C9F90055E56"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using HostApp;
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


namespace HostApp {
    
    
    /// <summary>
    /// QuestionWindow
    /// </summary>
    public partial class QuestionWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\QuestionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label QuestionLabel;
        
        #line default
        #line hidden
        
        
        #line 45 "..\..\QuestionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.ListBox AnswerQueueBox;
        
        #line default
        #line hidden
        
        
        #line 53 "..\..\QuestionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RightAnswerButton;
        
        #line default
        #line hidden
        
        
        #line 54 "..\..\QuestionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button WrongAnswerButton;
        
        #line default
        #line hidden
        
        
        #line 62 "..\..\QuestionWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SkipButton;
        
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
            System.Uri resourceLocater = new System.Uri("/HostApp;component/questionwindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\QuestionWindow.xaml"
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
            
            #line 5 "..\..\QuestionWindow.xaml"
            ((HostApp.QuestionWindow)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.QuestionLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 3:
            this.AnswerQueueBox = ((System.Windows.Controls.ListBox)(target));
            return;
            case 4:
            this.RightAnswerButton = ((System.Windows.Controls.Button)(target));
            
            #line 53 "..\..\QuestionWindow.xaml"
            this.RightAnswerButton.Click += new System.Windows.RoutedEventHandler(this.RightAnswerButton_Click);
            
            #line default
            #line hidden
            return;
            case 5:
            this.WrongAnswerButton = ((System.Windows.Controls.Button)(target));
            
            #line 54 "..\..\QuestionWindow.xaml"
            this.WrongAnswerButton.Click += new System.Windows.RoutedEventHandler(this.WrongAnswerButton_Click);
            
            #line default
            #line hidden
            return;
            case 6:
            this.SkipButton = ((System.Windows.Controls.Button)(target));
            
            #line 62 "..\..\QuestionWindow.xaml"
            this.SkipButton.Click += new System.Windows.RoutedEventHandler(this.SkipButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

