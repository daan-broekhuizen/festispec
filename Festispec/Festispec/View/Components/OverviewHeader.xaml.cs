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
    /// Interaction logic for OverviewHeader.xaml
    /// </summary>
    public partial class OverviewHeader : UserControl
    {
        public string PageName
        {
            get => (string)GetValue(PageNameProperty);
            set => SetValue(PageNameProperty, value);
        }

        public static readonly DependencyProperty PageNameProperty = DependencyProperty.Register("PageName", typeof(string), typeof(OverviewHeader), new PropertyMetadata("Titel"));

        public OverviewHeader()
        {
            InitializeComponent();
        }
    }
}
