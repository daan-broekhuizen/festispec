using Festispec.ViewModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Utility.Validators
{
    public class RegisterValidator : AbstractValidator<AccountViewModel>
    {
        public RegisterValidator()
        {
            RuleFor(c => c.City).NotEmpty().WithMessage("Voer een stad in.");
            RuleFor(c => c.StreetName).NotEmpty().WithMessage("Voer een straat in.");
            RuleFor(c => c.Username).NotEmpty().WithMessage("Voer een gebruikersnaam in.");
            RuleFor(c => c.HouseNumber).NotEmpty().WithMessage("Voer een huisnummer in.");
            RuleFor(c => c.FirstName).NotEmpty().WithMessage("Voer een voornaam in.");
            RuleFor(c => c.Infix).NotEmpty().WithMessage("Voer een tussenvoegsel in.");
            RuleFor(c => c.LastName).NotEmpty().WithMessage("Voer een achternaam in.");
            RuleFor(c => c.Password).NotEmpty().WithMessage("Voer een wachtwoord in.");

            RuleFor(c => c.Username).Must(IsValidLength).WithMessage("Maak de gebruikersnaam niet langer dan 25 karakters.");
            RuleFor(c => c.Password).Must(IsValidLength).WithMessage("Maak het wachtwoord niet langer dan 25 karakters.");
        }

        private bool IsValidLength(string arg)
        {
            if (arg == null || arg.Length > 25)
                return false;
            else
                return true;
        }
    }
}
