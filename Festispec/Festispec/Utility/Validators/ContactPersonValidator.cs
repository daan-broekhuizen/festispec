using Festispec.ViewModel;
using FestiSpec.Domain.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Validators
{
    public class ContactPersonValidator : AbstractValidator<ContactPersonViewModel>
    {
        public ContactPersonValidator()
        {
            RuleFor(x => x.FirstName).NotEmpty().WithMessage("Voer een voornaam in.");
            RuleFor(x => x.FirstName).MaximumLength(30).WithMessage("Voornaam te lang (max 30).");
            RuleFor(x => x.LastName).NotEmpty().WithMessage("Voer een achternaam in.");
            RuleFor(x => x.LastName).MaximumLength(30).WithMessage("Achternaam te lang (max 30).");
            RuleFor(x => x.Infix).MaximumLength(15).WithMessage("Tussenvoegsel te lang (max 15).");
            RuleFor(x => x.Role).MaximumLength(30).WithMessage("Rol te lang (max 30).");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Voer een email adres in");
            RuleFor(x => x.Email).MaximumLength(130).WithMessage("Email te lang (max 130).");
            RuleFor(x => x.Telephone).Must(IsValidTelephone).WithMessage("Voer een geldig telefoonnummer in (0612345678).");
        }

        private bool IsValidTelephone(string arg)
        {
            if (arg == null) return false;
            return arg.All(char.IsDigit) && arg.Length == 10;
        }
    }
}
