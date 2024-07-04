using AppService.Dto;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppService.Validators
{
    public class CreateEventValidation : AbstractValidator<CreateEventRequestDto>
    {
        public CreateEventValidation()
        {
            RuleFor(x => x.Title).NotEmpty().WithMessage("Please enter a title");
            RuleFor(x => x.Text).NotEmpty().WithMessage("Please enter a text");
            RuleFor(x => x.EventDate).NotEmpty().WithMessage("Please enter a event date");
            RuleFor(x => x.FacultyName).NotEmpty().WithMessage("Please enter the faculty name");
            RuleFor(x => x.Text).NotEmpty().WithMessage("Please enter a text");
        }
    }
}
