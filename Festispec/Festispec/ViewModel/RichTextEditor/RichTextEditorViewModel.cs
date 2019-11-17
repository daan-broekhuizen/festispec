using Festispec.View.RichTextEditor;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using HtmlAgilityPack;
using mshtml;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Interactivity;
using TidyManaged;

namespace Festispec.ViewModel.RichTextEditor
{
    public class RichTextEditorViewModel : ViewModelBase
    {
        // Commands
        public ICommand ModeChangedCommand { get; set; }

        public ICommand ApplyStyleCommand { get; set; }

        public ICommand FontTypeChangedCommand { get; set; }
        public ICommand FontSizeChangedCommand { get; set; }
        public ICommand ApplyAlignmentCommand { get; set; }

        // Properties
        private string _content;

        public string Content
        {
            get => this._content;
            set
            {
                this._content = value;

                RaisePropertyChanged("Content");
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

        public RichTextEditorViewModel()
        {
            this.ModeChangedCommand = new RelayCommand<object[]>(ModeChanged);
            this.ApplyStyleCommand = new RelayCommand<object[]>((parameters) => ApplyStyle((WebBrowser)parameters[0], (string)parameters[1]));
            this.FontTypeChangedCommand = new RelayCommand<object[]>((parameters) => FontTypeChanged((WebBrowser)parameters[0], (string)parameters[1]));
            this.FontSizeChangedCommand = new RelayCommand<object[]>((parameters) => FontSizeChanged((WebBrowser)parameters[0], (string)parameters[1]));
            this.ApplyAlignmentCommand = new RelayCommand<object[]>((parameters) => ApplyAlignment((WebBrowser)parameters[0], (string)parameters[1]));

            this.Content = "<html><body><h1>Header</h1><a href=\"http://www.google.nl\">Google</a><img src=\"https://www.perwez.be/actualites/images-actualites/test.png/@@images/image.png\" alt=\"test\" width=\"100\"/></body></html>";

            this.IsEditable = Visibility.Visible;
        }

        private void ApplyStyle(WebBrowser browser, string style)
        {
            HTMLDocument document = (HTMLDocument)browser.Document;
            if (document != null)
                document.execCommand(style);
        }

        public void FontTypeChanged(WebBrowser browser, string fontType)
        {
            HTMLDocument document = (HTMLDocument)browser.Document;
            if (document != null)
                document.execCommand("FontName", false, fontType);
        }

        public void ApplyAlignment(WebBrowser browser, string alignment)
        {
            HTMLDocument document = (HTMLDocument)browser.Document;
            if (document != null)
                document.execCommand(alignment);
        }

        public void FontSizeChanged(WebBrowser browser, string fontSizeString)
        {
            if (int.TryParse(fontSizeString, out int fontSize))
            {
                HTMLDocument document = (HTMLDocument)browser.Document;
                if (document != null)
                    document.execCommand("FontSize", false, fontSize);
            }
        }

        private void ModeChanged(object[] parameters)
        {
            WebBrowser browser = (WebBrowser)parameters[0];
            TextBox htmlEditor = (TextBox)parameters[1];

            HTMLDocument document = (HTMLDocument)browser.Document;
            if (document != null)
            {
                switch ((int)browser.Tag)
                {
                    case 0:
                        browser.Visibility = Visibility.Visible;
                        htmlEditor.Visibility = Visibility.Hidden;
                        this.IsEditable = Visibility.Collapsed;

                        ((HTMLBody)document.body).contentEditable = "False";

                        break;
                    case 1:
                        browser.Visibility = Visibility.Visible;
                        htmlEditor.Visibility = Visibility.Hidden;
                        this.IsEditable = Visibility.Visible;

                        ((HTMLBody)document.body).contentEditable = "True";

                        break;
                    case 2:
                        browser.Visibility = Visibility.Hidden;
                        htmlEditor.Visibility = Visibility.Visible;
                        this.IsEditable = Visibility.Collapsed;

                        break;
                }

                this.Content = CleanHTML(document.documentElement.outerHTML);
            }
        }

        private string CleanHTML(string input)
        {
            string output = input;

            // Zorgt dat de HTML code netjes wordt.
            using(Document doc = Document.FromString(input))
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
            using(StringWriter writer = new StringWriter())
            {
                HtmlDocument htmlDocument = new HtmlDocument();
                htmlDocument.LoadHtml(output);

                htmlDocument.DocumentNode.SelectSingleNode("//body").Attributes.Remove("contenteditable");

                htmlDocument.Save(writer);

                output = writer.ToString();
            }

            return output;
        }

        public void Save()
        {
            
        }
    }
}
