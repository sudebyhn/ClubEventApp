using AppService.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.Validators
{
    public class UpdateClubMenagerValidation:AbstractValidator<UpdateClubMenagerRequestDto>
    {
        public UpdateClubMenagerValidation()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Please enter email to create account");
            RuleFor(x => x.Email).EmailAddress().WithMessage("Please enter valid email adress");
            RuleFor(x => x.Name).MaximumLength(15).WithMessage("Please enter shorter input for name");
            RuleFor(x => x.Surname).MaximumLength(15).WithMessage("Please enter shorter input for name");
            RuleFor(x => x.Name).NotEmpty().WithMessage("Please enter name to create account");
            RuleFor(x => x.Surname).NotEmpty().WithMessage("Please enter surname to create account");
            RuleFor(x => x.Password).MinimumLength(6).WithMessage("Minimum password length is 6");
            RuleFor(x => x.Password).MaximumLength(10).WithMessage("Maximum password length is 10");
        }
    }
}
