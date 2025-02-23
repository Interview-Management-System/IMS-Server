using InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs;

namespace InterviewManagementSystem.Application.Validations.FluentValidations.UserValidations
{
    public sealed class UserForCreateValidator : AbstractValidator<UserCreateDTO>
    {
        public UserForCreateValidator() : base()
        {
            Include(new BaseUserDTOValidator<UserCreateDTO>());
        }
    }
}
