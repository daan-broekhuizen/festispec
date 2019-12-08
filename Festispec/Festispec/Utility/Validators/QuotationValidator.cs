using Festispec.ViewModel.QuotationViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Utility.Validators
{
    public class QuotationValidator : AbstractValidator<QuotationViewModel>
    {
        public QuotationValidator()
        {
            RuleFor(q => q.Description).NotEmpty().WithMessage("Voer een omschrijving in.");
            RuleFor(q => q.Price).NotEmpty().WithMessage("Voer een totaalprijs in.");
            RuleFor(q => q.Price).Must(IsValidPrice).WithMessage("Voer een geldige prijs in, bv. 10,50.");
        }

        private bool IsValidPrice(string arg)
        {
            if (arg == null) return false;
            string trimmed = arg.Trim('€');
            return Decimal.TryParse(trimmed, out _);
        }
    }
}
