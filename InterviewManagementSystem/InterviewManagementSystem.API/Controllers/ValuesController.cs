using InterviewManagementSystem.Application.Shared.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace InterviewManagementSystem.API.Controllers
{
    public class ValuesController(IHttpContextAccessor httpContextAccessor) : ControllerBase
    {
        [HttpGet("test")]
        //[Authorize(Roles = nameof(RoleEnum.Recruiter))] // Fixed: Use nameof to ensure a constant expression
        public async Task<IActionResult> Get()
        {
            //var aa = User.FindFirstValue(JwtRegisteredClaimNames.Name);

            throw new Exception("Test exception");
            IMSLogger.Success("safdf");
            return Ok();
        }
    }



}


