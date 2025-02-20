using InterviewManagementSystem.Application.DTOs.InterviewDTOs;
using InterviewManagementSystem.Application.Managers.InterviewManager;
using Microsoft.AspNetCore.Mvc;

namespace InterviewManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class InterviewController : ControllerBase
    {

        private readonly InterviewManager _interviewManager;

        public InterviewController(InterviewManager interviewManager)
        {
            _interviewManager = interviewManager;
        }


        [HttpPost("pagination")]
        public async Task<IActionResult> GetListInterviewPageResultAsync(InterviewPaginatedSearchRequest? request)
        {
            var newRequest = request ?? new InterviewPaginatedSearchRequest();

            var apiResponse = await _interviewManager.GetListInterviewPagingAsync(newRequest);
            return Ok(apiResponse);
        }



        [HttpGet("detail/{id}")]
        public async Task<IActionResult> GetInterviewDetailByIdAsync(Guid id)
        {
            var apiResponse = await _interviewManager.GetDetailByIdAsync<InterviewForDetailRetrieveDTO>(id);
            return Ok(apiResponse);
        }
    }
}
