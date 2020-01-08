﻿using Festispec.Service;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
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

namespace Festispec.View
{
    /// <summary>
    /// Interaction logic for AddContactPersonView.xaml
    /// </summary>
    public partial class AddContactPersonView : Page
    {
        public AddContactPersonView()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e) => CustomerLogo.Source = new ImageSelectService().SelectPngImage();

    }
}
