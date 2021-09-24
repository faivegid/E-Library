using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GBLAC.Services.FluentValidation.Validators
{
    public static class ValidationRUleExtension
    {
        public static void Password<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder.NotNull().WithMessage("Password is required")
             .NotEmpty()
             .MinimumLength(6).WithMessage("Password must contain at least 6 characters")
             .Matches("[A-Z]").WithMessage("Password must contain atleast 1 uppercase letter")
             .Matches("[a-z]").WithMessage("Password must contain atleast 1 lowercase letter")
             .Matches("[0-9]").WithMessage("Password must contain a number")
             .Matches(@"[!@ #$%^&*-.,/\~]").WithMessage("Password must contain a special character")
             .Matches("[^a-zA-Z0-9]").WithMessage("Password must contain non alphanumeric");
        }

        public static void HumanName<T>(this IRuleBuilder<T, string> ruleBuilder)
        {
            ruleBuilder.NotNull().WithMessage("Name cannot be null")
              .NotEmpty().WithMessage("Name must be provided")
              .Matches("[A-Za-z]").WithMessage("Name can only contain alphabeths")
              .MinimumLength(2).WithMessage("Name is limited to a minimum of 2 characters")
              .MaximumLength(25).WithMessage("Name is limited to a maximum of 25 characters");
        }
    }
}
