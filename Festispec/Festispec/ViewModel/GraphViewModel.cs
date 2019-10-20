using Festispec.Model;
using Festispec.Model.Misc;
using Festispec.Model.Repositories;
using GalaSoft.MvvmLight;
using LiveCharts;
using LiveCharts.Wpf;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel
{
    public class GraphViewModel : ViewModelBase
    {
        private GraphRepository _repo;

        public GraphViewModel()
        {
            this._repo = new GraphRepository();
        }
    }
}
