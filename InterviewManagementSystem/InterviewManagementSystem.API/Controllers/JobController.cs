using InterviewManagementSystem.Application.DTOs.JobDTOs;
using InterviewManagementSystem.Application.Features.JobFeature;
using Microsoft.AspNetCore.Mvc;

namespace InterviewManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobController : ControllerBase
{

    private readonly JobFacade _jobFacade;


    public JobController(JobFacade jobFacade)
    {
        _jobFacade = jobFacade;
    }



    [HttpGet("list-open-job")]
    public async Task<IActionResult> GetListOpenJobAsync()
    {
        var apiResponse = await _jobFacade.GetListOpenJobAsync();
        return Ok(apiResponse);
    }



    [HttpGet("list-paging")]
    public async Task<IActionResult> GetListJobPageResultAsync(JobPaginatedSearchRequest request)
    {
        var apiResponse = await _jobFacade.GetListJobPagingAsync(request);
        return Ok(apiResponse);
    }




    [HttpGet("detail")]
    public async Task<IActionResult> GetJobDetailByIdAsync(Guid id)
    {
        var apiResponse = await _jobFacade.GetJobDetailByIdAsync(id);
        return Ok(apiResponse);
    }




    [HttpPost("create")]
    public async Task<IActionResult> CreateJobAsync([FromBody] JobForCreateDTO jobForCreateDTO)
    {
        var response = await _jobFacade.CreateJobAsync(jobForCreateDTO);
        return Created("", response);
    }



    [HttpPut("update")]
    public async Task<IActionResult> UpdateJobAsync([FromBody] JobForUpdateDTO jobForUpdateDTO)
    {
        var response = await _jobFacade.UpdateJobAsync(jobForUpdateDTO);
        return Ok(response);
    }



    [HttpPatch("delete")]
    public async Task<IActionResult> DeleteJobAsync(Guid id)
    {
        var responseMessage = await _jobFacade.DeleteJobAsync(id);
        return Accepted("", responseMessage);
    }

}
