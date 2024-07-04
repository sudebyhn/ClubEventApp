using AppService.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.Validators
{
    public class CreateClubValidation : AbstractValidator<CreateClubRequestDto>
    {
        public CreateClubValidation()
        {
            RuleFor(x => x.ClubName).NotEmpty().WithMessage("Please enter a club name");
            RuleFor(x => x.FacultyName).NotEmpty().WithMessage("Please enter faculty name for related club");
            RuleFor(x => x.FacultyName).MaximumLength(100).WithMessage("Please enter shorter name for the club");

        }
    }
}
