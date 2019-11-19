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
        public CustomerListViewModel CustomerList => new CustomerListViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), CustomerRepo);
        public AddContactInfoViewModel AddContactInfo => new AddContactInfoViewModel(ServiceLocator.Current.GetInstance<NavigationService>());
        public AddContactPersonViewModel AddContactPerson => new AddContactPersonViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), CustomerRepo);
        public AddCustomerInfoViewModel AddCustomerInfo => new AddCustomerInfoViewModel(ServiceLocator.Current.GetInstance<NavigationService>());
        public ContactPersonListViewModel ContactPersons => new ContactPersonListViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), CustomerRepo);
        public CustomerInfoViewModel CustomerInfo => new CustomerInfoViewModel(ServiceLocator.Current.GetInstance<NavigationService>(), CustomerRepo);
        #endregion

        //Clean when logging out?
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}