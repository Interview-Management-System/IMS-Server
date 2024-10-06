using InterviewManagementSystem.Application.DTOs.JobDTOs;
using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.Enums;
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
        public IActionResult Get(BenefitEnum offerStatusEnum)
        {

            return Ok();
        }


        void SS<T>() where T : class
        {

        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] JobForRetrieveDTO jobForRetrieveDTO)
        {
            /*
            var aaaa = await a.FindByIdAsync(id.ToString());

            if (aaaa is Candidate can)
            {
                can.YearsOfExperience = 100;
            }
*/
            return Ok("asdf");
        }
    }




    public class Test
    {
        public int MyProperty { get; set; }
        public string Name { get; set; }
    }
}


