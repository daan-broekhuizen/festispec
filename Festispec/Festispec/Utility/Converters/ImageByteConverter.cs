using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Festispec.Utility.Converters
{
    public class ImageByteConverter
    {
        public static ImageSource ByteToImage(byte[] imageData)
        {
            BitmapImage image = new BitmapImage();
            if (imageData == null) return image;
            MemoryStream ms = new MemoryStream(imageData);
            image.BeginInit();
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }

        public static byte[] ImageToBytes(ImageSource image)
        {
            if (image == null) return null;
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            BitmapImage target = image as BitmapImage;
            encoder.Frames.Add(BitmapFrame.Create(target));

            byte[] data;
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }

            return data;
        }
    }
}
