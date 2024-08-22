
using BusinessLayer.DTOs;
using FluentValidation;

namespace BusinessLayer.Validator
{
    public class UserRegisterValidator : AbstractValidator<UserRegister>
    {
        public UserRegisterValidator()
        {
            RuleFor(t => t.Email)
                .NotEmpty().WithMessage("Email is required")
                .EmailAddress().WithMessage("Invalid email address!")
                .MaximumLength(100);

            RuleFor(t => t.Password)
                .NotEmpty().WithMessage("Password is required")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters !");

            RuleFor(t => t.FirstName)
               .NotEmpty().WithMessage("Please Enter Your First Name!");

            RuleFor(t => t.LastName)
               .NotEmpty().WithMessage("Please Enter Your Last Name!");

            RuleFor(t => t.MobileNumber)
                .NotEmpty().WithMessage("Enter your mobile number");

            RuleFor(t => t.DateOfBirth)
                .NotEmpty();

            RuleFor(t => t.Address)
                .NotEmpty();
        }
    }
}
