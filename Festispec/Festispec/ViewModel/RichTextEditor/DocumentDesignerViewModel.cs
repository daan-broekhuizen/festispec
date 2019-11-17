using Festispec.View.RichTextEditor;
using GalaSoft.MvvmLight;
using HtmlAgilityPack;
using mshtml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
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

        public void Create(WebBrowser webBrowser, TextBox htmlEditor)
        {
            this.WebBrowser = webBrowser;
            this.HtmlEditor = htmlEditor;
        }

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

                this.DesignerContent = CleanHTML(document.documentElement.outerHTML);
            }
        }

        private string CleanHTML(string input)
        {
            string output = input;

            // Zorgt dat de HTML code netjes wordt.
            using (Document doc = Document.FromString(input))
            {
                doc.OutputXhtml = true;
                doc.AddTidyMetaElement = false;
                doc.IndentWithTabs = true;
                doc.IndentBlockElements = AutoBool.Auto;
                doc.IndentSpaces = 1;
                doc.AttributeSortType = SortStrategy.Alpha;
                doc.CleanAndRepair();

                output = doc.Save();
            }

            /// Verwijder contenteditable attribuut van body.
            using (StringWriter writer = new StringWriter())
            {
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(output);

                htmlDocument.DocumentNode.SelectSingleNode("//body").Attributes.Remove("contenteditable");

                htmlDocument.Save(writer);

                output = writer.ToString();
            }

            return output;
        }

        public void ApplyStyle(string style)
        {
            ExecuteCommand(style);
        }

        public void ApplyFontType(string fontType)
        {
            ExecuteCommand("FontName", fontType);
        }

        public void ApplyFontSize(string fontSizeString)
        {
            if (int.TryParse(fontSizeString, out int fontSize))
                ExecuteCommand("FontSize", fontSize);
        }

        public void ApplyAlignment(string alignment)
        {
            ExecuteCommand(alignment);
        }

        public void AddImage(string image)
        {
            ExecuteCommand("InsertImage", image);
        }

        public void ExecuteCommand(string command, object value = null)
        {
            HTMLDocument document = (HTMLDocument)WebBrowser.Document;
            if (document != null)
                document.execCommand(command, false, value);
        }
    }
}
