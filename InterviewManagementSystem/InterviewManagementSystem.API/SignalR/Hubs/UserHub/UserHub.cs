using InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs;
using InterviewManagementSystem.Application.Managers.UserManagers;
using InterviewManagementSystem.Domain.Shared.Paginations;

namespace InterviewManagementSystem.API.SignalR.Hubs.UserHub;

public sealed class UserHub(UserManager userManager) : BaseHub<IUserSignalEvent>
{

    [HubMethodName("UserPagination")]
    public Task<PageResult<UserPaginationRetrieveDTO>> GetUserListPaging()
    {
        return ExecuteWithCancellationAsync(async () =>
        {
            var apiResponse = await userManager.GetListUserPagingAsync(new UserPaginatedSearchRequest());
            return apiResponse.Data!;
        });
    }
}