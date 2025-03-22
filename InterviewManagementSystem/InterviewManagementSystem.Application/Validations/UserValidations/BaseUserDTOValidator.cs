using InterviewManagementSystem.Application.DTOs.UserDTOs;

namespace InterviewManagementSystem.Application.Validations.UserValidations
{
    public class BaseUserDTOValidator<T> : AbstractValidator<T> where T : BaseUserDTO
    {
        public BaseUserDTOValidator()
        {
            RuleFor(x => x.PersonalInformation)
                .NotNull().WithMessage("Personal Information is required.");

            RuleFor(x => x.PersonalInformation.Email)
                .NotEmpty().WithMessage("Email is required.")
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.PersonalInformation.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MinimumLength(3).WithMessage("Username must be at least 3 characters long.");

            RuleFor(x => x.PersonalInformation.PhoneNumber)
                .Matches(@"^\+?[1-9]\d{1,14}$")
                .WithMessage("Invalid phone number format.");

            RuleFor(x => x.PersonalInformation.Dob)
                .LessThan(DateTime.UtcNow).WithMessage("Date of birth must be in the past.");
        }
    }
}
