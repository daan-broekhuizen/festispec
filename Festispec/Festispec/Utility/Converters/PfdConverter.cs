using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PdfSharp;
using PdfSharp.Drawing;
using PdfSharp.Pdf;

namespace Festispec.Utility.Converters
{
    public class PDFConverter
    {
        public void Export()
        {
            using (PdfDocument document = new PdfDocument())
            {
                document.Info.Title = "Opdrachten";

                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                XFont font = new XFont("Arial", 12, XFontStyle.Bold);

                gfx.DrawString("FestiSpec opdracht", font, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormat.TopCenter);
                DrawImage(gfx, @"..\..\Images/festispec_logo.png", 10, 10, 200, 50);

                const string file = "Test.pdf";
                
                document.Save(file);
                StartPDF(file);
            }
        }

        public void StartPDF(string file) => Process.Start(file);

        private void DrawImage(XGraphics gfx, string path, int x, int y, int width, int height)
        {
            XImage image = XImage.FromFile(path);
            gfx.DrawImage(image, x, y, width, height);
        }
    }
}
