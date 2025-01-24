using InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs;
using InterviewManagementSystem.Application.Managers.UserManagers;
using InterviewManagementSystem.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace InterviewManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class CandidateController(CandidateManager candidateManager) : ControllerBase
{

    [HttpGet("candidate-list")]
    public async Task<IActionResult> GetListCandidateAsync()
    {
        var apiResponse = await candidateManager.GetCandidateListAsync();
        return Ok(apiResponse);
    }


    [HttpGet("candidate-detail")]
    public async Task<IActionResult> GetCandidateDetailAsync([FromQuery] Guid id)
    {
        var apiResponse = await candidateManager.GetCandidateByIdAsync(id);
        return Ok(apiResponse);
    }



    [HttpPost("candidate-create")]
    public async Task<IActionResult> CreateCandidateAsync([FromForm] CandidateForCreateDTO candidateForCreateDTO)
    {
        var apiResponse = await candidateManager.CreateCandidateAsync(candidateForCreateDTO);
        return Ok(apiResponse);
    }



    [HttpPut("candidate-update")]
    public async Task<IActionResult> UpdateCandidateAsync([FromForm] CandidateForUpdateDTO candidateForUpdateDTO)
    {
        var apiResponse = await candidateManager.UpdateCandidateAsync(candidateForUpdateDTO);
        return Ok(apiResponse);
    }




    [HttpPatch("candidate-set-status")]
    public async Task<IActionResult> SetCandidateStatusAsync([FromQuery] Guid id, [FromQuery] CandidateStatusEnum candidateStatusEnum)
    {
        var apiResponse = await candidateManager.SetCandidateStatus(id, candidateStatusEnum);
        return Ok(apiResponse);
    }
}
