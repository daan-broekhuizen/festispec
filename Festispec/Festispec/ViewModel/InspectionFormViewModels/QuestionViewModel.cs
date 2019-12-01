using Festispec.Model;
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
        private int _CurrentInspectionFormID;
        public bool Changed;
        public bool Created;
        public int BottomValue;
        public int TopValue;
        public int ScaleSize;

        public QuestionViewModel(Vraag v, Inspectieformulier inspec)
        {
            _question = v;
            _CurrentInspectionFormID = inspec.InspectieformulierID;
            List<InspectieformulierVragenlijstCombinatie> vicList = new List<InspectieformulierVragenlijstCombinatie>();
            VIC = vicList;
            Changed = false;
            InspectieformulierVragenlijstCombinatie vic = new InspectieformulierVragenlijstCombinatie
            {
                VraagID = v.VraagID,
                InspectieformulierID = inspec.InspectieformulierID
            };
            VIC.Add(vic);
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
                _question.Vraagtype= value;
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
            get => VIC.Where(i => i.InspectieformulierID == _CurrentInspectionFormID).FirstOrDefault().VraagVolgordeNummer;
            set
            {
                VIC.Where(i => i.InspectieformulierID == _CurrentInspectionFormID).FirstOrDefault().VraagVolgordeNummer = value;
                Changed = true;
                RaisePropertyChanged("OrderNumber");
            }
        }

        public ICollection<InspectieformulierVragenlijstCombinatie> VIC
        {
            get => _question.InspectieformulierVragenlijstCombinatie;
            set
            {
                _question.InspectieformulierVragenlijstCombinatie = value;
                Changed = true;
                RaisePropertyChanged("VIC");
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
                for (int i = 0; i < 5; i++)
                {
                    _question.VraagMogelijkAntwoord.Add(new VraagMogelijkAntwoord
                    {
                        VraagID = _question.VraagID,
                        AntwoordNummer = i + 1,
                        AntwoordText = (i + 1).ToString()
                    });
                }

                BottomValue = 0;
                TopValue = 5;
                ScaleSize = 5;
            }
        }
    }
}
