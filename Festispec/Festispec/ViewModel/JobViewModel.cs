using System;
using System.Collections.Generic;
using Festispec.Model;
using FestiSpec.Domain;
using FestiSpec.Domain.Repositories;
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

        public JobViewModel()
        {
            _opdracht = new Opdracht();

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
            get => _opdracht.OpdrachtNaam;
            set
            {
                _opdracht.OpdrachtNaam = value;
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
            get => _opdracht.CreatieDatum;
            set
            {
                _opdracht.CreatieDatum = value;
                RaisePropertyChanged("CreationDate");
            }
        }

        public string CustomerID
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
            get => _opdracht.GebruikteRechtsgebieden;
            set
            {
                _opdracht.GebruikteRechtsgebieden = value;
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
            get => _opdracht.LaatsteWijziging;
            set
            {
                _opdracht.LaatsteWijziging = value;
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

        public string CustomerName
        {
            get
            {
                if(_opdracht.Klant == null)
                {
                    return "";
                }
                else
                {
                    return _opdracht.Klant.Naam;
                }
            }
            set
            {
                if(_opdracht.Klant == null)
                {
                    _opdracht.Klant = new Klant();
                }
                _opdracht.Klant.Naam = value;
                RaisePropertyChanged("CustomerName");
            }
        }


    }
}