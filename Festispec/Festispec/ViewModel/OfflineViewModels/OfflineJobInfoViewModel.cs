using Festispec.Model.Repositories;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec.Service;

namespace Festispec.ViewModel.OfflineViewModels
{
    public class OfflineJobInfoViewModel : ViewModelBase
    {
        public OfflineJobViewModel JobVM { get; set; }
        private NavigationService _navigationService;
        private OfflineJobRepository _offlineJobRepo;
        public OfflineJobInfoViewModel(NavigationService service, OfflineJobRepository repo)
        {
            this._navigationService = service;
            this._offlineJobRepo = repo;
            if (service.Parameter is OfflineJobViewModel)
                JobVM = service.Parameter as OfflineJobViewModel;
        }
    }
}
