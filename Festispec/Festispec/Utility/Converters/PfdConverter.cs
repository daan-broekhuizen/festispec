using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.IO;
using System.Diagnostics;
using System.Windows.Forms;

namespace Festispec.Utility.Converters
{
    public class PDFConverter
    {
        /// <summary>
        /// Used to export offertes
        /// </summary>
        /// <param name="title"> Title of the document</param>
        /// <param name="documentName">Name of the document when saved</param>
        /// <param name="description">Despription of the offerte</param>
        /// <param name="price"></param>
        /// <param name="customerNumber"></param>
        public void Export(string title, string documentName, string description, string price, string customerNumber = null, bool canChooseCustomLocation = true)
        {
            using (PdfDocument document = new PdfDocument())
            {
                document.Info.Title = "Offertes";

                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                // Title font
                XFont fontTitle = new XFont("Arial", 24, XFontStyle.Bold);
                // Text font
                XFont fontText = new XFont("Arial", 14, XFontStyle.Bold);

                // Title
                gfx.DrawString(title, fontTitle, XBrushes.Black, new XRect(0, 30, page.Width, page.Height), XStringFormats.TopCenter);
                // Price
                gfx.DrawString("Prijs: " + price + "€", fontText, XBrushes.Black, new XRect(50, -40, page.Width, page.Height), XStringFormats.CenterLeft);
                // Description
                gfx.DrawString(description, fontText, XBrushes.Black, new XRect(50, -70, page.Width, page.Height), XStringFormats.CenterLeft);
                // Logo
                DrawImage(gfx, @"..\..\Images/festispec_logo.png", 10, 10, 200, 50);
                // Info
                gfx.DrawString("FestiSpec Inc.", fontText, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormats.BottomLeft);
                gfx.DrawString("1.", fontText, XBrushes.Black, new XRect(0, 0, page.Width, page.Height), XStringFormats.BottomRight);


                if(canChooseCustomLocation)
                {
                    SaveFileDialog saveFileDialog = new SaveFileDialog();
                    saveFileDialog.Title = "Save";
                    saveFileDialog.Filter = "PDF Files (*.pdf) " +"|";
                    if (saveFileDialog.ShowDialog() == DialogResult.OK)
                        documentName = saveFileDialog.FileName;
                }

                if (!documentName.Contains(".pdf"))
                    documentName += ".pdf";

                document.Save(documentName);
                StartPDF(documentName);
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
