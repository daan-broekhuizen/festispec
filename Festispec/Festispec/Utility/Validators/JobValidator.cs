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
        }
    }
}
