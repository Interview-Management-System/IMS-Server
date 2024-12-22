using InterviewManagementSystem.Application.DTOs.InterviewScheduleDTOs;
using InterviewManagementSystem.Application.Features.InterviewScheduleFeature;
using InterviewManagementSystem.Domain.Enums;
using Microsoft.AspNetCore.Mvc;

namespace InterviewManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class InterviewScheduleController(InterviewScheduleFacade interviewScheduleFacade) : ControllerBase
{


    [HttpGet("list-paging")]
    public async Task<IActionResult> GetListInterviewPageResultAsync(InterviewSchedulePaginatedSearchRequest request)
    {
        var apiResponse = await interviewScheduleFacade.GetInterviewSchedulePagingAsync(request);
        return Ok(apiResponse);
    }



    [HttpPost("create")]
    public async Task<IActionResult> CreateInterviewAsync([FromBody] InterviewScheduleForCreateDTO interviewScheduleForCreateDTO)
    {
        var apiResponse = await interviewScheduleFacade.CreateInterviewScheduleAsync(interviewScheduleForCreateDTO);
        return Created("", apiResponse);
    }



    [HttpGet("detail")]
    public async Task<IActionResult> GetInterviewByIdAsync([FromQuery] Guid id)
    {
        var apiResponse = await interviewScheduleFacade.GetInterviewByIdAsync(id);
        return Ok(apiResponse);
    }


    [HttpPatch("set-result")]
    public async Task<IActionResult> SetInterviewResultAsync([FromQuery] Guid id, [FromQuery] InterviewResultEnum resultId)
    {
        var apiResponse = await interviewScheduleFacade.SetInterviewResultAsync(id, resultId);
        return Ok(apiResponse);
    }


    [HttpPatch("set-status")]
    public async Task<IActionResult> SetInterviewStatusAsync([FromQuery] Guid id, [FromQuery] InterviewStatusEnum statusId)
    {
        var apiResponse = await interviewScheduleFacade.SetInterviewStatusAsync(id, statusId);
        return Ok(apiResponse);
    }



    [HttpPut("update")]
    public async Task<IActionResult> UpdateInterviewAsync([FromBody] InterviewScheduleForUpdateDTO interviewScheduleForUpdateDTO)
    {
        var apiResponse = await interviewScheduleFacade.UpdateInterviewAsync(interviewScheduleForUpdateDTO);
        return Ok(apiResponse);
    }

}
