using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using Festispec.Model;
using Festispec.Service;
using Festispec.Utility.Validators;
using FestiSpec.Domain.Repositories;
using FluentValidation.Results;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;

namespace Festispec.ViewModel
{
    public class AccountInfoViewModel : ViewModelBase
    {
        private NavigationService navigationService;
        private UserRepository userRepo;
        public AccountViewModel AccountVM { get; set; }
        public List<string> Rol { get; set; }

        public ICommand ShowUserInfoCommand { get; set; }
        public ICommand SaveAccountInfoCommand { get; set; }
        public AccountInfoViewModel(NavigationService navigationService, UserRepository userRepo)
        {
            this.navigationService = navigationService;
            this.userRepo = userRepo;

            if (navigationService.Parameter is AccountViewModel)
                AccountVM = navigationService.Parameter as AccountViewModel;

            Rol = new List<string>();
            Rol = userRepo.GetRols().Select(e => e.Betekenis).ToList();

            ShowUserInfoCommand = new RelayCommand(ShowUserInfo);
            SaveAccountInfoCommand = new RelayCommand(CanSaveAccount);
        }

        private void ShowUserInfo() => navigationService.NavigateTo("UserInfo", AccountVM);

        private void CanSaveAccount()
        {
            ValidationResult result = new RegisterValidator().Validate(AccountVM);
            //Get validation errors
            if (result.Errors.Count() == 0)
            {
                //if validated update account
                userRepo.UpdateAccount(new Account()
                {
                    AccountID = AccountVM.Id,
                    Gebruikersnaam = AccountVM.Username,
                    Wachtwoord = AccountVM.Password,
                    Rol = AccountVM.Role,
                    DatumCertificering = AccountVM.DateOfCertification,
                    EinddatumCertificering = AccountVM.EndDateOfCertification,
                    Voornaam = AccountVM.FirstName,
                    Tussenvoegsel = AccountVM.Infix,
                    Achternaam = AccountVM.LastName,
                    Straatnaam = AccountVM.StreetName,
                    Stad = AccountVM.City,
                    Huisnummer = AccountVM.HouseNumber,
                    Telefoonnummer = AccountVM.PhoneNumber,
                    Email = AccountVM.Email,
                    IBAN = AccountVM.IBAN,
                    LaatsteWijziging = DateTime.Now
                });

                Messenger.Default.Send("Wijzigingen opgeslagen", this.GetHashCode());

            }
            else
            {
                //Get error messages as string
                string message = "";
                foreach (ValidationFailure failure in result.Errors)
                    message += (failure.ErrorMessage + "\n");
                //Use messenger to send error message to view
                //(Hashcode to match view and viewmodel - see code behind)
                Messenger.Default.Send(message, this.GetHashCode());
            }

        }

    }
}
