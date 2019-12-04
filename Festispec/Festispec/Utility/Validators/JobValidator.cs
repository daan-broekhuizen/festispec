using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Festispec.ViewModel;
using FestiSpec.Domain.Repositories;
using FluentValidation;

namespace Festispec.Utility.Validators
{
    public class JobValidator : AbstractValidator<JobViewModel>
    {
        private JobRepository _jobRepositiory;

        public JobValidator()
        {
            _jobRepositiory = new JobRepository();
            RuleFor(x => x.JobName).NotEmpty().WithMessage("Voer een opdrachtnaam in.");
            RuleFor(x => x.CustomerName).NotEmpty().WithMessage("Selecteer een klant.");
            RuleFor(x => x.JobName).MaximumLength(45).WithMessage("Voer een geldig opdracht naam in (45 karakters).");
            RuleFor(x => x.Status).NotEmpty().WithMessage("Selecteer een status.");
            RuleFor(x => x.StartDatum).NotEmpty().WithMessage("Selecteer een datum.");
            RuleFor(x => x.EindDatum).NotEmpty().WithMessage("Selecteer een datum.");
            RuleFor(x => x.CustomerWishes).NotEmpty().WithMessage("Voer klantenwensen in.");
            RuleFor(x => x.EindDatum).GreaterThanOrEqualTo(x => x.StartDatum).WithMessage("EindDatum mag niet voor de startdatum liggen.");
            RuleFor(x => x.StartDatum).LessThanOrEqualTo(x => x.EindDatum).WithMessage("Startdatum mag niet na de einddatum liggen.");
        }
    }
}
