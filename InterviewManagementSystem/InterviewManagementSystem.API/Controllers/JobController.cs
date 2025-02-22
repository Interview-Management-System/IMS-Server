using InterviewManagementSystem.Application.DTOs.JobDTOs;
using InterviewManagementSystem.Application.Managers.JobManager;
using Microsoft.AspNetCore.Mvc;

namespace InterviewManagementSystem.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class JobController(JobManager manager) : ControllerBase
{


    private readonly JobManager _jobManager = manager;


    [HttpGet("open-jobs")]
    public async Task<IActionResult> GetListOpenJobAsync()
    {
        var apiResponse = await _jobManager.GetListOpenJobAsync();
        return Ok(apiResponse);
    }



    [HttpPost("pagination")]
    public async Task<IActionResult> GetListJobPageResultAsync(JobPaginatedSearchRequest? request)
    {
        var newRequest = request ?? new JobPaginatedSearchRequest();

        var apiResponse = await _jobManager.GetListJobPagingAsync(newRequest);
        return Ok(apiResponse);
    }


    [HttpGet("detail/{id}")]
    public async Task<IActionResult> GetJobDetailByIdAsync(Guid id)
    {
        var apiResponse = await _jobManager.GetDetailByIdAsync<JobForDetailRetrieveDTO>(id);
        return Ok(apiResponse);
    }



    [HttpPost("create")]
    public async Task<IActionResult> CreateJobAsync([FromBody] JobForCreateDTO jobForCreateDTO)
    {
        var response = await _jobManager.CreateNewJobAsync(jobForCreateDTO);
        return Created("", response);
    }



    [HttpPut("update")]
    public async Task<IActionResult> UpdateJobAsync([FromBody] JobForUpdateDTO jobForUpdateDTO)
    {
        var response = await _jobManager.UpdateJobAsync(jobForUpdateDTO);
        return Ok(response);
    }



    [HttpDelete("delete/{id}")]
    public async Task<IActionResult> DeleteJobAsync(Guid id, [FromQuery] bool? isHardDelete)
    {
        var responseMessage = await _jobManager.DeleteAsync(id, isHardDelete ?? false);
        return Ok(responseMessage);
    }



    [HttpPatch("undo-delete/{id}")]
    public async Task<IActionResult> UnDoDeleteJobAsync(Guid id)
    {
        var responseMessage = await _jobManager.UndoDeleteAsync(id);
        return Ok(responseMessage);
    }
}
