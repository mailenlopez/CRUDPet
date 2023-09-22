using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.User.Commands.CreateUser
{
    public class CreateUserCommandValidator : AbstractValidator<CreateUserCommand>
    {
        public CreateUserCommandValidator()
        {
            RuleFor(u => u.Email).NotEmpty().WithMessage("Email is required.")
                .MaximumLength(50).WithMessage("Email exceeds the maximum of 50 characters.");

            RuleFor(u => u.Name).NotEmpty().WithMessage("Name is required.")
                .MaximumLength(50).WithMessage("Name exceeds the maximum of 50 characters.");

            RuleFor(u => u.Password).NotEmpty().WithMessage("Password can not be empty.")
                .MaximumLength(50).WithMessage("Password exceeds the maximum of 50 characters.");
        }
    }
}
