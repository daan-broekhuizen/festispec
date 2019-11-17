using mshtml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Festispec.ViewModel.RichTextEditor
{
    public class WebBrowserHelper
    {
        public static readonly DependencyProperty BodyProperty = DependencyProperty.RegisterAttached("Body", typeof(string), typeof(WebBrowserHelper), new PropertyMetadata(OnBodyChanged));

        public static string GetBody(DependencyObject obj)
        {
            return (string)obj.GetValue(BodyProperty);
        }

        public static void SetBody(DependencyObject obj, string value)
        {
            obj.SetValue(BodyProperty, value);
        }

        private static void OnBodyChanged(DependencyObject obj, DependencyPropertyChangedEventArgs e)
        {
            if(LicenseManager.UsageMode == LicenseUsageMode.Runtime)
                ((WebBrowser)obj).NavigateToString((string)e.NewValue);
        }
    }
}
