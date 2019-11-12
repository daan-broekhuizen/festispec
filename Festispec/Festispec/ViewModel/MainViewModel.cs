using FestiSpec.Domain;
using FestiSpec.Domain.Repositories;
using GalaSoft.MvvmLight;
using System;
using System.Diagnostics;
using System.Windows;

namespace Festispec.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        /// <summary>
        /// Initializes a new instance of the MainViewModel class.
        /// </summary>
        public MainViewModel()
        {
            //Account acc = new Account()
            //{ Wachtwoord = "123", Gebruikersnaam = "Dummy"};

            //UserRepository repo = new UserRepository();
            //Debug.WriteLine(repo.Login(acc) + " ingelogd als: " + acc.Gebruikersnaam);

            PlanningViewModel c = new PlanningViewModel();
            _ = c.CalculateDistance();
            
        }
    }
}