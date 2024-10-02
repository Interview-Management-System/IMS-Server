using InterviewManagementSystem.Application.CustomClasses.Extensions;
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



            var s = offerStatusEnum.GetStatusName();

            //var aa = a.Users.ToList()[0];


            //var aaaaa = a.CreateAsync(new Candidate() { Id = Guid.NewGuid(), UserName = "can", Email = "candidate@gmail.com", PhoneNumber = "1", YearsOfExperience = 5 }, "T@n75541972").Result;



            /*
            var s = a.FindByIdAsync("b363c4ab-b6ff-40cd-a342-bde83ad83cb9").Result;


            var ss = a.GetRolesAsync(s).Result;
            */

            //var q = new InterviewManagementSystemContext().Candidates.ToList();

            return Ok(s);
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


