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
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;

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
            AddPossibleAnwsers();
            ImageButton = new RelayCommand(SelectImage);
            
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

        public void AddPossibleAnwsers()
        {
            if (_question.Vraagtype == "sv")
            {
                BottomValue = 0;
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
                    if (updateModelDirect)
                    {
                        _question.VraagMogelijkAntwoord.Add(answerModelnew.PossibleAnwser);
                    }
                    newPosAnwsers.Add(answerModelnew);
                }
                PossibleAnwsers = new ObservableCollection<PossibleAnwserViewModel>(newPosAnwsers);
                Changed = true;
            }
        }

        public void UpdateScalePosAnwsers()
        {
            if(ScaleSize == 0)
            {
                return;
            }
            List<PossibleAnwserViewModel> newPosAnwsers = new List<PossibleAnwserViewModel>();
            int scalePart = (TopValue - BottomValue) / ScaleSize;
            bool directUpdate = false;
            if (_question.VraagMogelijkAntwoord.Count() != 0)
            {
                _question.VraagMogelijkAntwoord.Clear();
                directUpdate = true;
            }
            
            for (int i = 0; i < ScaleSize; i++)
            {
                newPosAnwsers.Add(new PossibleAnwserViewModel(new VraagMogelijkAntwoord
                {
                    VraagID = _question.VraagID,
                    AntwoordNummer = i + 1,
                    AntwoordText = (BottomValue + (i * scalePart) + 1).ToString()
                }));

                if (directUpdate)
                {
                    _question.VraagMogelijkAntwoord.Add(newPosAnwsers[i].PossibleAnwser);
                }
            }
            PossibleAnwsers = new ObservableCollection<PossibleAnwserViewModel>(newPosAnwsers);
            Changed = true;
        }

        private BitmapImage _image;
        public BitmapImage Image
        {
            get
            { if (_image == null)
                {
                    BitmapImage bm = new BitmapImage();
                    bm.BeginInit();
                    bm.UriSource = new Uri("/Images/addImageIcon.png", UriKind.Relative);
                    bm.EndInit();
                    return bm;
                }
                else
                {
                    return _image;
                }
            }
            set
            {
                _image = value;
                RaisePropertyChanged("Image");
            }
        }

        public void SelectImage()
        {
            ImageSelectService selectionService = new ImageSelectService();
            BitmapImage image = selectionService.SelectPngImage();
            if(image.UriSource == null)
            {
                return;
            }

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
