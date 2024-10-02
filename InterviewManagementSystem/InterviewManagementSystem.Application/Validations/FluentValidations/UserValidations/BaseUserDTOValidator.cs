using InterviewManagementSystem.Application.DTOs.UserDTOs;

namespace InterviewManagementSystem.Application.Validations.FluentValidations.UserValidations;

public class BaseUserDTOValidator<T> : AbstractValidator<T> where T : BaseUserDTO
{

    public BaseUserDTOValidator()
    {

        RuleFor(s => s.CreatedBy).NotEmpty().WithMessage("Creator must not empty");
        RuleFor(s => s.DepartmentId).NotEmpty().WithMessage("Department can not empty");
        RuleFor(s => s.RoleId).NotEmpty().WithMessage("Role must not empty");
        RuleFor(s => s.Address).NotEmpty().WithMessage("Address must not empty");
        RuleFor(s => s.Username).NotEmpty().WithMessage("Username name must not empty");



        RuleFor(s => s.Dob)
            .NotNull().WithMessage("Date of birth must not empty")
            .LessThan(DateTime.Now).WithMessage("Date of Birth must be in the past.")
            .Must(BeAValidDate).WithMessage("Birthdate must be a valid date.");



        RuleFor(x => x.IsActive)
           .NotNull().WithMessage("Status cannot be empty.")
           .Must(value => (value == true || value == false))
           .WithMessage("Status must have a valid value (true or false).");


        RuleFor(x => x.Gender)
          .NotNull().WithMessage("Gender cannot be empty.")
          .Must(value => (value == true || value == false))
          .WithMessage("Gender must have a valid value (true or false).");


        RuleFor(s => s.Email)
            .NotNull().NotEmpty().WithMessage("Email must not empty")
            .EmailAddress().WithMessage("Invalid email format");



        /*
        RuleFor(s => s.PhoneNumber)
            .NotEmpty().WithMessage("Phone number must not empty")
            .Matches(@"^\d{11,13}$").WithMessage("Invalid phone number format");
        */


    }



    private static bool BeAValidDate(DateTime date)
    {
        return DateTime.TryParse(date.ToString(), out _);
    }


    private static bool BeAValidDateWithFormat(DateTime date)
    {
        return DateTime.TryParseExact(date.ToString(), "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.None, out _);
    }
}
