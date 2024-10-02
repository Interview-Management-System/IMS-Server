using InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs;

namespace InterviewManagementSystem.Application.Validations.FluentValidations.UserValidations
{
    public sealed class UserForCreateValidator : AbstractValidator<UserForCreateDTO>
    {
        public UserForCreateValidator() : base()
        {
            Include(new BaseUserDTOValidator<UserForCreateDTO>());
        }
    }
}
