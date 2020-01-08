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
        private Rol rol;

        public RollenViewModel()
        {

        }

        public RollenViewModel(Rol rol)
        {
            this.rol = rol;
        }

        public string Afkorting
        {
            get => rol.Afkorting;
            set
            {
                rol.Afkorting = value;
                RaisePropertyChanged("Afkorting");
            }
        }

        public string Betekenis
        {
            get => rol.Betekenis;
            set
            {
                rol.Betekenis = value;
                RaisePropertyChanged("Betekenis");
            }
        }

    }
}