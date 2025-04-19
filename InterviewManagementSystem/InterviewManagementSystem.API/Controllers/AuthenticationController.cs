using InterviewManagementSystem.Application.DTOs;
using InterviewManagementSystem.Application.Managers.AuthenticationManager;
using Microsoft.AspNetCore.Mvc;

namespace InterviewManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class AuthenticationController(AuthenticationManager authenticationManager) : ControllerBase
{


    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequest userLoginRequest)
    {
        var apiResponse = await authenticationManager.BasicLoginAsync(userLoginRequest);
        return Ok(apiResponse);
    }



    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest resetPasswordRequest)
    {
        var apiResponse = await authenticationManager.ResetPasswordAsync(resetPasswordRequest);
        return Ok(apiResponse);
    }


}

