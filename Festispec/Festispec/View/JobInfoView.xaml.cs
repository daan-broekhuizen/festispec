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
using Festispec.Model.Repositories;
using GalaSoft.MvvmLight.Messaging;

namespace Festispec.View
{
    /// <summary>
    /// Interaction logic for JobInfoView.xaml
    /// </summary>
    public partial class JobInfoView : Page
    {
        public JobInfoView()
        {
            InitializeComponent();
            Messenger.Default.Register<string>(this, DataContext.GetHashCode(), ShowWindow);
        }

        private void ShowWindow(string message) => MessageBox.Show(message);

    }
}
