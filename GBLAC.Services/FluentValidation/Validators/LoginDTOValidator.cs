using FluentValidation;
using GBLAC.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBLAC.Services.FluentValidation.Validators
{
    public class LoginDTOValidator : AbstractValidator<LoginDTO>
    {
        public LoginDTOValidator()
        {
            RuleFor(login => login.Email).NotEmpty().EmailAddress().WithMessage("Invalid Email format");
            RuleFor(login => login.Password).NotEmpty().WithMessage("Can't login without a password");
        }
    }
}
