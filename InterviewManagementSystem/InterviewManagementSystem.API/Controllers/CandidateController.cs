using InterviewManagementSystem.Application.DTOs.UserDTOs.CandidateDTOs;
using InterviewManagementSystem.Application.Managers.UserManagers;
using InterviewManagementSystem.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace InterviewManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class CandidateController(CandidateManager candidateManager) : ControllerBase
{

    [HttpGet("list")]
    public async Task<IActionResult> GetListCandidateAsync()
    {
        //var apiResponse = await candidateManager.GetCandidateListAsync();
        return Ok("");
    }


    [HttpPost("pagination")]
    public async Task<IActionResult> GetListUserPagingAsync(CandidatePaginatedSearchRequest? request)
    {
        var newRequest = request ?? new CandidatePaginatedSearchRequest();

        var apiResponse = await candidateManager.GetListCandidatePagingAsync(newRequest);
        return Ok(apiResponse);
    }


    [HttpGet("detail/{id}")]
    public async Task<IActionResult> GetCandidateDetailAsync(Guid id)
    {
        var apiResponse = await candidateManager.GetCandidateByIdAsync(id);
        return Ok(apiResponse);
    }



    [HttpPost("create")]
    public async Task<IActionResult> CreateCandidateAsync([FromForm] CandidateForCreateDTO candidateForCreateDTO)
    {
        var apiResponse = await candidateManager.CreateCandidateAsync(candidateForCreateDTO);
        return Ok(apiResponse);
    }



    [HttpPut("update")]
    public async Task<IActionResult> UpdateCandidateAsync([FromForm] CandidateForUpdateDTO candidateForUpdateDTO)
    {
        var apiResponse = await candidateManager.UpdateCandidateAsync(candidateForUpdateDTO);
        return Ok(apiResponse);
    }




    [HttpPatch("change-status")]
    public async Task<IActionResult> SetCandidateStatusAsync([FromQuery] Guid id, [FromQuery] CandidateStatusEnum candidateStatusEnum)
    {
        var apiResponse = await candidateManager.SetCandidateStatus(id, candidateStatusEnum);
        return Ok(apiResponse);
    }
}
