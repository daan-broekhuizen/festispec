
using Festispec.ViewModel.QuotationViewModels;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Diagnostics;
using System.IO;
using System.Text.RegularExpressions;
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
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);
                // Title font
                XFont fontTitle = new XFont("Arial", 20, XFontStyle.Bold);
                // Text font
                XFont fontText = new XFont("Arial", 14, XFontStyle.Regular);
                XPen lineBlack = new XPen(XColors.Black, 0.3);

                PdfPage currentPage = page;

                document.Info.Title = "Offertes";
                string documentName = "";
                string[] lines = null;
                int y = 140, lineY = 160, linesOnPage = 0, pageCount = 1;

                DrawBase(gfx, currentPage, fontTitle, fontText, title, pageCount);


                // Jobs
                gfx.DrawString(" Opdracht: " + vm.Job, fontText, XBrushes.Black, new XRect(20, 100, currentPage.Width, currentPage.Height), XStringFormats.TopLeft);
                gfx.DrawLine(lineBlack, 20, 120, 400, 120);

                if (vm.Description.Length > 80)
                {
                    lines = GetLines(vm.Description);

                    for (int i = 0; i < lines.Length; i++)
                    {
                        gfx.DrawString(lines[i], fontText, XBrushes.Black, new XRect(20, y, currentPage.Width, currentPage.Height), XStringFormats.TopLeft);
                        gfx.DrawLine(lineBlack, 20, lineY, 550, lineY);
                        lineY += 40;
                        y += 40;
                        linesOnPage++;
                        if (linesOnPage == 17)
                        {
                            PdfPage newPage = document.AddPage();
                            currentPage = newPage;
                            gfx = XGraphics.FromPdfPage(currentPage);
                            linesOnPage = 0;
                            pageCount++;
                            y = 140;
                            lineY = 160;
                            DrawBase(gfx, currentPage, fontTitle, fontText, title, pageCount);

                        }
                    }
                }
                else
                {
                    gfx.DrawString(vm.Description, fontText, XBrushes.Black, new XRect(20, y, currentPage.Width, currentPage.Height), XStringFormats.TopLeft);
                    gfx.DrawLine(lineBlack, 20, 160, 550, 160);
                }

                // Price
                gfx.DrawString("Prijs: " + vm.Price + "€", fontText, XBrushes.Black, new XRect(20, -60, currentPage.Width, currentPage.Height), XStringFormats.BottomLeft);
                gfx.DrawLine(lineBlack, 20, 760, 400, 760);

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

        private void DrawBase(XGraphics gfx, PdfPage page, XFont fontTitle,  XFont fontText, string title, int pageCount)
        {
            // Title
            gfx.DrawString(title, fontTitle, XBrushes.Black, new XRect(0, 20, page.Width, page.Height), XStringFormats.TopCenter);
            // Logo
            DrawImage(gfx, @"..\..\Images/festispec_logo.png", 20, 20, 100, 25);
            // Info
            gfx.DrawString("FestiSpec.", fontText, XBrushes.Black, new XRect(20, -20, page.Width, page.Height), XStringFormats.BottomLeft);
            gfx.DrawString(pageCount.ToString(), fontText, XBrushes.Black, new XRect(-20, -20, page.Width, page.Height), XStringFormats.BottomRight);
        }

        private void StartPDF(string file) => Process.Start(file);

        private void DrawImage(XGraphics gfx, string path, int x, int y, int width, int height)
        {
            XImage image = XImage.FromFile(path);
            gfx.DrawImage(image, x, y, width, height);
        }

        private string[] GetLines(string arg)
        {
            int chars = arg.Length;
            string tmp = arg;

            for (int i = 0; i < tmp.Length; i++)
                tmp = Regex.Replace(tmp, @"\t|\n|\r", " ");

            string[] lines;
            if (Between(chars, 80, 160))
                lines = new string[2];
            else
                lines = new string[(chars + 80) / 80];


            for (int i = 0; i < lines.Length; i++)
            {
                if (tmp.Length < 80)
                    lines[i] = tmp.Substring(0, tmp.Length);
                else
                {
                    lines[i] = tmp.Substring(0, 80);
                    tmp = tmp.Remove(0, 80);
                }   
            }

            return lines;
        }

        public bool Between(int num, int min, int max) => min < num && num < max;

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
