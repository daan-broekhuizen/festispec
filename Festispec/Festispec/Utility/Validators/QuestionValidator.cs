using Festispec.ViewModel.InspectionFormViewModels;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Utility.Validators
{
    public class QuestionValidator : AbstractValidator<QuestionViewModel>
    {
        public QuestionValidator()
        {
            RuleFor(x => x.QuestionText).NotEmpty().WithMessage("Vraagstelling mag niet leeg zijn");
        }
    }
}
