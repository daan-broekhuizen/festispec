using Festispec.API.ImageShack;
using Festispec.Model;
using Festispec.Service;
using Festispec.Utility.Converters;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using FluentValidation.Results;
using Festispec.Utility.Validators;
using Festispec.Validators;
using GalaSoft.MvvmLight.Messaging;

namespace Festispec.ViewModel.InspectionFormViewModels
{
    public class QuestionViewModel : ViewModelBase
    {
        private Vraag _question;
        public bool Changed;
        public bool Created;
        

        public ICommand ImageButton { get; set; }

        public QuestionViewModel(Vraag v, Inspectieformulier inspec)
        {
            _question = v;
            InspectionFormID = inspec.InspectieformulierID;
            Changed = false;
            if (_question.VraagMogelijkAntwoord.Count() > 0)
            {
                AddPossibleAnwsersCreate();
            }
            else
            {
                AddPossibleAnwsers();
            }
            if (_question.AfbeeldingURL != null)
            {
                WebRequest webRequest = WebRequest.CreateDefault(new Uri("http://" + _question.AfbeeldingURL, UriKind.Absolute));
                webRequest.ContentType = "image/jpeg";
                WebResponse webResponse = webRequest.GetResponse();

                var image = new BitmapImage();
                image.CreateOptions = BitmapCreateOptions.None;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.BeginInit();

                image.StreamSource = webResponse.GetResponseStream();
                image.EndInit();

                Image = image;
            }
            ImageButton = new RelayCommand(SelectImage);
            
        }

        public int QuestionID
        {
            get => _question.VraagID;
            set
            {
                _question.VraagID = value;
                Changed = true;
                if (PossibleAnwsers != null)
                {
                    foreach(var anwser in PossibleAnwsers)
                    {
                        anwser.QuestionNumber = _question.VraagID;
                    }
                }
                RaisePropertyChanged("QuestionID");
            }
        }

        public ObservableCollection<PossibleAnwserViewModel> PossibleAnwsers { get; set; }

        private int _bottomValue;
        public int BottomValue
        {
            get => _bottomValue;
            set
            {
                _bottomValue = value;
                RaisePropertyChanged("BottomValue");
                UpdateScalePosAnwsers();
            }
        }

        private int _topValue;
        public int TopValue
        {
            get => _topValue;
            set
            {
                _topValue = value;
                RaisePropertyChanged("TopValue");
                UpdateScalePosAnwsers();
            }
        }

