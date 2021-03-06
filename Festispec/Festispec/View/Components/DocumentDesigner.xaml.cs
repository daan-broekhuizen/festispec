﻿using Festispec.ViewModel.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Festispec.View.Components
{
    /// <summary>
    /// Interaction logic for DocumentDesigner.xaml
    /// </summary>
    public partial class DocumentDesigner : UserControl
    {
        public DocumentDesignerViewModel ViewModel
        {
            get
            {
                DocumentDesignerViewModel viewModel = (DocumentDesignerViewModel)Resources["ViewModel"];

                viewModel.Create(webBrowser, htmlEditor);
                viewModel.ContentChanged += (sender, content) => this.DesignerContent = content;

                return viewModel;
            }
        }

        // Content
        public static readonly DependencyProperty DesignerContentProperty = DependencyProperty.Register("DesignerContent", typeof(string), typeof(DocumentDesigner), new FrameworkPropertyMetadata(default(string), FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, OnDesignerContentChanged));

        [Description("The Designer Content")]
        public string DesignerContent
        {
            get => (string)GetValue(DesignerContentProperty);
            set => SetValue(DesignerContentProperty, value);
        }

        private static void OnDesignerContentChanged(DependencyObject obj, DependencyPropertyChangedEventArgs args)
        {
            if (LicenseManager.UsageMode == LicenseUsageMode.Runtime && !string.IsNullOrEmpty((string)args.NewValue))
                ((DocumentDesigner)obj).ViewModel.WebBrowser.NavigateToString((string)args.NewValue);
            else if (string.IsNullOrEmpty((string)args.NewValue))
                ((DocumentDesigner)obj).ViewModel.WebBrowser.NavigateToString("<html><body></body></html>");

            ((DocumentDesigner)obj).ViewModel.DesignerContent = (string)args.NewValue;
        }

        public DocumentDesigner()
        {
            InitializeComponent();
        }
    }
}
