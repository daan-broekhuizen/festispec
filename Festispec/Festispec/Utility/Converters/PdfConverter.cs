﻿
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
        public void Export(string title, string description, string price)
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

                // Title
                gfx.DrawString(title, fontTitle, XBrushes.Black, new XRect(0, 20, page.Width, page.Height), XStringFormats.TopCenter);
                // Price
                gfx.DrawString("Prijs: " + price + "€", fontText, XBrushes.Black, new XRect(20, -80, page.Width, page.Height), XStringFormats.BottomLeft);
                // Description
                gfx.DrawString("Offerte omschrijving:  \n\n" + description, fontText, XBrushes.Black, new XRect(20, 80, page.Width, page.Height), XStringFormats.TopLeft);
                // Logo
                DrawImage(gfx, @"..\..\Images/festispec_logo.png", 20, 20, 100, 25);
                // Info
                gfx.DrawString("FestiSpec.", fontText, XBrushes.Black, new XRect(20, -20, page.Width, page.Height), XStringFormats.BottomLeft);
                gfx.DrawString("1.", fontText, XBrushes.Black, new XRect(-20, -20, page.Width, page.Height), XStringFormats.BottomRight);

                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.Title = "Save";
                saveFileDialog.Filter = "PDF Files (*.pdf) " + "|";
                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                    documentName = saveFileDialog.FileName;
                
                if (!documentName.Contains(".pdf"))
                    documentName += ".pdf";

                document.Save(documentName);
                StartPDF(documentName);
            }
        }

        private void StartPDF(string file) => Process.Start(file);

        private void DrawImage(XGraphics gfx, string path, int x, int y, int width, int height)
        {
            XImage image = XImage.FromFile(path);
            gfx.DrawImage(image, x, y, width, height);
        }
    }
}