using Festispec.ViewModel.InspectionFormViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Utility.Validators
{
    public class InspectionFormValidator : AbstractValidator<InspectionFormViewModel>
    {
        public InspectionFormValidator()
        {
            RuleFor(x => x.Titel).NotEmpty().WithMessage("Titel mag niet leeg zijn");
        }
    }
}
