using InterviewManagementSystem.Application.CustomClasses;
using InterviewManagementSystem.Application.DTOs.InterviewScheduleDTOs;
using InterviewManagementSystem.Application.Features.InterviewScheduleFeature;
using InterviewManagementSystem.Domain.Enums;
using Microsoft.AspNetCore.Mvc;
using static InterviewManagementSystem.Application.CustomClasses.Helpers.EntityHelper;

namespace InterviewManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public sealed class InterviewScheduleController : ControllerBase
{


    private readonly InterviewScheduleFacade _interviewScheduleFacade;


    public InterviewScheduleController(InterviewScheduleFacade interviewScheduleFacade)
    {
        _interviewScheduleFacade = interviewScheduleFacade;
    }





    [HttpGet("list-paging")]
    public async Task<IActionResult> GetListInterviewPageResultAsync(string? interviewTitle, Guid? interviewerId, InterviewStatusEnum? interviewStatusId, int pageSize = 5, int pageIndex = 1)
    {

        var paginationRequest = new PaginationRequest()
        {
            PageSize = pageSize,
            PageIndex = pageIndex,
            EnumsToFilter = EntityEnumMapping.BuildInterviewEnumFilter(interviewStatusId),
            FieldNamesToSearch = EntityPropertyMapping.BuildInterviewSearchFieldMapping(interviewTitle)
        };


        var apiResponse = await _interviewScheduleFacade.GetInterviewSchedulePagingAsync(paginationRequest, interviewerId);
        return Ok(apiResponse);
    }



    [HttpPost("create")]
    public async Task<IActionResult> CreateInterviewAsync([FromBody] InterviewScheduleForCreateDTO interviewScheduleForCreateDTO)
    {
        var apiResponse = await _interviewScheduleFacade.CreateInterviewScheduleAsync(interviewScheduleForCreateDTO);
        return Created("", apiResponse);
    }



    [HttpGet("detail")]
    public async Task<IActionResult> GetInterviewByIdAsync([FromQuery] Guid id)
    {
        //var apiResponse = await _interviewScheduleFacade.CreateInterviewScheduleAsync(interviewScheduleForCreateDTO);
        return Created("", "");
    }
}
