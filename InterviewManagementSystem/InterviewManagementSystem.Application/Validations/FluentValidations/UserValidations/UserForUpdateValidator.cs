using InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs;

namespace InterviewManagementSystem.Application.Validations.FluentValidations.UserValidations
{
    public sealed class UserForUpdateValidator : AbstractValidator<UserForUpdateDTO>
    {
        public UserForUpdateValidator() : base()
        {
            Include(new BaseUserDTOValidator<UserForUpdateDTO>());
            RuleFor(a => a.Id)
                .NotEmpty().NotNull().WithMessage("Id must not empty")
                .Must(id => BeAValidGuid(id.ToString())).WithMessage("Id must be a valid GUID."); ;
        }




        private static bool BeAValidGuid(string id)
        {
            return Guid.TryParse(id, out _);
        }
    }
}
