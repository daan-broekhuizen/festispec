using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Festispec.Model;
using Festispec.Service;
using FestiSpec.Domain.Repositories;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace Festispec.ViewModel
{
    public class RollenViewModel : ViewModelBase
    {
        private Rol _rol;

        public RollenViewModel()
        {

        }

        public RollenViewModel(Rol rol)
        {
            this._rol = rol;
        }

        public string Afkorting
        {
            get => _rol.Afkorting;
            set
            {
                _rol.Afkorting = value;
                RaisePropertyChanged("Afkorting");
            }
        }

        public string Betekenis
        {
            get => _rol.Betekenis;
            set
            {
                _rol.Betekenis = value;
                RaisePropertyChanged("Betekenis");
            }
        }

    }
}