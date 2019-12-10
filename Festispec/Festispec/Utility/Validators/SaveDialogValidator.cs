using Festispec.ViewModel.Components;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Festispec.Utility.Validators
{
    public class SaveDialogValidator : AbstractValidator<SaveDialogViewModel>
    {
        public SaveDialogValidator()
        {
            RuleFor(x => x.Name).NotEmpty().WithMessage("Voer een naam in.");
        }
    }
}
