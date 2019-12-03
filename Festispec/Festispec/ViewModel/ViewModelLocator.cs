/*
  In App.xaml:
  <Application.Resources>
      <vm:ViewModelLocator xmlns:vm="clr-namespace:Festispec"
                           x:Key="Locator" />
  </Application.Resources>
  
  In the View:
  DataContext="{Binding Source={StaticResource Locator}, Path=ViewModelName}"

  You can also use Blend to do all this with the tool's support.
  See http://www.galasoft.ch/mvvm
*/

using CommonServiceLocator;
using Festispec.Model.Repositories;
using Festispec.Service;
using Festispec.ViewModel;
using Festispec.ViewModel.InspectionFormViewModels;
using Festispec.ViewModel.RapportageViewModels;
using Festispec.ViewModel.TemplateViewModels;
using FestiSpec.Domain.Repositories;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;
using System;

namespace Festispec.ViewModel
{
    /// <summary>
    /// This class contains static references to all the view models in the
    /// application and provides an entry point for the bindings.
    /// </summary>
    public class ViewModelLocator
    {
        /// <summary>
        /// Initializes a new instance of the ViewModelLocator class.
        /// </summary>
        ///
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SetupNavigation();
            RegisterRepositories();
            RegisterViewModels();
        }

        //Register singleton repositories here
        private static void RegisterRepositories()
        {
            SimpleIoc.Default.Register<CustomerRepository>();
            SimpleIoc.Default.Register<JobRepository>();
            SimpleIoc.Default.Register<StatusRepository>();
            SimpleIoc.Default.Register<UserRepository>();
            SimpleIoc.Default.Register<TemplateRepository>();
        }
        //Register singeltonviews here
        private static void RegisterViewModels()
        {
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<DashboardViewModel>();
            SimpleIoc.Default.Register<ApplicationViewModel>();

        }
        //Configure view mappings here and register navigation service
        private static void SetupNavigation()
        {
            NavigationService navigationService = new NavigationService();
            navigationService.Configure("Dashboard", new Uri("../View/DashboardView.xaml", UriKind.Relative));
            navigationService.Configure("Main", new Uri("../View/MainWindow.xaml", UriKind.Relative));
            navigationService.Configure("Login", new Uri("../View/LoginView.xaml", UriKind.Relative));
            navigationService.Configure("Rapportage", new Uri("../View/RapportageView/RapportageView.xaml", UriKind.Relative));
            navigationService.Configure("CreateInspectionForm", new Uri("../View/InspectionFormView/CreateInspectionFormView.xaml", UriKind.Relative));
            navigationService.Configure("ManagementReport", new Uri("../View/ManagementReport.xaml", UriKind.Relative));


            #region CustomerViews
            navigationService.Configure("Customers", new Uri("../View/CustomerView/CustomerListView.xaml", UriKind.Relative));
            navigationService.Configure("AddCustomerInfo", new Uri("../View/CustomerView/AddCustomerInfoView.xaml", UriKind.Relative));
            navigationService.Configure("AddContactInfo", new Uri("../View/CustomerView/AddContactInfoView.xaml", UriKind.Relative));
            navigationService.Configure("AddContactPerson", new Uri("../View/CustomerView/AddContactPersonView.xaml", UriKind.Relative));
            navigationService.Configure("CustomerInfo", new Uri("../View/CustomerView/CustomerInfoView.xaml", UriKind.Relative));
            navigationService.Configure("ContactInfo", new Uri("../View/CustomerView/ContactInfoView.xaml", UriKind.Relative));
            navigationService.Configure("ContactPersons", new Uri("../View/CustomerView/ContactPersonListView.xaml", UriKind.Relative));
            #endregion

            #region JobViews
            navigationService.Configure("Jobs", new Uri("../View/JobsWindow.xaml", UriKind.Relative));
            navigationService.Configure("JobInfo", new Uri("../View/JobInfoView.xaml", UriKind.Relative));
            navigationService.Configure("AddJob", new Uri("../View/AddJobView.xaml", UriKind.Relative));
            #endregion



            navigationService.Configure("ContactPersons", new Uri("../View/CustomerView/ContactPersonListView.xaml", UriKind.Relative));
            #region TemplateViews
            navigationService.Configure("RapportageTemplateOverview", new Uri("../View/TemplateView/RapportageTemplateOverviewView.xaml", UriKind.Relative));
            navigationService.Configure("InspectionFormTemplateOverview", new Uri("../View/TemplateView/InspectionFormTemplateOverviewView.xaml", UriKind.Relative));
            #endregion

            SimpleIoc.Default.Register<NavigationService>(() => navigationService);

        }

        // Singleton repos
        public CustomerRepository CustomerRepo => ServiceLocator.Current.GetInstance<CustomerRepository>();
        public JobRepository JobRepo => ServiceLocator.Current.GetInstance<JobRepository>();
        public StatusRepository StatusRepo => ServiceLocator.Current.GetInstance<StatusRepository>();


        public UserRepository UserRepo => ServiceLocator.Current.GetInstance<UserRepository>();
        public TemplateRepository TemplateRepo => ServiceLocator.Current.GetInstance<TemplateRepository>();

        // Viewmodels used for datacontext
        #region Singleton VM's
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public DashboardViewModel Dashboard => ServiceLocator.Current.GetInstance<DashboardViewModel>();
        public ApplicationViewModel Application => ServiceLocator.Current.GetInstance<ApplicationViewModel>();
        public LoginViewModel Login => new LoginViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), UserRepo);
        public RapportageViewModel Rapportage => new RapportageViewModel(ServiceLocator.Current.GetInstance<NavigationService>());
        public CreateInspectionFormViewModel CreateInspectionForm => new CreateInspectionFormViewModel(ServiceLocator.Current.GetInstance<NavigationService>());
        #endregion

        #region CustomerVM's
        public CustomerListViewModel CustomerList => new CustomerListViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), CustomerRepo);
        public AddContactInfoViewModel AddContactInfo => new AddContactInfoViewModel(ServiceLocator.Current.GetInstance<NavigationService>());
        public AddContactPersonViewModel AddContactPerson => new AddContactPersonViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), CustomerRepo);
        public AddCustomerInfoViewModel AddCustomerInfo => new AddCustomerInfoViewModel(ServiceLocator.Current.GetInstance<NavigationService>());
        public ContactPersonListViewModel ContactPersons => new ContactPersonListViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), CustomerRepo);
        public CustomerInfoViewModel CustomerInfo => new CustomerInfoViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), CustomerRepo);
        #endregion

        #region JobsVM's
        public JobListViewModel JobList => new JobListViewModel(SimpleIoc.Default.GetInstance<NavigationService>());
        public JobInfoViewModel JobInfo => new JobInfoViewModel(SimpleIoc.Default.GetInstance<NavigationService>(), JobRepo, StatusRepo);
        public AddJobViewModel AddJob => new AddJobViewModel(SimpleIoc.Default.GetInstance<NavigationService>(),JobRepo, CustomerRepo, StatusRepo);
        #endregion
        #region Template VM's
        public RapportageTemplateOverviewViewModel RapportageTemplateOverview => new RapportageTemplateOverviewViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), TemplateRepo);
        public InspectionFormTemplateOverviewViewModel InspectionFormTemplateOverview => new InspectionFormTemplateOverviewViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), TemplateRepo);
        #endregion

        //Clean when logging out?
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}