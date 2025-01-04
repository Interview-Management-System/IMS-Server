using InterviewManagementSystem.Domain.Entities.AppUsers;
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

        [HttpGet("get")]
        public async Task<IActionResult> Get()
        {

            //var a = new InterviewManagementSystemContext();

            //ApplicationException.ThrowIfOperationFail()

            Application.CustomClasses.Exceptions.ApplicationException.ThrowIfOperationFail(false, "t546hyt");
            return Ok("test message");
        }
    }

}


