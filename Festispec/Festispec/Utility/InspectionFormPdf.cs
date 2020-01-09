using Festispec.Model;
using Festispec.Model.Repositories;
using PdfSharp.Drawing;
using PdfSharp.Drawing.Layout;
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
                List<Vraag> questions = repo.GetQuestionsFromInspector(account.AccountID, jobId);

                int questionAmount = questions.Count;
                int latestQuestion = 0;

                while(questionAmount != latestQuestion)
                    latestQuestion = AddPage(document, repo, account, jobId, questions, latestQuestion);
            }
        }

        private int AddPage(PdfDocument document, RapportageRepository repo, Account account, int jobId, List<Vraag> questions, int latestQuestion)
        {
            PdfPage page = document.AddPage();
            XGraphics gfx = XGraphics.FromPdfPage(page);
            XTextFormatter txtFormatter = new XTextFormatter(gfx);

            // Fonts
            XFont normalFont = new XFont("Arial", 14, XFontStyle.Regular);
            XFont boldFont = new XFont("Arial", 14, XFontStyle.Bold);
            XFont titleFont = new XFont("Arial", 20, XFontStyle.Bold);
            XFont italicFont = new XFont("Arial", 14, XFontStyle.Italic);

            // Inspecteur
            txtFormatter.DrawString($"Inspecteur: {account.Voornaam} {account.Tussenvoegsel} {account.Achternaam}", titleFont, XBrushes.Black, new XRect(20, 20, page.Width, page.Height), XStringFormats.TopLeft);

            // Vragen
            int currentY = 60;

            int currentQuestion = latestQuestion;
            int sizeLeft = 2000;

            while ((currentQuestion < questions.Count && sizeLeft > 200))
            {
                Vraag question = questions[currentQuestion];

                int textHeight = txtFormatter.DrawString($"Vraag {currentQuestion + 1}: {question.Vraagstelling}", boldFont, XBrushes.Black, new XRect(20, currentY, page.Width, page.Height), XStringFormats.TopLeft);

                currentY += textHeight;

                List<Antwoorden> answers = question.Antwoorden.Where(x => x.InspecteurID == account.AccountID).ToList();

                switch (question.Vraagtype)
                {
                    case "tv":
                        currentY = DrawTableQuestionAnswer(gfx, page, answers, question.VraagMogelijkAntwoord.Count, italicFont, currentY);

                        break;
                    case "av":
                        currentY = DrawImageQuestionAnswer(gfx, page, answers, currentY);

                        break;
                    default:
                        currentY = DrawQuestionAnswer(txtFormatter, page, answers, italicFont, currentY);
                        break;
                }

                sizeLeft -= currentY;
                currentQuestion++;
            }

            return currentQuestion;
        }

        private int DrawTableQuestionAnswer(XGraphics gfx, PdfPage page, List<Antwoorden> answers, int columnSize, XFont font, int currentY)
        {
            int cellWidth = 0;

            foreach (Antwoorden answer in answers)
            {
                int width = (int)gfx.MeasureString(answer.AntwoordText, font).Width;
                if (width > cellWidth)
                    cellWidth = width;
            }

            gfx.DrawString("Antwoord:", font, XBrushes.Black, new XRect(20, currentY, page.Width, page.Height), XStringFormats.TopLeft);

            int index = 0;
            for (int y = 0; y < answers.Count / columnSize; y++)
            {
                int currentX = 20;
                currentY += 20;

                for (int x = 0; x < columnSize; x++)
                {
                    Antwoorden answer = answers[index];

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
                        using (XImage xImage = XImage.FromStream(ms))
                        {
                            gfx.DrawImage(xImage, new XRect(20, currentY, xImage.PixelWidth > 200 ? 200 : xImage.PixelWidth, xImage.PixelHeight > 200 ? 200 : xImage.PixelHeight));
                            currentY += xImage.PixelHeight > 200 ? 200 : xImage.PixelHeight;
                        }
                    }
                }

                currentY += 20;
            }

            return currentY + 20;
        }

        private int DrawQuestionAnswer(XTextFormatter txtFormatter, PdfPage page, List<Antwoorden> answers, XFont font, int currentY)
        {
            for (int j = 0; j < answers.Count; j++)
            {
                int height = txtFormatter.DrawString($"Antwoord: {answers[j].AntwoordText}", font, XBrushes.Black, new XRect(20, currentY, page.Width, page.Height), XStringFormats.TopLeft);

                currentY += height;
            }

            return currentY + 20;
        }
    }
}
