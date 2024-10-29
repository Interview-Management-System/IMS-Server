using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.Entities.Interviews;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

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



        [HttpGet]
        //[Authorize(Policy = AuthorizationPolicy.RequiredAdmin)]
        public async Task<IActionResult> Get()
        {

            var i = new InterviewSchedule();


            Stopwatch stopwatch = Stopwatch.StartNew();
            i.SetInterviewers(await _userManager.Users.ToListAsync());

            stopwatch.Stop();
            return Ok(stopwatch.ElapsedMilliseconds);
        }



        [HttpPost]
        public async Task<IActionResult> Post([FromBody] TimeOnly timeOnly)
        {

            return Ok("asdf");
        }


        private void Tessss(Test test)
        {
            test.MyProperty = 0;
            test.Candidate.UserName = "ohla";
        }
    }










    internal struct Test
    {
        public int MyProperty { get; set; }
        public Candidate Candidate { get; set; }
    }
}


