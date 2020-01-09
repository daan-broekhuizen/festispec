using Festispec.ViewModel.Components;
using FluentValidation;

namespace Festispec.Utility.Validators
{
    public class SaveDialogValidator : AbstractValidator<SaveDialogViewModel>
    {
        public SaveDialogValidator() => RuleFor(x => x.Name).NotEmpty().WithMessage("Voer een naam in.");
    }
}
