﻿using Microsoft.AspNetCore.Mvc;

namespace InterviewManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public sealed class InterviewController : ControllerBase
    {

        /*
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
            var apiResponse = await _interviewManager.GetDetailByIdAsync<InterviewDetailRetrieveDTO>(id);
            return Ok(apiResponse);
        }



        [HttpPost("create")]
        public async Task<IActionResult> CreateInterviewAsync([FromBody] InterviewCreateDTO request)
        {
            var response = await _interviewManager.CreateInterview(request);
            return Ok(response);
        }


        [HttpPatch("submit-result")]
        public async Task<IActionResult> SubmitInterviewResultAsync([FromBody] InterviewSubmitResultDTO request)
        {
            var response = await _interviewManager.SubmitInterviewAsync(request);
            return Ok(response);
        }

        */
    }

}
