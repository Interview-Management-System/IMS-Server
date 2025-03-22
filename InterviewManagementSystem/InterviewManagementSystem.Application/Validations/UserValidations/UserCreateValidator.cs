using InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs;

namespace InterviewManagementSystem.Application.Validations.UserValidations;

public sealed class UserCreateValidator : BaseUserDTOValidator<UserCreateDTO>
{
    public UserCreateValidator()
    {
        RuleFor(x => x.RoleId)
            .NotEmpty().WithMessage("RoleId is required.");

        RuleFor(x => x.IsActive)
            .NotNull().WithMessage("IsActive must be specified.");

        RuleFor(x => x.DepartmentId)
            .IsInEnum().WithMessage("Invalid DepartmentId.");
    }
}
