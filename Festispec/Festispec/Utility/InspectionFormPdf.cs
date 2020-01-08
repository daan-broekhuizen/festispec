using Festispec.Model;
using Festispec.Model.Repositories;
using PdfSharp.Drawing;
using PdfSharp.Pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Utility
{
    public class InspectionFormPdf
    {
        public void ExportQuestion(PdfDocument document, RapportageRepository repo, int jobId)
        {
            foreach (Account account in repo.GetInspectorsWithFilledAnswers())
            {
                PdfPage page = document.AddPage();
                XGraphics gfx = XGraphics.FromPdfPage(page);

                // Fonts
                XFont normalFont = new XFont("Arial", 14, XFontStyle.Regular);
                XFont titleFont = new XFont("Arial", 20, XFontStyle.Bold);
                XFont italicFont = new XFont("Arial", 14, XFontStyle.Italic);

                // Inspecteur
                gfx.DrawString($"Inspecteur: {account.Voornaam} {account.Tussenvoegsel} {account.Achternaam}", titleFont, XBrushes.Black, new XRect(20, 20, page.Width, page.Height), XStringFormats.TopLeft);

                // Vragen
                int currentY = 60;
                List<Vraag> questions = repo.GetQuestionsFromInspector(account.AccountID, jobId);

                for (int i = 0; i < questions.Count; i++)
                {
                    Vraag question = questions[i];

                    gfx.DrawString($"Vraag {i + 1}: {question.Vraagstelling}", normalFont, XBrushes.Black, new XRect(20, currentY, page.Width, page.Height), XStringFormats.TopLeft);
                    currentY += 20;

                    List<Antwoorden> answers = question.Antwoorden.Where(x => x.InspecteurID == account.AccountID).ToList();

                    switch(question.Vraagtype)
                    {
                        case "tv":
                            currentY = DrawTableQuestionAnswer(gfx, page, answers, italicFont, currentY);

                            break;
                        case "av":
                            currentY = DrawImageQuestionAnswer(gfx, page, answers, currentY);

                            break;
                        default:
                            currentY = DrawQuestionAnswer(gfx, page, answers, italicFont, currentY);
                            break;
                    }
                }
            }

        }

        private int DrawTableQuestionAnswer(XGraphics gfx, PdfPage page, List<Antwoorden> answers, XFont font, int currentY)
        {
            // Calculate Largest Answer
            int cellWidth = 0;

            foreach (Antwoorden answer in answers)
            {
                int width = (int)gfx.MeasureString(answer.AntwoordText, font).Width;
                if (width > cellWidth)
                    cellWidth = width;
            }

            gfx.DrawString("Antwoord:", font, XBrushes.Black, new XRect(20, currentY, page.Width, page.Height), XStringFormats.TopLeft);

            int square = (int)Math.Sqrt(answers.Count);
            int index = 0;
            for (int y = 0; y < square; y++)
            {
                int currentX = 20;
                currentY += 20;

                Antwoorden answer = answers[index];

                for (int x = 0; x < square; x++)
                {
                    gfx.DrawString(answer.AntwoordText, font, XBrushes.Black, new XRect(currentX, currentY, page.Width, page.Height), XStringFormats.TopLeft);
                    currentX += cellWidth + 20;

                    index++;
                }
            }
            currentY += 40;

            return currentY;
        }

        private int DrawImageQuestionAnswer(XGraphics gfx, PdfPage page, List<Antwoorden> answers, int currentY)
        {
            for (int j = 0; j < answers.Count; j++)
            {
                Antwoorden answer = answers[j];

                if (answer.AntwoordImage != null)
                {
                    using (MemoryStream ms = new MemoryStream(answer.AntwoordImage))
                    {
                        XImage xImage = XImage.FromStream(ms);

                        gfx.DrawImage(xImage, new XRect(20, currentY, xImage.PixelWidth, xImage.PixelHeight));

                        currentY += xImage.PixelHeight;
                    }
                }

                currentY += 20;
            }

            return currentY + 20;
        }

        private int DrawQuestionAnswer(XGraphics gfx, PdfPage page, List<Antwoorden> answers, XFont font, int currentY)
        {
            for (int j = 0; j < answers.Count; j++)
            {
                gfx.DrawString($"Antwoord: {answers[j].AntwoordText}", font, XBrushes.Black, new XRect(20, currentY, page.Width, page.Height), XStringFormats.TopLeft);

                currentY += 20;
            }

            return currentY + 20;
        }
    }
}
