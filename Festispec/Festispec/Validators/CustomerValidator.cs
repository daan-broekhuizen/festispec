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
        private CustomerRepository _customerRepository;
        public CustomerValidator()
        {
            _customerRepository = new CustomerRepository();
            RuleFor(x => x.Name).NotEmpty().WithMessage("Voer een naam in.");
            RuleFor(x => x.KvK).NotEmpty().WithMessage("Voer een KvK nummer in.");
            RuleFor(x => x.KvK).Length(8).WithMessage("Voer een geldig KvK nummer in (8 cijfers)");
            RuleFor(x => x.KvK).Must(IsNumericalSequence).WithMessage("Voer een geldig KvK nummer in (8 cijfers)");
            RuleFor(x => x.KvK).Must(IsUniqueKvK).WithMessage("KvK bestaat al.");
            RuleFor(x => x.HouseNumber).NotEmpty().WithMessage("Voer een huisnummer in.");
            RuleFor(x => x.PostalCode).Must(IsValidPostalCode).WithMessage("Voer een geldige postcode in (1234AB)");
            RuleFor(x => x.Email).NotEmpty().WithMessage("Voer een email adres in");
            RuleFor(x => x.Telephone).Length(10).WithMessage("Voer een geldig telefoonnummer in (0612345678)");
            RuleFor(x => x.Telephone).Must(IsNumericalSequence).WithMessage("Voer een geldigtelefoon nummer in (0612345678)");
            RuleFor(x => x.HouseNumber).NotEmpty().WithMessage("Voer een huisnummer in");
        }

        private bool IsNumericalSequence(string arg)
        {
            if (arg == null) return false;
            return arg.All(char.IsDigit);
        }

        private bool IsValidPostalCode(string arg)
        {
            if (arg == null) return false;
            return arg.Length == 6;
        }

        private bool IsUniqueKvK(string arg)
        {
            if (arg == null) return false;
            Klant klant = _customerRepository.GetCustomers().Where(c => c.KvK_nummer == arg).FirstOrDefault();
            return arg.All(char.IsDigit) && klant == null;
        }
    }
}
