using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Model.Repositories
{
    class AvailabilityRepository
    {
        #region Availability
        public ObservableCollection<AvailabilityModel> _availabilities { get; set; }

        public AvailabilityRepository()
        {
            _availabilities = new ObservableCollection<AvailabilityModel>();
            _availabilities.Add(new AvailabilityModel(new DateTime(2019, 11, 13)));
            _availabilities.Add(new AvailabilityModel(new DateTime(2019, 11, 14)));
            _availabilities.Add(new AvailabilityModel(new DateTime(2019, 11, 17)));
            _availabilities.Add(new AvailabilityModel(new DateTime(2019, 11, 19)));
            _availabilities.Add(new AvailabilityModel(new DateTime(2019, 12, 4)));
            _availabilities.Add(new AvailabilityModel(new DateTime(2019, 12, 21)));
        }

        public ObservableCollection<AvailabilityModel> GetAvailabilities()
        {
            return _availabilities;
        }
        #endregion
    }
}
