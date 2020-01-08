using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using TidyManaged;

namespace Festispec.Service
{
    public class WebService
    {
        /// <summary>
        /// Maakt de HTML schoon (format)
        /// </summary>
        /// <param name="input">De schoon te maken HTML</param>
        /// <param name="cleanEditable">Verwijdert de contenteditable tag</param>
        /// <returns>De schone HTML</returns>
        public static string CleanHTML(string input, bool cleanEditable = false)
        {
            string output = input;

            // Zorgt dat de HTML code netjes wordt.
            using (Document doc = Document.FromString(input))
            {
                doc.OutputXml = true;
                doc.ShowWarnings = false;
                doc.AddTidyMetaElement = false;
                doc.IndentWithTabs = true;

                doc.IndentBlockElements = AutoBool.Auto;
                doc.IndentSpaces = 1;

                doc.CleanAndRepair();

                output = doc.Save();
            }

            /// Verwijder contenteditable attribuut van body.
            using (StringWriter writer = new StringWriter())
            {
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(output);

                if (cleanEditable)
                    htmlDocument.DocumentNode.SelectSingleNode("//body").Attributes.Remove("contenteditable");

                htmlDocument.Save(writer);

                output = writer.ToString();
            }

            // Maakt alle attributes lower case
            //output = Regex.Replace(output, @"<(.|\n)*?>", match => match.ToString().ToLower());

            return output;
        }
    }
}