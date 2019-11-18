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
using Festispec.Service;
using Festispec.ViewModel;
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
        }
        //Register singeltonviews here
        private static void RegisterViewModels()
        {
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<DashboardViewModel>();
        }
        //Configure view mappings here and register navigation service
        private static void SetupNavigation()
        {
            NavigationService navigationService = new NavigationService();
            navigationService.Configure("Dashboard", new Uri("../View/DashboardView.xaml", UriKind.Relative));

            #region CustomerViews
            navigationService.Configure("Customers", new Uri("../View/CustomerView/CustomerListView.xaml", UriKind.Relative));
            navigationService.Configure("AddCustomerInfo", new Uri("../View/CustomerView/AddCustomerInfoView.xaml", UriKind.Relative));
            navigationService.Configure("AddContactInfo", new Uri("../View/CustomerView/AddContactInfoView.xaml", UriKind.Relative));
            navigationService.Configure("AddContactPerson", new Uri("../View/CustomerView/AddContactPersonView.xaml", UriKind.Relative));
            navigationService.Configure("CustomerInfo", new Uri("../View/CustomerView/CustomerInfoView.xaml", UriKind.Relative));
            navigationService.Configure("ContactInfo", new Uri("../View/CustomerView/ContactInfoView.xaml", UriKind.Relative));
            navigationService.Configure("ContactPeople", new Uri("../View/CustomerView/ContactPeopleView.xaml", UriKind.Relative));
            #endregion

            #region JobViews
            navigationService.Configure("Jobs", new Uri("../View/JobsWindow.xaml", UriKind.Relative));
            navigationService.Configure("JobInfo", new Uri("../View/JobInfoView.xaml", UriKind.Relative));

            navigationService.Configure("ContactPersons", new Uri("../View/CustomerView/ContactPersonListView.xaml", UriKind.Relative)); 
            #endregion

            SimpleIoc.Default.Register<NavigationService>(() => navigationService);

        }

        // Singleton repos
        public CustomerRepository CustomerRepo => ServiceLocator.Current.GetInstance<CustomerRepository>();

        // Viewmodels used for datacontext
        #region Singleton VM's
        public MainViewModel Main => ServiceLocator.Current.GetInstance<MainViewModel>();
        public DashboardViewModel Dashboard => ServiceLocator.Current.GetInstance<DashboardViewModel>(); 
        #endregion

        #region CustomerVM's
        public CustomerListViewModel CustomerList => new CustomerListViewModel(SimpleIoc.Default.GetInstance<NavigationService>());
        public AddContactInfoViewModel AddContactInfo => new AddContactInfoViewModel(SimpleIoc.Default.GetInstance<NavigationService>());
        public AddContactPersonViewModel AddContactPerson => new AddContactPersonViewModel(SimpleIoc.Default.GetInstance<NavigationService>());
        public AddCustomerInfoViewModel AddCustomerInfo => new AddCustomerInfoViewModel(SimpleIoc.Default.GetInstance<NavigationService>());
        public ContactPersonListViewModel ContactPersons => new ContactPersonListViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), CustomerRepo);
        public CustomerInfoViewModel CustomerInfo => new CustomerInfoViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), CustomerRepo);
        public CustomerInfoViewModel CustomerInfo => new CustomerInfoViewModel(SimpleIoc.Default.GetInstance<NavigationService>());
        #endregion

        #region JobsVM's
        public JobListViewModel JobList => new JobListViewModel(SimpleIoc.Default.GetInstance<NavigationService>());
        public JobInfoViewModel JobInfo => new JobInfoViewModel(SimpleIoc.Default.GetInstance<NavigationService>());
        #endregion

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}