﻿using Festispec.Model;
using Festispec.Utility.Converters;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace Festispec.ViewModel.InspectionFormViewModels
{
    public class QuestionViewModel : ViewModelBase
    {
        private Vraag _question;
        private int _InspectionFormID;
        public bool Changed;
        public bool Created;
        public int BottomValue;
        public int TopValue;
        public int ScaleSize;

        public QuestionViewModel(Vraag v, Inspectieformulier inspec)
        {
            _question = v;
            InspectionFormID = inspec.InspectieformulierID;
            Changed = false;
            AddPossibleAnwsers();
        }



        public int QuestionID
        {
            get => _question.VraagID;
            set
            {
                _question.VraagID = value;
                Changed = true;
                RaisePropertyChanged("QuestionID");
            }
        }

        public int InspectionFormID
        {
            get => _question.InspectieFormulierID;
            set
            {
                _question.InspectieFormulierID = value;
            }
        }

        public Vraag Question
        {
            get => _question;
        }

        public string QuestionText
        {
            get => _question.Vraagstelling;
            set
            {
                _question.Vraagstelling = value;
                Changed = true;
                RaisePropertyChanged("QuestionText");
            }
        }

        public string QuestionType
        {
            get => _question.Vraagtype;
            set
            {
                _question.Vraagtype = value;
                Changed = true;
                RaisePropertyChanged("QuestionType");
            }
        }

        public byte[] Image
        {
            get => _question.Bijlage;
            set
            {
                _question.Bijlage = value;
                Changed = true;
                RaisePropertyChanged("Image");
            }
        }

        public int OrderNumber
        {
            get => _question.VolgordeNummer;
            set
            {
                _question.VolgordeNummer = value;
                Changed = true;
                RaisePropertyChanged("OrderNumber");
            }
        }

        public ImageSource Bijlage
        {
            get => ImageByteConverter.BytesToImage(_question.Bijlage);
            set
            {
                byte[] image = ImageByteConverter.PngImageToBytes(value);
                if (image != null)
                    _question.Bijlage = image;
                else
                    new BitmapImage(new Uri(@"pack://application:,,,/Images/addImageIcon"));
                RaisePropertyChanged("Bijlage");

            }
        }

        public void AddPossibleAnwsers()
        {
            if (_question.Vraagtype == "sv")
            {
                for (int i = 1; i < 6; i++)
                {
                    _question.VraagMogelijkAntwoord.Add(new VraagMogelijkAntwoord
                    {
                        VraagID = _question.VraagID,
                        AntwoordNummer = i,
                        AntwoordText = (i).ToString()
                    });
                }

                BottomValue = 0;
                TopValue = 5;
                ScaleSize = 5;
            }

            if(_question.Vraagtype == "mv")
            {
                for (int i = 1; i < 5; i++)
                {
                    _question.VraagMogelijkAntwoord.Add(new VraagMogelijkAntwoord
                    {
                        VraagID = _question.VraagID,
                        AntwoordNummer = i,
                        AntwoordText = (i).ToString()
                    });
                }
            }
        }
    }
}
