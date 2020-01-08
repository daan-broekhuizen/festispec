using Festispec.ViewModel.InspectionFormViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Utility.Validators
{
    public class PossibleAnwserValidator : AbstractValidator<PossibleAnwserViewModel>
    {
        public PossibleAnwserValidator()
        {
            RuleFor(x => x.AnwserTextString).NotEmpty().WithMessage("Alle mogelijke antwoorden moeten ingevuld zijn.");
        }
    }
}
