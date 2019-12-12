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
using Microsoft.Maps.MapControl.WPF;

namespace Festispec.View
{
    /// <summary>
    /// Interaction logic for Heatmap.xaml
    /// </summary>
    public partial class Heatmap : Page
    {
        public Heatmap()
        {
            InitializeComponent();
        }


        private void MapItemsControl_MouseEnter(object sender, MouseEventArgs e)
        {
            var TappedPin = (MapItemsControl)sender;
            Pushpin pushpin = (Pushpin)TappedPin.Items.CurrentItem;
            string test = pushpin.Content.ToString();
            TextBlock.Text = test;
        }
    }
}
