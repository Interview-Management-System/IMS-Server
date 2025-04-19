using InterviewManagementSystem.Domain.Enums;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace InterviewManagementSystem.API.Controllers
{
    public class ValuesController(IHttpContextAccessor httpContextAccessor) : ControllerBase
    {
        [HttpGet("test")]
        [Authorize(Roles = nameof(RoleEnum.Recruiter))] // Fixed: Use nameof to ensure a constant expression
        public async Task<IActionResult> Get()
        {
            var aa = User.FindFirstValue(JwtRegisteredClaimNames.Name);
            return Ok(aa);
        }
    }



}


