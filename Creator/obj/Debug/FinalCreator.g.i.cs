﻿#pragma checksum "..\..\FinalCreator.xaml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "C2D40E83650C1FFDEF557641D211F4310B4C6305"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using Creator;
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


namespace Creator {
    
    
    /// <summary>
    /// FinalCreator
    /// </summary>
    public partial class FinalCreator : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 18 "..\..\FinalCreator.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Grid QuestionGrid;
        
        #line default
        #line hidden
        
        
        #line 20 "..\..\FinalCreator.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label QuestionLabel;
        
        #line default
        #line hidden
        
        
        #line 21 "..\..\FinalCreator.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox QuestionTextBox;
        
        #line default
        #line hidden
        
        
        #line 22 "..\..\FinalCreator.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label AnswerLabel;
        
        #line default
        #line hidden
        
        
        #line 23 "..\..\FinalCreator.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox AnswerTextBox;
        
        #line default
        #line hidden
        
        
        #line 24 "..\..\FinalCreator.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Label NameLabel;
        
        #line default
        #line hidden
        
        
        #line 25 "..\..\FinalCreator.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.TextBox NameTextBox;
        
        #line default
        #line hidden
        
        
        #line 26 "..\..\FinalCreator.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button SaveButton;
        
        #line default
        #line hidden
        
        
        #line 27 "..\..\FinalCreator.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ExitButton;
        
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
            System.Uri resourceLocater = new System.Uri("/Creator;component/finalcreator.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\FinalCreator.xaml"
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
            
            #line 5 "..\..\FinalCreator.xaml"
            ((Creator.FinalCreator)(target)).Closing += new System.ComponentModel.CancelEventHandler(this.Window_Closing);
            
            #line default
            #line hidden
            return;
            case 2:
            this.QuestionGrid = ((System.Windows.Controls.Grid)(target));
            return;
            case 3:
            this.QuestionLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 4:
            this.QuestionTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 21 "..\..\FinalCreator.xaml"
            this.QuestionTextBox.LostFocus += new System.Windows.RoutedEventHandler(this.QuestionTextBox_LostFocus);
            
            #line default
            #line hidden
            return;
            case 5:
            this.AnswerLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 6:
            this.AnswerTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 23 "..\..\FinalCreator.xaml"
            this.AnswerTextBox.LostFocus += new System.Windows.RoutedEventHandler(this.AnswerTextBox_LostFocus);
            
            #line default
            #line hidden
            return;
            case 7:
            this.NameLabel = ((System.Windows.Controls.Label)(target));
            return;
            case 8:
            this.NameTextBox = ((System.Windows.Controls.TextBox)(target));
            
            #line 25 "..\..\FinalCreator.xaml"
            this.NameTextBox.LostFocus += new System.Windows.RoutedEventHandler(this.NameTextBox_LostFocus);
            
            #line default
            #line hidden
            return;
            case 9:
            this.SaveButton = ((System.Windows.Controls.Button)(target));
            
            #line 26 "..\..\FinalCreator.xaml"
            this.SaveButton.Click += new System.Windows.RoutedEventHandler(this.SaveButton_Click);
            
            #line default
            #line hidden
            return;
            case 10:
            this.ExitButton = ((System.Windows.Controls.Button)(target));
            
            #line 27 "..\..\FinalCreator.xaml"
            this.ExitButton.Click += new System.Windows.RoutedEventHandler(this.CancelButton_Click);
            
            #line default
            #line hidden
            return;
            }
            this._contentLoaded = true;
        }
    }
}

