using InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs;

namespace InterviewManagementSystem.Application.Validations.UserValidations;

public sealed class UserUpdateValidator : BaseUserDTOValidator<UserUpdateDTO>
{
    public UserUpdateValidator() : base()
    {
        RuleFor(x => x.Id)
       .NotEmpty().WithMessage("Id is required.");

        RuleFor(x => x.RoleId)
            .NotEmpty().WithMessage("RoleId is required.");

        RuleFor(x => x.IsActive)
            .NotNull().WithMessage("IsActive must be specified.");

        RuleFor(x => x.DepartmentId)
            .IsInEnum().WithMessage("Invalid DepartmentId.");

    }
}
