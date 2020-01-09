using GalaSoft.MvvmLight.Messaging;
using System.Windows;
using System.Windows.Controls;

namespace Festispec.View.QuotationView
{
    /// <summary>
    /// Interaction logic for QuotationView.xaml
    /// </summary>
    public partial class ShowQuotationView : Page
    {
        public ShowQuotationView()
        {
            InitializeComponent();
            Messenger.Default.Register<string>(this, DataContext.GetHashCode(), ShowWindow);

        }
        private void ShowWindow(string message) => MessageBox.Show(message, "FestiSpec");

    }
}
