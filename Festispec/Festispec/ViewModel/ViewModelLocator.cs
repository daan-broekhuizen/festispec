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
using Festispec.ViewModel.QuotationViewModels;
using Festispec.ViewModel.RapportageViewModels;
using Festispec.ViewModel.TemplateViewModels;
using FestiSpec.Domain.Repositories;
using GalaSoft.MvvmLight.Ioc;
using System;
using Festispec.ViewModel.InspectionFormViewModels;
using Festispec.Model;
using Festispec.ViewModel.OfflineViewModels;

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
            SimpleIoc.Default.Register<QuotationRepository>();
            SimpleIoc.Default.Register<TemplateRepository>();
            SimpleIoc.Default.Register<RapportageRepository>();
            SimpleIoc.Default.Register<InspectionFormRepository>();
            SimpleIoc.Default.Register<OfflineJobRepository>();
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
            navigationService.Configure("UserRights", new Uri("../View/UserRightsView.xaml", UriKind.Relative));
            navigationService.Configure("AddUser", new Uri("../View/AddGebruiker.xaml", UriKind.Relative));

            #region CustomerViews
            navigationService.Configure("Customers", new Uri("../View/CustomerView/CustomerListView.xaml", UriKind.Relative));
            navigationService.Configure("AddCustomerInfo", new Uri("../View/CustomerView/AddCustomerInfoView.xaml", UriKind.Relative));
            navigationService.Configure("AddContactInfo", new Uri("../View/CustomerView/AddContactInfoView.xaml", UriKind.Relative));
            navigationService.Configure("AddContactPerson", new Uri("../View/CustomerView/AddContactPersonView.xaml", UriKind.Relative));
            navigationService.Configure("CustomerInfo", new Uri("../View/CustomerView/CustomerInfoView.xaml", UriKind.Relative));
            navigationService.Configure("ContactInfo", new Uri("../View/CustomerView/ContactInfoView.xaml", UriKind.Relative));
            navigationService.Configure("ContactPersons", new Uri("../View/CustomerView/ContactPersonListView.xaml", UriKind.Relative));
            #endregion

            #region QuotationViews
            navigationService.Configure("QuotationList", new Uri("../View/QuotationView/QuotationListView.xaml", UriKind.Relative));
            navigationService.Configure("AddQuotation", new Uri("../View/QuotationView/AddQuotationView.xaml", UriKind.Relative));
            navigationService.Configure("ShowQuotation", new Uri("../View/QuotationView/ShowQuotationView.xaml", UriKind.Relative));
            #endregion

            #region TemplateViews
            navigationService.Configure("TemplateOverview", new Uri("../View/TemplateView/TemplateOverviewView.xaml", UriKind.Relative));
            #endregion

            #region JobViews
            navigationService.Configure("Jobs", new Uri("../View/JobView/JobsWindow.xaml", UriKind.Relative));
            navigationService.Configure("JobInfo", new Uri("../View/JobView/JobInfoView.xaml", UriKind.Relative));
            navigationService.Configure("AddJob", new Uri("../View/JobView/AddJobView.xaml", UriKind.Relative));
            #endregion

            #region TemplateViews
            navigationService.Configure("RapportageTemplateOverview", new Uri("../View/TemplateView/RapportageTemplateOverviewView.xaml", UriKind.Relative));
            navigationService.Configure("InspectionFormTemplateOverview", new Uri("../View/TemplateView/InspectionFormTemplateOverviewView.xaml", UriKind.Relative));
            #endregion

            #region InspectionFormsViews
            navigationService.Configure("InspectionFormEditView", new Uri("../View/InspectionFormView/InspectionFormEditView.xaml", UriKind.Relative));
            navigationService.Configure("InspectionFormShowView", new Uri("../View/InspectionFormView/InspectionFormShowView.xaml", UriKind.Relative));
            #endregion

            #region OfflineViews
            navigationService.Configure("OfflineLogin", new Uri("../View/OfflineView/OfflineLoginView.xaml", UriKind.Relative));
            navigationService.Configure("OfflineJobList", new Uri("../View/OfflineView/OfflineJobsView.xaml", UriKind.Relative));
            navigationService.Configure("OfflineJob", new Uri("../View/OfflineView/OfflineJobInfoView.xaml", UriKind.Relative));
            #endregion

            SimpleIoc.Default.Register<NavigationService>(() => navigationService);

        }

        // Singleton repos
        public CustomerRepository CustomerRepo => ServiceLocator.Current.GetInstance<CustomerRepository>();
        public JobRepository JobRepo => ServiceLocator.Current.GetInstance<JobRepository>();
        public StatusRepository StatusRepo => ServiceLocator.Current.GetInstance<StatusRepository>();
        public UserRepository UserRepo => ServiceLocator.Current.GetInstance<UserRepository>();
        public QuotationRepository QuotationRepo => ServiceLocator.Current.GetInstance<QuotationRepository>();
        public TemplateRepository TemplateRepo => ServiceLocator.Current.GetInstance<TemplateRepository>();
        public RapportageRepository RapportageRepo => ServiceLocator.Current.GetInstance<RapportageRepository>();
        public InspectionFormRepository InspectionFormRepo => ServiceLocator.Current.GetInstance<InspectionFormRepository>();
        public OfflineJobRepository OfflineJobRepo => ServiceLocator.Current.GetInstance<OfflineJobRepository>();
      
        // Viewmodels used for datacontext
        #region Singleton VM's
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public DashboardViewModel Dashboard => ServiceLocator.Current.GetInstance<DashboardViewModel>();
        public ApplicationViewModel Application => ServiceLocator.Current.GetInstance<ApplicationViewModel>();
        public LoginViewModel Login => new LoginViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), UserRepo);
        public RapportageViewModel Rapportage => new RapportageViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), RapportageRepo);
        public CreateInspectionFormViewModel CreateInspectionForm => new CreateInspectionFormViewModel(ServiceLocator.Current.GetInstance<NavigationService>());
        public UserRightsViewModel UserRights => new UserRightsViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), UserRepo);
        public RegisterViewModel Register => new RegisterViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), UserRepo);
        #endregion

        #region CustomerVM's
        public CustomerListViewModel CustomerList => new CustomerListViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), CustomerRepo);
        public AddContactInfoViewModel AddContactInfo => new AddContactInfoViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), CustomerRepo);
        public AddContactPersonViewModel AddContactPerson => new AddContactPersonViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), CustomerRepo);
        public AddCustomerInfoViewModel AddCustomerInfo => new AddCustomerInfoViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), CustomerRepo);
        public ContactPersonListViewModel ContactPersons => new ContactPersonListViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), CustomerRepo);
        public CustomerInfoViewModel CustomerInfo => new CustomerInfoViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), CustomerRepo);
        #endregion

        #region QuotationsVM's
        public AddQuotationViewModel AddQuotation => new AddQuotationViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), QuotationRepo);
        public QuotationListViewModel QuotationList => new QuotationListViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), QuotationRepo);
        public ShowQuotationViewModel ShowQuotation => new ShowQuotationViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), QuotationRepo);
        #endregion

        #region JobsVM's
        public JobListViewModel JobList => new JobListViewModel(SimpleIoc.Default.GetInstance<NavigationService>());
        public JobInfoViewModel JobInfo => new JobInfoViewModel(SimpleIoc.Default.GetInstance<NavigationService>(), JobRepo, StatusRepo,  QuotationRepo);
        public AddJobViewModel AddJob => new AddJobViewModel(SimpleIoc.Default.GetInstance<NavigationService>(),JobRepo, CustomerRepo, StatusRepo);
        #endregion

        #region Template VM's
        public RapportageTemplateOverviewViewModel RapportageTemplateOverview => new RapportageTemplateOverviewViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), TemplateRepo);
        public InspectionFormTemplateOverviewViewModel InspectionFormTemplateOverview => new InspectionFormTemplateOverviewViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), TemplateRepo);
        #endregion

        public ManagementViewModel Management => new ManagementViewModel(JobRepo, QuotationRepo, UserRepo, CustomerRepo, InspectionFormRepo);

        #region InspectionFormVM's
        public InspectionFormViewModel InspectionForm => new InspectionFormViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), InspectionFormRepo);
        public InspectionFormListViewModel InspectionFormList => new InspectionFormListViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), InspectionFormRepo);

        #endregion

        #region OfflineVM's
        public OfflineLoginViewModel OfflineLogin => new OfflineLoginViewModel(ServiceLocator.Current.GetInstance<NavigationService>());
        public OfflineJobListViewModel OfflineJobList => new OfflineJobListViewModel(ServiceLocator.Current.GetInstance<NavigationService>());
        public OfflineJobInfoViewModel OfflineJob => new OfflineJobInfoViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), OfflineJobRepo);
        #endregion

        //Clean when logging out?
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
} 