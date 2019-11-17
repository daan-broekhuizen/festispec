﻿using System;
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
        public static BitmapImage ByteToImage(byte[] imageData)
        {
            BitmapImage image = new BitmapImage();
            if (imageData == null) return image;
            MemoryStream ms = new MemoryStream(imageData);
            image.BeginInit();
            image.StreamSource = ms;
            image.EndInit();
            return image;
        }

        public static byte[] ImageToBytes(BitmapImage image)
        {
            byte[] data;
            PngBitmapEncoder encoder = new PngBitmapEncoder();
            encoder.Frames.Add(BitmapFrame.Create(image));
            using (MemoryStream ms = new MemoryStream())
            {
                encoder.Save(ms);
                data = ms.ToArray();
            }
            return data;
        }
    }
}
