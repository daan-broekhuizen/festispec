using Festispec.Model;
using GalaSoft.MvvmLight;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel
{
    public class OfflineInspectionViewModel : ViewModelBase
    {
        public ObservableCollection<InspectionModel> OfflineInspections { get; set; }
        public OfflineInspectionViewModel()
        {
            GetInspections();
        }

        private void GetInspections()
        {
            OfflineInspections = new ObservableCollection<InspectionModel>();
            string path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "/Resources/inspections.json";
            string json = File.ReadAllText(path);
            List<InspectionModel> inspections = JsonConvert.DeserializeObject<List<InspectionModel>>(json);
            foreach(InspectionModel m in inspections)
            {
                OfflineInspections.Add(m);
                RaisePropertyChanged();
            }
        }
    }
}
