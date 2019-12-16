using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model.Repositories
{
    class InspectionRepository
    {
        public ObservableCollection<InspectionModel> _inspections { get; set; }

        public InspectionRepository()
        {
            _inspections = new ObservableCollection<InspectionModel>();
            _inspections.Add(new InspectionModel(1, "PinkPop", new DateTime(2019, 12, 20)));
            _inspections.Add(new InspectionModel(1, "PinkPop", new DateTime(2019, 12, 21)));
            _inspections.Add(new InspectionModel(1, "PinkPop", new DateTime(2019, 11, 12)));
            _inspections.Add(new InspectionModel(1, "PinkPop", new DateTime(2019, 9, 13)));
            _inspections.Add(new InspectionModel(1, "PinkPop", new DateTime(2019, 4, 7)));
        }

        public ObservableCollection<InspectionModel> GetInspections()
        {
            return _inspections;
        }
    }
}
