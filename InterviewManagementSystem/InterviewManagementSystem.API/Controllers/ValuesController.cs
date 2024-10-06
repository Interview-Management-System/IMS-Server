using InterviewManagementSystem.Application.DTOs.JobDTOs;
using InterviewManagementSystem.Domain.Entities.AppUsers;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InterviewManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        UserManager<AppUser> a;

        public ValuesController(UserManager<AppUser> a)
        {
            this.a = a;
        }



        [HttpGet]
        //[Authorize(Policy = AuthorizationPolicy.RequiredAdmin)]
        public IActionResult Get()
        {
            TimeOnly a = new(10, 22, 43);
            return Ok(a.ToString("HH:mm"));
        }



        [HttpPost]
        public async Task<IActionResult> Post([FromBody] JobForRetrieveDTO jobForRetrieveDTO)
        {

            return Ok("asdf");
        }
    }





}


