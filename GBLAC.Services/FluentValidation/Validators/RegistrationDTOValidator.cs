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
            RuleFor(request => request.Email)
                .NotEmpty()
                .EmailAddress();
            RuleFor(request => request.FirstName)
                .NotEmpty();
            RuleFor(request => request.LastName)
                .NotEmpty();
            RuleFor(request => request.Password)
                .NotEmpty();
            RuleFor(request => request.ConfirmPassword)
                .NotEmpty()
                .Equal(request => request.Password).WithMessage("Password does not Match");
        }
    }
}
