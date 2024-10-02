using InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs;
using InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs;
using InterviewManagementSystem.Application.Features.UserFeature;
using InterviewManagementSystem.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace InterviewManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class UserController : ControllerBase
{

    private readonly UserFacade _userFacade;



    public UserController(UserFacade userFacade)
    {
        _userFacade = userFacade;
    }



    [HttpGet("list")]
    public async Task<IActionResult> GetListUserAsync()
    {
        var apiResponse = await _userFacade.GetListUserAsync();
        return Ok(apiResponse);
    }



    [HttpGet("detail")]
    public async Task<IActionResult> GetUserDetailAsync([FromQuery] Guid id)
    {
        var apiResponse = await _userFacade.GetUserByIdAsync(id);
        return Ok(apiResponse);
    }



    [HttpGet("candidate-detail")]
    public async Task<IActionResult> GetCandidateDetailAsync([FromQuery] Guid id)
    {
        var apiResponse = await _userFacade.GetCandidateByIdAsync(id);
        return Ok(apiResponse);
    }



    [HttpPost("candidate-create")]
    public async Task<IActionResult> CreateCandidateAsync([FromForm] CandidateForCreateDTO candidateForCreateDTO)
    {
        var apiResponse = await _userFacade.CreateCandidateAsync(candidateForCreateDTO);
        return Ok(apiResponse);
    }



    [HttpPut("candidate-update")]
    public async Task<IActionResult> UpdateCandidateAsync([FromForm] CandidateForUpdateDTO candidateForUpdateDTO)
    {
        var apiResponse = await _userFacade.UpdateCandidateAsync(candidateForUpdateDTO);
        return Ok(apiResponse);
    }




    [HttpPatch("candidate-set-status")]
    public async Task<IActionResult> BanCandidateAsync([FromQuery] Guid id, [FromQuery] CandidateStatusEnum candidateStatusEnum)
    {
        var apiResponse = await _userFacade.SetCandidateStatus(id, candidateStatusEnum);
        return Ok(apiResponse);
    }




    [HttpPost("create")]
    public async Task<IActionResult> CreateUserAsync([FromBody] UserForCreateDTO userForCreateDTO)
    {
        string responseMessage = await _userFacade.CreateUserAsync(userForCreateDTO);
        return Ok(responseMessage);
    }



    [HttpPut("update")]
    public async Task<IActionResult> UpdateUserAsync([FromBody] UserForUpdateDTO userForUpdateDTO)
    {
        var apiResponse = await _userFacade.UpdateUserAsync(userForUpdateDTO);
        return Ok(apiResponse);
    }




    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteUserAsync([FromQuery] Guid id)
    {
        var apiResponse = await _userFacade.DeleteUserAsync(id);
        return Ok(apiResponse);
    }




    [HttpPatch("undo-delete")]
    public async Task<IActionResult> UndoDeleteUserAsync([FromQuery] Guid id)
    {
        var apiResponse = await _userFacade.UndoDeleteUserAsync(id);
        return Ok(apiResponse);
    }




    [HttpPatch("activate-user")]
    public async Task<IActionResult> ActivateUserAsync([FromQuery] Guid id)
    {
        var apiResponse = await _userFacade.ActivateUserAsync(id);
        return Ok(apiResponse);
    }





    [HttpPatch("deActivate-user")]
    public async Task<IActionResult> DeActivateUserAsync([FromQuery] Guid id)
    {
        var apiResponse = await _userFacade.DeActivateUserAsync(id);
        return Ok(apiResponse);
    }
}
