﻿using Festispec.Service;
using GalaSoft.MvvmLight.Messaging;
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

namespace Festispec.View.CustomerView
{
    /// <summary>
    /// Interaction logic for ContactPersonView.xaml
    /// </summary>
    public partial class ContactPersonList : Page
    {
        public ContactPersonList()
        {
            InitializeComponent();
            Messenger.Default.Register<string>(this, DataContext.GetHashCode(), ShowWindow);
        }

        private void Button_Click(object sender, RoutedEventArgs e) => CustomerLogo.Source = new ImageSelectService().SelectPngImage();

        private void ShowWindow(string message) => MessageBox.Show(message, "FestiSpec");
    }
}
