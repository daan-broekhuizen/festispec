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
            SimpleIoc.Default.Register<MainViewModel>();
            SetupNavigation();
        }

        private static void SetupNavigation()
        {
            var navigationService = new NavigationService();
            navigationService.Configure("Dashboard", new Uri("../View/TestView.xaml", UriKind.Relative));
            navigationService.Configure("Customers", new Uri("../View/CustomersWindow.xaml", UriKind.Relative));
            navigationService.Configure("AddCustomerInfo", new Uri("../View/CustomerView/AddCustomerInfoView.xaml", UriKind.Relative));
            navigationService.Configure("AddContactInfo", new Uri("../View/CustomerView/AddContactInfoView.xaml", UriKind.Relative));
            navigationService.Configure("AddContactPerson", new Uri("../View/CustomerView/AddContactPersonView.xaml", UriKind.Relative));

            SimpleIoc.Default.Register<NavigationService>(() => navigationService);
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public CustomerListViewModel CustomerList => new CustomerListViewModel();
        public AddContactInfoViewModel AddContactInfo => new AddContactInfoViewModel(SimpleIoc.Default.GetInstance<NavigationService>());
        public AddContactPersonViewModel AddContactPerson => new AddContactPersonViewModel(SimpleIoc.Default.GetInstance<NavigationService>());
        public AddCustomerInfoViewModel AddCustomerInfo => new AddCustomerInfoViewModel(SimpleIoc.Default.GetInstance<NavigationService>());

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}