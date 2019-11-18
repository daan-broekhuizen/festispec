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
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;

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
        public ICommand AddImageCommand { get; set; }
        public ICommand TestCommand { get; set; }

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

                RaisePropertyChanged("IsEditable");
            }
        }

        public RichTextEditorViewModel()
        {
            this.ModeChangedCommand = new RelayCommand<object[]>((parameters) => ChangeMode((DocumentDesigner)parameters[0], (int)parameters[1]));
            this.ApplyStyleCommand = new RelayCommand<object[]>((parameters) => ((DocumentDesigner)parameters[0]).ViewModel.ApplyStyle((string)parameters[1]));
            this.FontTypeChangedCommand = new RelayCommand<object[]>((parameters) => ((DocumentDesigner)parameters[0]).ViewModel.ApplyFontType((string)parameters[1]));
            this.FontSizeChangedCommand = new RelayCommand<object[]>((parameters) => ((DocumentDesigner)parameters[0]).ViewModel.ApplyFontSize((string)parameters[1]));
            this.ApplyAlignmentCommand = new RelayCommand<object[]>((parameters) => ((DocumentDesigner)parameters[0]).ViewModel.ApplyAlignment((string)parameters[1]));
            this.AddImageCommand = new RelayCommand<object[]>((parameters) => ((DocumentDesigner)parameters[0]).ViewModel.AddImage((string)parameters[1]));
            this.TestCommand = new RelayCommand<object[]>((parameters) => ((DocumentDesigner)parameters[0]).ViewModel.Test());
            this.Content = "<html><body><h1>Header</h1><a href=\"http://www.google.nl\">Google</a><img src=\"https://www.perwez.be/actualites/images-actualites/test.png/@@images/image.png\" alt=\"test\" width=\"100\"/></body></html>";

            this.IsEditable = Visibility.Collapsed;
        }

        public void ChangeMode(DocumentDesigner designer, int selectedMode)
        {
            designer.ViewModel.ChangeMode(selectedMode);

            if (selectedMode == 1)
                this.IsEditable = Visibility.Visible;
            else
                this.IsEditable = Visibility.Collapsed;
        }

        public void Save()
        {
            
        }
    }
}
