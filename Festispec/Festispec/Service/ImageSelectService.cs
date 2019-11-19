using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Festispec.Service
{
    public class ImageSelectService
    {
        public BitmapImage SelectPngImage()
        {
            OpenFileDialog op = new OpenFileDialog();
            BitmapImage source = new BitmapImage();
            op.Title = "Kies een logo";
            op.Filter = "Portable Network Graphic (*.png)|*.png";
            if (op.ShowDialog() == true)
                source = new BitmapImage(new Uri(op.FileName));
            return source;
        }
        
    }
}
