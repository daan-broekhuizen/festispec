using Festispec.Model;
using Festispec.ViewModel;
using FestiSpec.Domain;
using FestiSpec.Domain.Repositories;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Validators
{
    public class CustomerValidator : AbstractValidator<CustomerViewModel>
    {
        public CustomerValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Voer een naam in.");
            RuleFor(x => x.Name).MaximumLength(45).WithMessage("Klantnaam te lang (max 45).");
            RuleFor(x => x.KvK).NotEmpty().WithMessage("Voer een KvK nummer in.");
            RuleFor(x => x.KvK).Must(IsNumericalSequence).Length(8).WithMessage("Voer een geldig KvK nummer in (8 cijfers).");
            RuleFor(x => x.Branchnumber).Must(IsNumericalSequence).Length(12).WithMessage("Voer een geldig vestigingsnummer in (12 cijfers).");
            RuleFor(x => x.Streetname).NotEmpty().WithMessage("Voer een straatnaam in.");
            RuleFor(x => x.Streetname).MaximumLength(50).WithMessage("Straatnaam te lang (max 50).");
            RuleFor(x => x.HouseNumber).NotEmpty().WithMessage("Voer een huisnummer in.");
            RuleFor(x => x.HouseNumber).MaximumLength(4).WithMessage("Huisnummer + toevoeging mag max. 4 lang zijn");
            RuleFor(x => x.City).NotEmpty().WithMessage("Voer een plaastnaam in.");
            RuleFor(x => x.City).MaximumLength(50).WithMessage("Plaatsnaam te lang (max 50).");
            RuleFor(x => x.PostalCode).Must(IsValidPostalCode).WithMessage("Address is ongeldig, zie postcode.");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Voer een email adres in.");
            RuleFor(x => x.Email).MaximumLength(130).WithMessage("Email te lang (max 130).");
            RuleFor(x => x.Website).MaximumLength(100).WithMessage("Website te lang (max 100).");
            RuleFor(x => x.Telephone).Length(10).WithMessage("Voer een geldig telefoonnummer in (10 cijfers).");
            RuleFor(x => x.Telephone).Must(IsNumericalSequence).WithMessage("Voer een geldig telefoonnummer in (0612345678).");
        }


        private bool IsNumericalSequence(string arg)
        {
            if (arg == null) return false;
            return arg.All(char.IsDigit);
        }

        private bool IsValidPostalCode(string arg)
        {
            if (arg == null) return false;
            return arg.Length == 7 && arg.Contains(" ");
        }

    }
}
