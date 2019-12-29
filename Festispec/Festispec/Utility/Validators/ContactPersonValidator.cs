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
            RuleFor(x => x.Email).NotEmpty().WithMessage("Voer een email adres in");
            RuleFor(x => x.Telephone).Must(IsValidTelephone).WithMessage("Voer een geldig telefoonnummer in (0612345678).");
        }

        private bool IsValidTelephone(string arg)
        {
            if (arg == null) return false;
            return arg.All(char.IsDigit) && arg.Length == 10;
        }
    }
}