        private int _scaleSize;
        public int ScaleSize
        {
            get => _scaleSize;
            set
            {
                _scaleSize = value;
                RaisePropertyChanged("ScaleSize");
                UpdateScalePosAnwsers();
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

        private int _multipleChoiceOptionsAmount;
        public int MultipleChoiceOptionsAmount
        {
            get => _multipleChoiceOptionsAmount;
            set
            {
                _multipleChoiceOptionsAmount = value;
                UpdateMulipleChoiceAnwsers();
                RaisePropertyChanged("MultipleChoiceOptionAmount");
                Changed = true;
            }
        }

        private void UpdateMulipleChoiceAnwsers()
        {
            if(MultipleChoiceOptionsAmount < PossibleAnwsers.Count())
            {
                bool updateModel = false;
                if (_question.VraagMogelijkAntwoord.Count() != 0)
                {
                    updateModel = true;
                    _question.VraagMogelijkAntwoord.Clear();
                }
                int oldSize = PossibleAnwsers.Count();
                for (int i = MultipleChoiceOptionsAmount; i < oldSize; i++)
                {
                    if (updateModel){_question.VraagMogelijkAntwoord.Remove(PossibleAnwsers[MultipleChoiceOptionsAmount].PossibleAnwser);}
                    PossibleAnwsers.RemoveAt(MultipleChoiceOptionsAmount);
                } 
            }else if(MultipleChoiceOptionsAmount > PossibleAnwsers.Count())
            {
                int oldSize = PossibleAnwsers.Count();
                for (int i = oldSize; i < MultipleChoiceOptionsAmount; i++)
                {
                    PossibleAnwserViewModel answerModelnew = new PossibleAnwserViewModel(new VraagMogelijkAntwoord
                    {
                        VraagID = _question.VraagID,
                        AntwoordNummer = i + 1,
                        AntwoordText = (i + 1).ToString()
                    });
                    PossibleAnwsers.Add(answerModelnew);
                    if (_question.VraagMogelijkAntwoord.Count() != 0){_question.VraagMogelijkAntwoord.Add(answerModelnew.PossibleAnwser);}
                }
            }
        }

        public void AddPossibleAnwsersCreate()
        {
            if (_question.Vraagtype == "sv")
            {
                BottomValue = Int32.Parse(_question.VraagMogelijkAntwoord.First().AntwoordText);
                TopValue = Int32.Parse(_question.VraagMogelijkAntwoord.Last().AntwoordText);
                ScaleSize = _question.VraagMogelijkAntwoord.Count();
                UpdateScalePosAnwsers();
            }
            if (_question.Vraagtype == "mv")
            {
                List<PossibleAnwserViewModel> newPosAnwsers = new List<PossibleAnwserViewModel>();
                foreach (var answer in _question.VraagMogelijkAntwoord)
                {
                    PossibleAnwserViewModel answerModelnew = new PossibleAnwserViewModel(new VraagMogelijkAntwoord
                    {
                        VraagID = _question.VraagID,
                        AntwoordNummer = answer.AntwoordNummer,
                        AntwoordText = answer.AntwoordText

                    });
                    newPosAnwsers.Add(answerModelnew);
                }
                PossibleAnwsers = new ObservableCollection<PossibleAnwserViewModel>(newPosAnwsers);
                Changed = true;
                MultipleChoiceOptionsAmount = _question.VraagMogelijkAntwoord.Count();
            }
        }

        public void AddPossibleAnwsers()
        {

            if (_question.Vraagtype == "sv")
            {
                BottomValue = 1;
                TopValue = 5;
                ScaleSize = 5;
                UpdateScalePosAnwsers();
            }

            if(_question.Vraagtype == "mv")
            {
                List<PossibleAnwserViewModel> newPosAnwsers = new List<PossibleAnwserViewModel>();
                bool updateModelDirect = false;
                if (_question.VraagMogelijkAntwoord.Count != 0)
                    {
                    _question.VraagMogelijkAntwoord.Clear();
                    updateModelDirect = true;
                    }
                for (int i = 1; i < 5; i++)
                {
                    PossibleAnwserViewModel answerModelnew = new PossibleAnwserViewModel(new VraagMogelijkAntwoord
                    {
                        VraagID = _question.VraagID,
                        AntwoordNummer = i,
                        AntwoordText = (i).ToString()
                    });
                    if (updateModelDirect){_question.VraagMogelijkAntwoord.Add(answerModelnew.PossibleAnwser);}
                    newPosAnwsers.Add(answerModelnew);
                }
                PossibleAnwsers = new ObservableCollection<PossibleAnwserViewModel>(newPosAnwsers);
                Changed = true;
                MultipleChoiceOptionsAmount = 4;
            }
        }

        public void UpdateScalePosAnwsers()
        {
            if(ScaleSize == 0){return;}
            List<PossibleAnwserViewModel> newPosAnwsers = new List<PossibleAnwserViewModel>();
            if (_question.VraagMogelijkAntwoord.Count() != 0)
            {
                _question.VraagMogelijkAntwoord.Clear();
            }
            
            for (int i = 0; i < ScaleSize; i++)
            {
                newPosAnwsers.Add(new PossibleAnwserViewModel(new VraagMogelijkAntwoord
                {
                    VraagID = _question.VraagID,
                    AntwoordNummer = i + 1,
                    AntwoordText = GetScalePart(i).ToString()
                }));

                _question.VraagMogelijkAntwoord.Add(newPosAnwsers[i].PossibleAnwser);
            }
            PossibleAnwsers = new ObservableCollection<PossibleAnwserViewModel>(newPosAnwsers);
            Changed = true;
        }

        private int GetScalePart(int number)
        {
            if(number == 0){ return BottomValue;}
            else if(number == ScaleSize){ return TopValue; }
            else
            {
                return (BottomValue + (number * ((TopValue - BottomValue) / (ScaleSize - 1))));
            }
            
        }

        private BitmapImage _image;
        public BitmapImage EditImage
        {
            get
            {
                if (_image == null)
                {
                    BitmapImage bm = new BitmapImage();
                    bm.BeginInit();
                    bm.UriSource = new Uri("/Images/addImageIcon.png", UriKind.Relative);
                    bm.EndInit();
                    return bm;
                }
                else{return _image;}
            }
            set
            {
                _image = value;
                RaisePropertyChanged("Image");
                RaisePropertyChanged("EditImage");
            }
        }

        public BitmapImage Image
        {
            get
            {
                if(_image == null) { return null; }
                else { return _image; }
            }
            set
            {
                _image = value;
                RaisePropertyChanged("Image");
                RaisePropertyChanged("EditImage");
            }
        }

        private string _firstErrorMessage;

        public string FirstErrorMessage
        {
            get => _firstErrorMessage;
            set
            {
                _firstErrorMessage = value;
                RaisePropertyChanged("FirstErrorMessage");
            }
        }

        private string _secondErrorMessage;

        public string SecondErrorMessage
        {
            get => _secondErrorMessage;
            set
            {
                _secondErrorMessage = value;
                RaisePropertyChanged("SecondErrorMessage");
            }
        }

        public bool Validate()
        {
            FirstErrorMessage = null;
            SecondErrorMessage = null;
            bool returnValue = true;
            ValidationResult result1 = new QuestionValidator().Validate(this);
            if (!result1.IsValid)
            {
                FirstErrorMessage = result1.Errors.FirstOrDefault().ErrorMessage;
                returnValue = false;
            }
            if(QuestionType == "mv" || QuestionType == "sv")
            {
                foreach (PossibleAnwserViewModel posAnwser in PossibleAnwsers)
                {
                    ValidationResult result2 = new PossibleAnwserValidator().Validate(posAnwser);
                    if (!result2.IsValid)
                    {
                        SecondErrorMessage = result2.Errors.FirstOrDefault().ErrorMessage;
                        returnValue = false;
                    }
                }
            }
            return returnValue;
        }

        public void SelectImage()
        {
            ImageSelectService selectionService = new ImageSelectService();
            BitmapImage image = selectionService.SelectPngImage();
            if(image.UriSource == null){return;}

            Image = image;
            string fileLocation = Image.UriSource.AbsolutePath;
           
            UploadModel response = new ImageShackClient().UploadImage(new ImageContainer[1]
            {
                new ImageContainer(fileLocation, "image/png")
            });

            _question.AfbeeldingURL = (response.Images.First().DirectLink).ToString();

            Changed = true;
        }
    }
}
