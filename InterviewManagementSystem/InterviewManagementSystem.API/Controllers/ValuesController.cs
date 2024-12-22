using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Infrastructure.Persistences;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InterviewManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        UserManager<AppUser> _userManager;

        public ValuesController(UserManager<AppUser> a)
        {
            this._userManager = a;
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

        [HttpGet("geteeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeeee")]
        public async Task<IActionResult> Get()
        {

            var a = new InterviewManagementSystemContext();
            return Ok(a.Positions.ToList());
        }
    }

}


