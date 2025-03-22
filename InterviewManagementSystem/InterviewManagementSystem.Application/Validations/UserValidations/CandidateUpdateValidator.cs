using InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs;

namespace InterviewManagementSystem.Application.Validations.UserValidations
{
    public sealed class CandidateUpdateValidator : AbstractValidator<CandidateUpdateDTO>
    {
        public CandidateUpdateValidator()
        {
            //Include(new BaseUserDTOValidator<CandidateForUpdateDTO>());
        }
    }
}
