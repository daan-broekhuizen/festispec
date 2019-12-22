using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Festispec.Utility.Extensions
{
    public static class FrameworkExtension
    {
        public static byte[] ToByteArray(this FrameworkElement control)
        {
            byte[] data;

            RenderTargetBitmap rtb = new RenderTargetBitmap((int)control.ActualWidth, (int)control.ActualHeight, 96, 96, PixelFormats.Pbgra32);
            rtb.Render(control);

            PngBitmapEncoder png = new PngBitmapEncoder();
            png.Frames.Add(BitmapFrame.Create(rtb));

            using (MemoryStream ms = new MemoryStream())
            {
                png.Save(ms);

                data = ms.ToArray();
            }

            return data;
        }
    }
}
