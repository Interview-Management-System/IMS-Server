using AutoMapper.QueryableExtensions;
using InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs;
using InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs;
using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.Enums.Extensions;
using InterviewManagementSystem.Domain.Paginations;

namespace InterviewManagementSystem.Application.Features.UserFeature.UseCases;

public sealed class UserRetrieveUseCase : BaseUserUseCase
{


    public UserRetrieveUseCase(IMapper mapper, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager) : base(mapper, unitOfWork, userManager, roleManager)
    {
    }



    internal async Task<ApiResponse<List<UserForRetrieveDTO>>> GetListAsync()
    {

        var listUser = await _userManager.Users.ToListAsync();
        var newListUser = _mapper.Map<List<UserForRetrieveDTO>>(listUser);


        foreach (var item in newListUser)
        {
            var userFoundById = await _userManager.FindByIdAsync(item.Id.ToString());
            var listRole = await _userManager.GetRolesAsync(userFoundById!);

            item.Role = listRole.FirstOrDefault();
        }


        return new ApiResponse<List<UserForRetrieveDTO>>()
        {
            Data = newListUser,
            Message = "Get user list successful"
        };
    }




    internal async Task<ApiResponse<List<CandidateForRetrieveDTO>>> GetCandidateListAsync()
    {

        var listUser = await _unitOfWork.CandidateRepository.GetAllAsync();
        var newListUser = _mapper.Map<List<CandidateForRetrieveDTO>>(listUser);


        foreach (var item in newListUser)
        {
            var userFoundById = await _userManager.FindByIdAsync(item.Id.ToString());
            var listRole = await _userManager.GetRolesAsync(userFoundById!);

            item.Role = listRole.FirstOrDefault();
        }


        return new ApiResponse<List<CandidateForRetrieveDTO>>()
        {
            Data = newListUser,
            Message = "Get user list successful"
        };
    }


    internal async Task<ApiResponse<PageResult<UserForRetrieveDTO>>> GetListUserPagingAsync(UserPaginatedSearchRequest request)
    {

        PaginationParameter<AppUser> paginationParameter = _mapper.Map<PaginationParameter<AppUser>>(request);

        RoleEnum? roleId = request.RoleId;


        if (roleId != null && roleId != RoleEnum.Default)
        {

            AppRole? role = await _roleManager.FindByIdAsync(roleId.Value.GetRoleId());
            ArgumentNullException.ThrowIfNull(role, "Role not found to filter");


            List<Guid> userWithRole = (await _userManager.GetUsersInRoleAsync(role.Name!)).Select(x => x.Id).ToList();
            paginationParameter.Filters.Add(x => userWithRole.Contains(x.Id));
        }

        IQueryable<UserForRetrieveDTO> queryModifier(IQueryable<AppUser> q) => q.ProjectTo<UserForRetrieveDTO>(_mapper.ConfigurationProvider);

        var pageResult = await _unitOfWork.AppUserRepository.GetPaginationList(paginationParameter, queryModifier: queryModifier);


        return new ApiResponse<PageResult<UserForRetrieveDTO>>()
        {
            Data = _mapper.Map<PageResult<UserForRetrieveDTO>>(pageResult),
            Message = "Get user list successful"
        };
    }


    internal async Task<ApiResponse<UserForRetrieveDTO>> GetUserByIdAsync(Guid id)
    {
        var userFoundById = await _unitOfWork.AppUserRepository.GetByIdAsync(id);

        ArgumentNullException.ThrowIfNull(userFoundById, "User not found");
        ApplicationException.ThrowIfGetDeletedRecord(userFoundById.IsDeleted);


        var mappedUser = _mapper.Map<UserForRetrieveDTO>(userFoundById);
        var listRole = await _userManager.GetRolesAsync(userFoundById!);

        mappedUser.Role = listRole.FirstOrDefault();


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

        var mappedCandidate = _mapper.Map<CandidateForRetrieveDTO>(candidateFoundById);

        return new ApiResponse<CandidateForRetrieveDTO>()
        {
            Data = mappedCandidate,
            Message = "Candidate found"
        };
    }
}
