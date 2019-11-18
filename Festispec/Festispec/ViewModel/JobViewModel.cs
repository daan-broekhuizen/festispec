using System;
using FestiSpec.Domain;
using GalaSoft.MvvmLight;

namespace Festispec.ViewModel
{
    public class JobViewModel : ViewModelBase
    {
        private Opdracht _opdracht;

        public JobViewModel(Opdracht opdracht)
        {
            _opdracht = opdracht;
        }

        public int JobID
        {
            get => _opdracht.OpdrachtID;
            set
            {
                _opdracht.OpdrachtID = value;
                RaisePropertyChanged("JobID");
            }
        }

        public string JobName
        {
            get => _opdracht.Opdracht_naam;
            set
            {
                _opdracht.Opdracht_naam = value;
                RaisePropertyChanged("JobName");
            }
        }

        public string Status
        {
            get => _opdracht.Status;
            set
            {
                _opdracht.Status = value;
                RaisePropertyChanged("Status");
            }
        }

        public DateTime CreationDate
        {
            get => _opdracht.Creatie_datum;
            set
            {
                _opdracht.Creatie_datum = value;
                RaisePropertyChanged("CreationDate");
            }
        }

        public long CustomerID
        {
            get => _opdracht.KlantID;
            set
            {
                _opdracht.KlantID = value;
                RaisePropertyChanged("CustomerID");
            }
        }

        public int WorkerID
        {
            get => _opdracht.MedewerkerID;
            set
            {
                _opdracht.MedewerkerID = value;
                RaisePropertyChanged("WorkerID");
            }
        }

        public string Jurisdictions
        {
            get => _opdracht.Gebruikte_rechtsgebieden;
            set
            {
                _opdracht.Gebruikte_rechtsgebieden = value;
                RaisePropertyChanged("Jurisdiction");
            }
        }

        public string Report
        {
            get => _opdracht.Rapportage;
            set
            {
                _opdracht.Rapportage = value;
                RaisePropertyChanged("Report");
            }
        }

        public DateTime LastChange
        {
            get => _opdracht.Laatste_weiziging;
            set
            {
                _opdracht.Laatste_weiziging = value;
                RaisePropertyChanged("LastChange");
            }
        }


        public string CustomerWishes
        {
            get => _opdracht.Klantwensen;
            set
            {
                _opdracht.Klantwensen = value;
                RaisePropertyChanged("CustomerWishes");
            }
        }

        
    }
}