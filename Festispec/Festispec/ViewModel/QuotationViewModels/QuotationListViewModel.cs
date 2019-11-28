using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media;
using Festispec.Model.Repositories;
using Festispec.Service;
using GalaSoft.MvvmLight.CommandWpf;

namespace Festispec.ViewModel.QuotationViewModels
{
    public class QuotationListViewModel : NavigatableViewModel
    {
        private QuotationViewModel selectedQuotation;
        public QuotationViewModel SelectedQuotation 
        { 
            get => selectedQuotation;
            set
            {
                selectedQuotation = value;
                RaisePropertyChanged("SelectedQuotation");
                ShowQuotation();
            }
        }
        public ObservableCollection<QuotationViewModel> Quotations { get; set; }
        private List<QuotationViewModel> _filteredQuotations;
        public List<QuotationViewModel> FilteredQuotations
        {
            get => _filteredQuotations;
            set
            {
                _filteredQuotations = value;
                RaisePropertyChanged("FilteredQuotations");
            }
        }

        public ICommand SearchButtonClickCommand { get; set; }
        public ICommand SearchTextChangedCommand { get; set; }
        public ICommand SortChangedCommand { get; set; }

        private Color[] _colorOrderAsc = { Colors.Green, Colors.Yellow, Colors.Red, Colors.Blue, Colors.Black };
        private Color[] _colorOrderDsc = { Colors.Blue, Colors.Red, Colors.Yellow, Colors.Green, Colors.Black };


        private QuotationRepository _quotationRepository;
        public QuotationListViewModel(NavigationService service, QuotationRepository repo) : base(service)
        {
            _quotationRepository = repo;
            var list = _quotationRepository.GetQuotations().Select(q => new QuotationViewModel(q)).ToList();
            Quotations = new ObservableCollection<QuotationViewModel>(list);
            FilteredQuotations = Quotations.ToList();

            SearchButtonClickCommand = new RelayCommand<string>(FilterQuotations);
            SearchTextChangedCommand = new RelayCommand<string>(FilterQuotations);
            SortChangedCommand = new RelayCommand<int>(ChangeSort);
        }

        private void ChangeSort(int sortMode)
        {
            switch (sortMode)
            {
                case 0:
                    FilteredQuotations = Quotations.OrderBy(q => Array.IndexOf(_colorOrderAsc, q.ColorCode)).ToList();
                    break;
                case 1:
                    FilteredQuotations = Quotations.OrderBy(q => Array.IndexOf(_colorOrderDsc, q.ColorCode)).ToList();
                    break;
                case 2:
                    FilteredQuotations = Quotations.OrderBy(q => q.CreationDate).ToList();
                    break;
                case 3:
                    FilteredQuotations = Quotations.OrderByDescending(q => q.CreationDate).ToList();
                    break;
                case 4:
                    FilteredQuotations = Quotations.OrderBy(q => q.LastEdit).ToList();
                    break;
                case 5:
                    FilteredQuotations = Quotations.OrderByDescending(q => q.LastEdit).ToList();
                    break;
            }
        }
        private void FilterQuotations(string searchText)
        {
            FilteredQuotations = Quotations.Where(q => q.Job.ToLower().Contains(searchText.ToLower())).ToList();
        }
        private void ShowQuotation()
        {
            if(SelectedQuotation != null)
                _navigationService.NavigateTo("ShowQuotation", SelectedQuotation);
        }



    }
}
