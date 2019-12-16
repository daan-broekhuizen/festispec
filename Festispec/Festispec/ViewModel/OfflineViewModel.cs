using Festispec.Model;
using Festispec.Model.Repositories;
using Festispec.View.Offline;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Festispec.ViewModel
{
    public class OfflineViewModel : ViewModelBase
    {
        private string path;
        private InspectionRepository _repo;
        public ObservableCollection<InspectionModel> Inspections { get; set; }
        public ICommand OpenSavedInspectionsCommand { get; set; }
        private OfflineInspectionView _offlineInspectionView;
        public ICommand SaveInspectionCommand { get; set; }
        private InspectionModel selectedInspection { get; set; }

        public OfflineViewModel()
        {
            this._repo = new InspectionRepository();
            this.Inspections = _repo.GetInspections();
            this.OpenSavedInspectionsCommand = new RelayCommand(() =>
            {
                this._offlineInspectionView = new OfflineInspectionView();
                this._offlineInspectionView.Show();
            });



            this.SaveInspectionCommand = new RelayCommand(SaveInspection);
        }

        public InspectionModel SelectedInspection
        {
            get { return selectedInspection; }
            set
            {
                if (selectedInspection != value) { }
                    selectedInspection = value;
                    RaisePropertyChanged();
            }
        }

        public void SaveInspection()
        {
            path = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName + "/Resources/inspections.json";
            if (selectedInspection != null)
            {
                List<InspectionModel> jsonarray = new List<InspectionModel>();
                if (!File.Exists(path))
                {
                    jsonarray.Add(selectedInspection);
                    using(StreamWriter sw = File.CreateText(path))
                    {
                        sw.WriteLine(JsonConvert.SerializeObject(jsonarray));
                    }
                    
                }
                else
                {
                    string newJson;
                    using(StreamReader sr = new StreamReader(path))
                    {
                        string json = sr.ReadToEnd();
                        List<InspectionModel> jsonInspections = JsonConvert.DeserializeObject<List<InspectionModel>>(json);
                        jsonInspections.Add(selectedInspection);
                        newJson = JsonConvert.SerializeObject(jsonInspections);
                    }

                    using (StreamWriter sw = new StreamWriter(path, false))
                    {
                        sw.WriteLine(newJson);
                    }
                }
            }
           
        }
    }
}
