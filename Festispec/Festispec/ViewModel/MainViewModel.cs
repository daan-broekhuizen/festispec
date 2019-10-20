using Festispec.View;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using System;
using System.IO;
using System.Reflection;
using System.Windows.Input;

namespace Festispec.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public ICommand OpenGraphCommand { get; set; }
        private GraphView _graphView;

        public MainViewModel()
        {
            this.OpenGraphCommand = new RelayCommand(() => {
                this._graphView = new GraphView();
                this._graphView.Show();
            });
        }
    }
}