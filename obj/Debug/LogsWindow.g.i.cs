﻿#pragma checksum "..\..\LogsWindow.xaml" "{8829d00f-11b8-4213-878b-770e8597ac16}" "35D999C0E254AC3F8D62CCF60AF0CB8F072A82298434B828951DD3E303E812A3"
//------------------------------------------------------------------------------
// <auto-generated>
//     Этот код создан программой.
//     Исполняемая версия:4.0.30319.42000
//
//     Изменения в этом файле могут привести к неправильной работе и будут потеряны в случае
//     повторной генерации кода.
// </auto-generated>
//------------------------------------------------------------------------------

using AssetsIS;
using MaterialDesignThemes.Wpf;
using MaterialDesignThemes.Wpf.Converters;
using MaterialDesignThemes.Wpf.Transitions;
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


namespace AssetsIS {
    
    
    /// <summary>
    /// LogsWindow
    /// </summary>
    public partial class LogsWindow : System.Windows.Window, System.Windows.Markup.IComponentConnector {
        
        
        #line 27 "..\..\LogsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RegistrationLogsButton;
        
        #line default
        #line hidden
        
        
        #line 41 "..\..\LogsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button RequestsLogsButton;
        
        #line default
        #line hidden
        
        
        #line 55 "..\..\LogsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Button ReturnLogsButton;
        
        #line default
        #line hidden
        
        
        #line 64 "..\..\LogsWindow.xaml"
        [System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1823:AvoidUnusedPrivateFields")]
        internal System.Windows.Controls.Frame LogsFrame;
        
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
            System.Uri resourceLocater = new System.Uri("/AssetsIS;component/logswindow.xaml", System.UriKind.Relative);
            
            #line 1 "..\..\LogsWindow.xaml"
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
            this.RegistrationLogsButton = ((System.Windows.Controls.Button)(target));
            
            #line 33 "..\..\LogsWindow.xaml"
            this.RegistrationLogsButton.Click += new System.Windows.RoutedEventHandler(this.RegistrationLogsButton_Click);
            
            #line default
            #line hidden
            return;
            case 2:
            this.RequestsLogsButton = ((System.Windows.Controls.Button)(target));
            
            #line 47 "..\..\LogsWindow.xaml"
            this.RequestsLogsButton.Click += new System.Windows.RoutedEventHandler(this.RequestsLogsButton_Click);
            
            #line default
            #line hidden
            return;
            case 3:
            this.ReturnLogsButton = ((System.Windows.Controls.Button)(target));
            
            #line 61 "..\..\LogsWindow.xaml"
            this.ReturnLogsButton.Click += new System.Windows.RoutedEventHandler(this.ReturnLogsButton_Click);
            
            #line default
            #line hidden
            return;
            case 4:
            this.LogsFrame = ((System.Windows.Controls.Frame)(target));
            return;
            }
            this._contentLoaded = true;
        }
    }
}
