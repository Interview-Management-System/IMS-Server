using InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs;

namespace InterviewManagementSystem.Application.Validations.FluentValidations.UserValidations
{
    public sealed class CandidateForUpdateValidator : AbstractValidator<CandidateForUpdateDTO>
    {
        public CandidateForUpdateValidator()
        {
            Include(new BaseUserDTOValidator<CandidateForUpdateDTO>());
        }
    }
}
