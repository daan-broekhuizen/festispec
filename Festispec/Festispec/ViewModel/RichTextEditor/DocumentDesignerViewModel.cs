using Festispec.View.RichTextEditor;
using GalaSoft.MvvmLight;
using HtmlAgilityPack;
using mshtml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using TidyManaged;

namespace Festispec.ViewModel.RichTextEditor
{
    public class DocumentDesignerViewModel : ViewModelBase
    {
        // Events
        public delegate void ContentChangedEventHandler(object sender, string content);

        public event ContentChangedEventHandler ContentChanged;

        // Properties
        private string _designerContent;

        public string DesignerContent
        {
            get => this._designerContent;
            set
            {
                this._designerContent = value;

                if (ContentChanged != null)
                    ContentChanged.Invoke(this, this._designerContent);

                RaisePropertyChanged("DesignerContent");
            }
        }

        private Visibility _isEditable;

        public Visibility IsEditable
        {
            get => this._isEditable;
            set
            {
                this._isEditable = value;

                RaisePropertyChanged();
            }
        }

        public WebBrowser WebBrowser { get; set; }

        public TextBox HtmlEditor { get; set; }

        public int Mode { get; set; }

        public void Create(WebBrowser webBrowser, TextBox htmlEditor)
        {
            this.WebBrowser = webBrowser;
            this.HtmlEditor = htmlEditor;
        }

        /// <summary>
        /// Veranderd de mode van de editor
        /// </summary>
        /// <param name="selectedMode">De nieuwe mode</param>
        public void ChangeMode(int selectedMode)
        {
            HTMLDocument document = (HTMLDocument)WebBrowser.Document;
            if (document != null)
            {
                switch (selectedMode)
                {
                    case 0:
                        this.WebBrowser.Visibility = Visibility.Visible;
                        this.HtmlEditor.Visibility = Visibility.Hidden;
                        this.IsEditable = Visibility.Collapsed;

                        ((HTMLBody)document.body).contentEditable = "False";

                        break;
                    case 1:
                        this.WebBrowser.Visibility = Visibility.Visible;
                        this.HtmlEditor.Visibility = Visibility.Hidden;
                        this.IsEditable = Visibility.Visible;

                        ((HTMLBody)document.body).contentEditable = "True";

                        break;
                    case 2:
                        this.WebBrowser.Visibility = Visibility.Hidden;
                        this.HtmlEditor.Visibility = Visibility.Visible;
                        this.IsEditable = Visibility.Collapsed;

                        break;
                }

                this.DesignerContent = CleanHTML(document.documentElement.outerHTML, selectedMode == 2);

                this.Mode = selectedMode;
            }
        }

