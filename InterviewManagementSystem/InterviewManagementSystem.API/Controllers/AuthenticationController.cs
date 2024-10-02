using InterviewManagementSystem.Application.Features.AuthenticationFeatures;
using Microsoft.AspNetCore.Mvc;

namespace InterviewManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class AuthenticationController : ControllerBase
{


    private readonly AuthenticationFacade _authenticationManagementFacade;


    public AuthenticationController(AuthenticationFacade authenticationManagementFacade)
    {
        _authenticationManagementFacade = authenticationManagementFacade;
    }



    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync([FromBody] UserLoginRequest userLoginRequest)
    {
        var apiResponse = await _authenticationManagementFacade.LoginUseCase.LoginAsync(userLoginRequest);
        return Ok(apiResponse);
    }



    [HttpPost("reset-password")]
    public async Task<IActionResult> ResetPasswordAsync([FromBody] ResetPasswordRequest resetPasswordRequest)
    {
        var apiResponse = await _authenticationManagementFacade.PasswordUseCase.ResetPasswordAsync(resetPasswordRequest);
        return Ok(apiResponse);
    }
}
