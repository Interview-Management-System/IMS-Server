using InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs;
using InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs;

namespace InterviewManagementSystem.Application.Features.UserFeature.UseCases;

public sealed class UserRetrieveUseCase : BaseUseCase
{


    public UserRetrieveUseCase(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }



    internal async Task<ApiResponse<List<UserForRetrieveDTO>>> GetListAsync()
    {
        var listUser = await _unitOfWork.AppUserRepository.GetAllAsync();
        var newListUser = _mapper.Map<List<UserForRetrieveDTO>>(listUser);


        return new ApiResponse<List<UserForRetrieveDTO>>()
        {
            Data = newListUser,
            Message = "Get user list successful"
        };
    }




    internal async Task<ApiResponse<UserForRetrieveDTO>> GetUserByIdAsync(Guid id)
    {
        var userFoundById = await _unitOfWork.AppUserRepository.GetByIdAsync(id);

        ArgumentNullException.ThrowIfNull(userFoundById, "User not found");
        ApplicationException.ThrowIfGetDeletedRecord(userFoundById.IsDeleted);


        var mappedUser = _mapper.Map<UserForRetrieveDTO>(userFoundById);


        return new ApiResponse<UserForRetrieveDTO>()
        {
            Data = mappedUser,
            Message = "User found"
        };
    }





    internal async Task<ApiResponse<CandidateForRetrieveDTO>> GetCandidateByIdAsync(Guid id)
    {
        var candidateFoundById = await _unitOfWork.CandidateRepository.GetByIdAsync(id);


        ArgumentNullException.ThrowIfNull(candidateFoundById, "Candidate not found");
        ApplicationException.ThrowIfGetDeletedRecord(candidateFoundById.IsDeleted);


        var mappedUser = _mapper.Map<CandidateForRetrieveDTO>(candidateFoundById);


        return new ApiResponse<CandidateForRetrieveDTO>()
        {
            Data = mappedUser,
            Message = "Candidate found"
        };
    }
}
