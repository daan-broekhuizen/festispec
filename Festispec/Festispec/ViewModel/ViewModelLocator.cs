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
using Festispec.ViewModel.Graph;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Ioc;

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

            SimpleIoc.Default.Register<MainViewModel>();
        }

        public MainViewModel Main
        {
            get
            {
                return ServiceLocator.Current.GetInstance<MainViewModel>();
            }
        }

        public GraphViewModel Graph
        {
            get
            {
                return new GraphViewModel();
            }
        }
        
        public SalesGraphViewModel SaleGraph
        {
            get
            {
                return new SalesGraphViewModel();
            }
        }

        public InspectorGraphViewModel InspectorGraph
        {
            get
            {
                return new InspectorGraphViewModel();
            }
        }

        public OffertesGraphViewModel OfferteGraph
        {
            get
            {
                return new OffertesGraphViewModel();
            }
        }

        public SalesMapGraphViewModel SalesMapGraph
        {
            get
            {
                return new SalesMapGraphViewModel();
            }
        }

        public static void Cleanup()
        {
            // TODO Clear the ViewModels
        }
    }
}