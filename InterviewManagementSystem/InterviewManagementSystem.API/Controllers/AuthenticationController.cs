using InterviewManagementSystem.Application.Managers.AuthenticationManager;
using Microsoft.AspNetCore.Mvc;

namespace InterviewManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class AuthenticationController(AuthenticationManager authenticationManagementFacade) : ControllerBase
{


    private readonly AuthenticationManager _authenticationManagementFacade = authenticationManagementFacade;


    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequest userLoginRequest)
    {
        var apiResponse = await _authenticationManagementFacade.BasicLoginAsync(userLoginRequest);
        return Ok(apiResponse);
    }



    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest resetPasswordRequest)
    {
        var apiResponse = await _authenticationManagementFacade.ResetPasswordAsync(resetPasswordRequest);
        return Ok(apiResponse);
    }
}
