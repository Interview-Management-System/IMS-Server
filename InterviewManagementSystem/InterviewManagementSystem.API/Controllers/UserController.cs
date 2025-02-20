using InterviewManagementSystem.API.SignalR.Services;
using InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs;
using InterviewManagementSystem.Application.Managers.UserManagers;
using Microsoft.AspNetCore.Mvc;

namespace InterviewManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class UserController(UserManager userManager, UserHubService userHubService) : ControllerBase
{


    [HttpGet("recruiters")]
    public async Task<IActionResult> GetListRecruiterAsync()
    {
        var apiResponse = await userManager.GetListAsync();
        return Ok(apiResponse);
    }


    [HttpPost("pagination")]
    public async Task<IActionResult> GetListUserPagingAsync(UserPaginatedSearchRequest? request)
    {
        var newRequest = request ?? new UserPaginatedSearchRequest();

        var apiResponse = await userManager.GetListUserPagingAsync(newRequest);
        return Ok(apiResponse);
    }



    [HttpGet("detail/{id}")]
    public async Task<IActionResult> GetUserDetailAsync(Guid id)
    {
        var apiResponse = await userManager.GetDetailByIdAsync<UserForDetailRetrieveDTO>(id);
        return Ok(apiResponse);
    }



    [HttpPost("create")]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserForCreateDTO userForCreateDTO)
    {
        string responseMessage = await userManager.CreateUserAsync(userForCreateDTO);
        return Ok(responseMessage);
    }



    [HttpPut("update")]
    public async Task<IActionResult> UpdateUserAsync([FromBody] UserForUpdateDTO userForUpdateDTO)
    {
        var apiResponse = await userManager.UpdateUserAsync(userForUpdateDTO);
        return Ok(apiResponse);
    }




    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteUserAsync(Guid id, [FromQuery] bool? isHardDelete)
    {
        var apiResponse = await userManager.DeleteAsync(id, isHardDelete ?? false);
        return Ok(apiResponse);
    }




    [HttpPatch("undo-delete/{id}")]
    public async Task<IActionResult> UndoDeleteUserAsync(Guid id)
    {
        var apiResponse = await userManager.UndoDeleteAsync(id);
        return Ok(apiResponse);
    }




    [HttpPatch("activate/{id}")]
    public async Task<IActionResult> ActivateUserAsync(Guid id)
    {
        var apiResponse = await userManager.ActivateUser(id);
        await userHubService.NotifyUserStatusChangeAsync();

        return Ok(apiResponse);
    }





    [HttpPatch("de-activate/{id}")]
    public async Task<IActionResult> DeActivateUserAsync(Guid id)
    {
        var apiResponse = await userManager.DeActivateUser(id);
        await userHubService.NotifyUserStatusChangeAsync();

        return Ok(apiResponse);
    }



    [HttpPatch("change-role/{id}/role/{roleId}")]
    public async Task<IActionResult> ChangeUserRoleAsync(Guid id, Guid roleId)
    {
        var apiResponse = await userManager.ChangeUserRole(id, roleId);
        return Ok(apiResponse);
    }

}