        /// <summary>
        /// Maakt de HTML schoon (format)
        /// </summary>
        /// <param name="input">De schoon te maken HTML</param>
        /// <param name="cleanEditable">Verwijdert de contenteditable tag</param>
        /// <returns>De schone HTML</returns>
        private string CleanHTML(string input, bool cleanEditable = false)
        {
            string output = input;

            // Zorgt dat de HTML code netjes wordt.
            using (Document doc = Document.FromString(input))
            {
                doc.OutputXml = true;
                doc.AddTidyMetaElement = false;
                doc.IndentWithTabs = true;
                
                doc.IndentBlockElements = AutoBool.Auto;
                doc.IndentSpaces = 1;
                //doc.MakeClean = true;
                //doc.Quiet = true;

                //doc.AttributeSortType = SortStrategy.Alpha;
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
            output = Regex.Replace(output, @"<(.|\n)*?>", match => match.ToString().ToLower());

            return output;
        }

        /// <summary>
        /// Voegt een style toe aan de selectie en gebruikt deze ook voor nieuwe input.
        /// </summary>
        /// <param name="style">De style</param>
        public void ApplyStyle(string style)
        {
            ExecuteCommand(style);
        }

        /// <summary>
        /// Veranderd het lettertype van de selectie en gebruikt deze ook voor nieuwe input.
        /// </summary>
        /// <param name="fontType">Het letter type</param>
        public void ApplyFontType(string fontType)
        {
            ExecuteCommand("FontName", fontType);
        }

        /// <summary>
        /// Veranderd de lettergrootte van de selectie en gebruikt deze ook voor nieuwe input.
        /// </summary>
        /// <param name="fontSizeString">De lettergrootte (1-7)</param>
        public void ApplyFontSize(string fontSizeString)
        {
            if (int.TryParse(fontSizeString, out int fontSize))
                ExecuteCommand("FontSize", fontSize);
        }

        /// <summary>
        /// Veranderd de positie van de selectie.
        /// </summary>
        /// <param name="alignment">De positie</param>
        public void ApplyAlignment(string alignment)
        {
            ExecuteCommand(alignment);
        }


        /// <summary>
        /// Zorgt ervoor dat elementen vrij bewogen kunnen worden.
        /// </summary>
        public void EnableMovement()
        {
            ExecuteCommand("2D-Position", true);
            ExecuteCommand("AbsolutePosition", true);
        }

        /// <summary>
        /// Voegt een afbeelding toe op de huidige positie
        /// </summary>
        /// <param name="image">De afbeelding</param>
        public void AddImage(string image)
        {
            ExecuteCommand("InsertImage", image);
        }

        public void Test()
        {
            IHTMLDocument2 document = (IHTMLDocument2)WebBrowser.Document;
            if(document != null)
            {
                IHTMLElement element = GetSelectedElement();
                element.style.fontSize = "12px";
            }
        }

        /// <summary>
        /// Haalt het element op wat op dit moment is geselecteerd.
        /// </summary>
        /// <returns>Het geselecteerde element</returns>
        public IHTMLElement GetSelectedElement()
        {
            IHTMLElement element = null;

            IHTMLDocument2 document = (IHTMLDocument2)WebBrowser.Document;

            if (document != null && Mode == 1)
            {
                IHTMLSelectionObject currentSelection = document.selection;

                if (currentSelection.type == "None")
                {
                    IDisplayServices displayServices = (IDisplayServices)document;
                    displayServices.GetCaret(out IHTMLCaret caret);

                    caret.GetLocation(out tagPOINT pt, 1);

                    element = document.elementFromPoint(pt.x, pt.y);
                }
                else if(currentSelection.type == "Text" && currentSelection != null)
                {
                    try
                    {
                        IHTMLTxtRange range = (IHTMLTxtRange)currentSelection.createRange();
                        element = range.parentElement();
                    }
                    catch (Exception) { }

                }
                else if(currentSelection.type == "Control" && currentSelection != null)
                {
                    try
                    {
                        IHTMLControlRange range = (IHTMLControlRange)currentSelection.createRange();
                        element = range.commonParentElement();
                    }
                    catch (Exception) { }
                }
                else
                {
                    // TODO: Weghalen, kan misschien gevaarlijk zijn
                    element = document.body;
                }
            }

            return element;
        }

        /// <summary>
        /// Haalt de huidige selectie op
        /// </summary>
        /// <returns>De huidige selectie</returns>
        public T GetSelectionRange<T>()
        {
            T range = default;
            IHTMLDocument2 document = (IHTMLDocument2)WebBrowser.Document;

            if (document != null && Mode == 1)
            {
                IHTMLSelectionObject currentSelection = document.selection;

                if (currentSelection != null)
                {
                    try
                    {
                        dynamic selection = currentSelection.createRange();
                        if (selection is T)
                            range = (T)selection;
                    }
                    catch (Exception) { }

                }
            }

            return range;
        }

        /// <summary>
        /// Voert een msHTML command uit.
        /// </summary>
        /// <param name="command">Het command</param>
        /// <param name="value">De waarde</param>
        public void ExecuteCommand(string command, object value = null)
        {
            HTMLDocument document = (HTMLDocument)WebBrowser.Document;
            if (document != null)
                document.execCommand(command, false, value);
        }
    }
}
