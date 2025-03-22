using InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs;

namespace InterviewManagementSystem.Application.Validations.UserValidations
{
    public sealed class CandidateCreateValidator : AbstractValidator<CandidateCreateDTO>
    {
        public CandidateCreateValidator() : base()
        {
            //Include(new BaseUserDTOValidator<CandidateForCreateDTO>());
        }
    }

}
