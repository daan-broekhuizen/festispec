using Festispec.Utility.Validators;
using FluentValidation.Results;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace Festispec.ViewModel.Components
{
    public class SaveDialogViewModel : ViewModelBase
    {      
        // Commands
        public ICommand SaveCommand { get; set; }

        // Properties
        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;

                RaisePropertyChanged("Name");
            }
        }

        private string _errorName;
        public string ErrorName
        {
            get => _errorName;
            set
            {
                _errorName = value;

                RaisePropertyChanged("ErrorName");
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;

                RaisePropertyChanged("Description");
            }
        }

        public SaveDialogViewModel()
        {
            SaveCommand = new RelayCommand<Window>(Save);
        }

        private void Save(Window window)
        {
            ValidationResult result = new SaveDialogValidator().Validate(this);

            if (result.IsValid)
                window.Close();
            else
            {
                ValidationFailure errorName = result.Errors.Where(e => e.PropertyName == "Name").FirstOrDefault();
                if (errorName != null)
                    ErrorName = errorName.ErrorMessage;
            }
        }
    }
}
