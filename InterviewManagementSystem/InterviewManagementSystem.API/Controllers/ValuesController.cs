using InterviewManagementSystem.API.SignalR.Hubs;
using InterviewManagementSystem.API.SignalR.Services;
using InterviewManagementSystem.Domain.Entities.AppUsers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;

namespace InterviewManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        UserManager<AppUser> _userManager;
        private readonly IHubContext<UserHub> _hubContext;

        private readonly UserHubService userHubService;

        public ValuesController(UserManager<AppUser> a, IHubContext<UserHub> hubContext, UserHubService userHubService)
        {
            this._userManager = a;
            _hubContext = hubContext;
            this.userHubService = userHubService;
        }



        [HttpPost("download-pdf")]
        public async Task<IActionResult> DownloadPdf(IFormFile file)
        {

            // Read the PDF file bytes
            byte[] fileBytes = [];


            using (var memoryStream = new MemoryStream())
            {
                await file.CopyToAsync(memoryStream);
                fileBytes = memoryStream.ToArray();
            }

            // Return the file as a response with the appropriate content type
            return Ok(fileBytes);
        }

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {
            //_hubContext.Clients.All.
            //await _hubContext.Clients.All.SendMessage(string.Empty);
            //await userHubService.NotifyUserDeletedAsync();



            return Ok("test message");
        }
    }

}


