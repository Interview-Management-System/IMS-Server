using InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs;

namespace InterviewManagementSystem.Application.Validations.FluentValidations.UserValidations
{
    public sealed class CandidateForCreateValidator : AbstractValidator<CandidateCreateDTO>
    {
        public CandidateForCreateValidator() : base()
        {
            //Include(new BaseUserDTOValidator<CandidateForCreateDTO>());
        }
    }

}
