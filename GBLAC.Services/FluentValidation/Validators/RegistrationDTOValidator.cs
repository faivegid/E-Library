using FluentValidation;
using GBLAC.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBLAC.Services.FluentValidation.Validators
{
    public class RegistrationDTOValidator : AbstractValidator<RegisterationDTO>
    {
        public RegistrationDTOValidator()
        {
            RuleFor(request => request.Email).NotEmpty().EmailAddress();
            RuleFor(request => request.FirstName).HumanName();
            RuleFor(request => request.LastName).HumanName();
            RuleFor(request => request.Password).Password();
            RuleFor(request => request.ConfirmPassword).Equal(request => request.Password).WithMessage("Password does not Match");
        }
    }
}
