using Festispec.ViewModel.RapportageViewModels;
using LiveCharts.Defaults;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.ViewModel.Components.Charts.Data
{
    public class GeneralChartData
    {
        public ObservableCollection<string> Labels { get; set; }
        public ObservableCollection<int> Values { get; set; }

        public GeneralChartData()
        {
            Labels = new ObservableCollection<string>();
            Values = new ObservableCollection<int>();
        }

        public void UpdateValues(List<double> values)
        {
            Values.Clear();

            foreach (int val in values)
                Values.Add(val);
        }

        public void Update(VraagViewModel question)
        {
            Console.WriteLine("Values");
        }
    }
}
