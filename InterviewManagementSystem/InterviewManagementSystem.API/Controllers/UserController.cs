﻿using InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs;
using InterviewManagementSystem.Application.Managers.UserManagers;
using Microsoft.AspNetCore.Mvc;

namespace InterviewManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class UserController(UserManager userManager) : ControllerBase
{


    [HttpGet("list")]
    public async Task<IActionResult> GetListUserAsync()
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
        var apiResponse = await userManager.GetUserByIdAsync(id);
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
    public async Task<IActionResult> DeleteUserAsync(Guid id)
    {
        var apiResponse = await userManager.DeleteUserAsync(id);
        return Ok(apiResponse);
    }




    [HttpPatch("undo-delete/{id}")]
    public async Task<IActionResult> UndoDeleteUserAsync(Guid id)
    {
        var apiResponse = await userManager.UnDoDeleteUserAsync(id);
        return Ok(apiResponse);
    }




    [HttpPatch("activate/{id}")]
    public async Task<IActionResult> ActivateUserAsync(Guid id)
    {
        var apiResponse = await userManager.ActivateUser(id);
        return Ok(apiResponse);
    }





    [HttpPatch("de-activate/{id}")]
    public async Task<IActionResult> DeActivateUserAsync(Guid id)
    {
        var apiResponse = await userManager.DeActivateUser(id);
        return Ok(apiResponse);
    }



    [HttpPatch("change-role/{id}/role/{roleId}")]
    public async Task<IActionResult> ChangeUserRoleAsync(Guid id, Guid roleId)
    {
        var apiResponse = await userManager.ChangeUserRole(id, roleId);
        return Ok(apiResponse);
    }

}
