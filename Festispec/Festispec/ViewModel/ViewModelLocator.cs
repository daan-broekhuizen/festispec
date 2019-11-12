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
        public ViewModelLocator()
        {
            ServiceLocator.SetLocatorProvider(() => SimpleIoc.Default);

            SetupNavigation();
            SimpleIoc.Default.Register<MainViewModel>();
            SimpleIoc.Default.Register<DashboardViewModel>();
        }

        private static void SetupNavigation()
        {
            NavigationService navigationService = new NavigationService();
            navigationService.Configure("Dashboard", new Uri("../View/DashboardView.xaml", UriKind.Relative));
            SimpleIoc.Default.Register<NavigationService>(() => navigationService);

        }

        public MainViewModel Main
        {
            get
            {
                MainViewModel main = ServiceLocator.Current.GetInstance<MainViewModel>();
                SimpleIoc.Default.GetInstance<NavigationService>().NavigateTo("Dashboard");
                return main;
            }
        }

        public DashboardViewModel Dashboard
        {
            get
            {
                return ServiceLocator.Current.GetInstance<DashboardViewModel>();
            }
        }
        
        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}