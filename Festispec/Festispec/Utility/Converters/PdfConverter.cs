
using Festispec.ViewModel.QuotationViewModels;
using GalaSoft.MvvmLight.Messaging;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System.Diagnostics;
using System.IO;
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
        public bool Export(QuotationViewModel vm, string title)
        {
            using (PdfDocument document = new PdfDocument())
            {
                document.Info.Title = "Offertes";
                string documentName = "";
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                // Title font
                XFont fontTitle = new XFont("Arial", 20, XFontStyle.Bold);
                // Text font
                XFont fontText = new XFont("Arial", 14, XFontStyle.Regular);
                XPen lineBlack = new XPen(XColors.Black, 1);

                // Title
                gfx.DrawString(title, fontTitle, XBrushes.Black, new XRect(0, 20, page.Width, page.Height), XStringFormats.TopCenter);
                // Price
                gfx.DrawString("Prijs: " + vm.Price + "€", fontText, XBrushes.Black, new XRect(20, -120, page.Width, page.Height), XStringFormats.BottomLeft);
                gfx.DrawLine(lineBlack, 20, 700, 400, 700);
                // Description
                gfx.DrawString(" Omschrijving: " + vm.Description, fontText, XBrushes.Black, new XRect(20, 140, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawLine(lineBlack, 20, 160, 400, 160);
                // Jobs
                gfx.DrawString(" Opdracht: " + vm.Job, fontText, XBrushes.Black, new XRect(20, 100, page.Width, page.Height), XStringFormats.TopLeft);
                gfx.DrawLine(lineBlack, 20, 120, 400, 120);

                // Logo
                DrawImage(gfx, @"..\..\Images/festispec_logo.png", 20, 20, 100, 25);
                // Info
                gfx.DrawString("FestiSpec.", fontText, XBrushes.Black, new XRect(20, -20, page.Width, page.Height), XStringFormats.BottomLeft);
                gfx.DrawString("1.", fontText, XBrushes.Black, new XRect(-20, -20, page.Width, page.Height), XStringFormats.BottomRight);

                SaveFileDialog saveFileDialog = new SaveFileDialog
                {
                    Title = "Save",
                    Filter = "PDF Files (*.pdf) " + "|"
                };
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    documentName = saveFileDialog.FileName;
                
                if (!documentName.Contains(".pdf"))
                    documentName += ".pdf";

                if (!DidSaveFailed(document, documentName))
                    StartPDF(documentName);
                else
                    return false;

                return true;
            }
        }

        private void StartPDF(string file) => Process.Start(file);

        private void DrawImage(XGraphics gfx, string path, int x, int y, int width, int height)
        {
            XImage image = XImage.FromFile(path);
            gfx.DrawImage(image, x, y, width, height);
        }

        private bool DidSaveFailed(PdfDocument file, string documentName)
        {
            try
            {
                file.Save(documentName);
            }
            catch (IOException)
            { 
                return true;
            }

            return false;
        }
    }
}
