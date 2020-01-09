using Festispec.ViewModel;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
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
            RuleFor(x => x.HouseNumber).MaximumLength(4).WithMessage("Huisnummer + toevoeging mag max. 4 lang zijn");
            RuleFor(c => c.FirstName).NotEmpty().WithMessage("Voer een voornaam in.");
            RuleFor(c => c.LastName).NotEmpty().WithMessage("Voer een achternaam in.");
            RuleFor(c => c.Password).NotEmpty().WithMessage("Voer een wachtwoord in.");
            RuleFor(c => c.PhoneNumber).NotEmpty().WithMessage("Voer een telefoonnummer in.");
            RuleFor(c => c.PhoneNumber).Length(10).WithMessage("Voer een geldig telefoonnummer in (10 cijfers).");
            RuleFor(c => c.PhoneNumber).Must(IsNumericalSequence).WithMessage("Voer een geldig telefoonnummer in (10 cijfers).");
            RuleFor(c => c.Username).Must(IsValidLength).WithMessage("Maak de gebruikersnaam niet langer dan 25 karakters.");
            RuleFor(c => c.Password).Must(IsValidLength).WithMessage("Maak het wachtwoord niet langer dan 25 karakters.");
            RuleFor(c => c.Email).NotEmpty().WithMessage("Voer een email in.");
            RuleFor(c => c.Email).Must(IsValidEmail).WithMessage("Ongeldig email adres (a@b.c)");
            RuleFor(c => c.Email).MaximumLength(120).WithMessage("Email te lang (max 130).");
            RuleFor(c => c.IBAN).NotEmpty().WithMessage("Voer een IBAN in.");
            RuleFor(c => c.Username).MaximumLength(45).WithMessage("Gebruikersnaam te lang (max 45).");
            RuleFor(c => c.Password).MaximumLength(45).WithMessage("Wachtwoord te lang (max 45).");
            RuleFor(c => c.FirstName).MaximumLength(30).WithMessage("Voornaam te lang (max 30).");
            RuleFor(c => c.LastName).MaximumLength(30).WithMessage("Achternaam te lang (max 30).");
            RuleFor(c => c.City).MaximumLength(50).WithMessage("Stadsnaam te lang (max 50).");
            RuleFor(c => c.StreetName).MaximumLength(50).WithMessage("Straatnaam te lang (max 50).");
            RuleFor(c => c.IBAN).MaximumLength(18).WithMessage("IBAN te lang (max 18).");
            RuleFor(c => c.Infix).MaximumLength(15).WithMessage("Tussenvoegsels te lang (max 15).");
            RuleFor(x => x.PostalCode).Must(IsValidPostalCode).WithMessage("Adres is ongeldig (postcode niet gevonden).");

        }

        private bool IsValidLength(string arg)
        {
            if (arg == null || arg.Length > 25)
                return false;
            else
                return true;
        }

        private bool IsNumericalSequence(string arg)
        {
            if (arg == null) return false;
            return arg.All(char.IsDigit);
        }

        private bool IsValidEmail(string arg)
        {
            if (arg == null) return false;
            string regex = @"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z";
            return Regex.IsMatch(arg, regex, RegexOptions.IgnoreCase);
        }

        private bool IsValidPostalCode(string arg)
        {
            if (arg == null) return false;
            return Regex.IsMatch(arg, @"\d{4}[ ][A-Z]{2}");
        }
    }
}
