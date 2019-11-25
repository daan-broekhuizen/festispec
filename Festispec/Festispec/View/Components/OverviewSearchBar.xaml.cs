using Festispec.ViewModel.Components;
using GalaSoft.MvvmLight.Command;
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

namespace Festispec.View.Components
{
    /// <summary>
    /// Interaction logic for OverviewSearchBar.xaml
    /// </summary>
    public partial class OverviewSearchBar : UserControl
    {
        public string ButtonText
        {
            get => (string)GetValue(ButtonTextProperty);
            set => SetValue(ButtonTextProperty, value);
        }

        public ICommand ButtonActionCommand
        {
            get => (ICommand)GetValue(ButtonActionCommandProperty);
            set => SetValue(ButtonActionCommandProperty, value);
        }

        public ICommand SearchActionCommand
        {
            get => (ICommand)GetValue(SearchActionCommandProperty);
            set => SetValue(SearchActionCommandProperty, value);
        }

        public ICommand SearchTextChangedCommand
        {
            get => (ICommand)GetValue(SearchTextChangedCommandProperty);
            set => SetValue(SearchTextChangedCommandProperty, value);
        }

        public Visibility IsButtonVisible
        {
            get
            {
                if (string.IsNullOrEmpty(ButtonText))
                    return Visibility.Collapsed;
                else
                    return Visibility.Visible;
            }
        }

        public static readonly DependencyProperty ButtonTextProperty = DependencyProperty.Register("ButtonText", typeof(string), typeof(OverviewSearchBar), new PropertyMetadata(""));
        public static readonly DependencyProperty ButtonActionCommandProperty = DependencyProperty.Register("ButtonActionCommand", typeof(ICommand), typeof(OverviewSearchBar), new UIPropertyMetadata(null));
        public static readonly DependencyProperty SearchActionCommandProperty = DependencyProperty.Register("SearchActionCommand", typeof(ICommand), typeof(OverviewSearchBar), new UIPropertyMetadata(null));
        public static readonly DependencyProperty SearchTextChangedCommandProperty = DependencyProperty.Register("SearchTextChangedCommand", typeof(ICommand), typeof(OverviewSearchBar), new UIPropertyMetadata(null));

        public OverviewSearchBar()
        {
            InitializeComponent();
        }

    }
}
