using System.ComponentModel.DataAnnotations;
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
        public IActionResult Get()
        {

            //var aa = a.Users.ToList()[0];


            //var aaaaa = a.CreateAsync(new Candidate() { Id = Guid.NewGuid(), UserName = "can", Email = "candidate@gmail.com", PhoneNumber = "1", YearsOfExperience = 5 }, "T@n75541972").Result;


            //var q = new InterviewManagementSystemContext().InterviewSchedules.Include(a => a.Candidate).ToList();


            var s = a.FindByIdAsync("b363c4ab-b6ff-40cd-a342-bde83ad83cb9").Result;


            var ss = a.GetRolesAsync(s).Result;
            return Ok("asaas");
        }


        [HttpPost]
        public async Task<IActionResult> Post([FromBody] A a)
        {
            return Ok(a);
        }
    }




    public class A
    {
        [Required, Range(10, 100)]
        public int MyProperty { get; set; }

        [Required]
        public string MyString { get; set; }
    }
}
