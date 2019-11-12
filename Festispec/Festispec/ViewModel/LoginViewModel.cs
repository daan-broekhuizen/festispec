using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;

namespace Festispec.ViewModel
{
    public class LoginViewModel
    {
        private string userName;
        public string UserName
        {
            get { return this.userName; }
            set
            {
                // Implement with property changed handling for INotifyPropertyChanged
                if (!string.Equals(this.userName, value))
                {
                    this.userName = value;
                    //RaisePropertyChanged(""); // Method to raise the PropertyChanged event in your BaseViewModel class...
                }
            }
        }

    }
}
