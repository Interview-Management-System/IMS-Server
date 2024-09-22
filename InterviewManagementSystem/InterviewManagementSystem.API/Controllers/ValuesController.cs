using System.ComponentModel.DataAnnotations;
using InterviewManagementSystem.Application.CustomClasses;
using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.ValueObjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InterviewManagementSystem.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        IGetEntityBaseService<object, object> get;
        UserManager<AppUser> a;

        public ValuesController(UserManager<AppUser> a, IGetEntityBaseService<object, object> test)
        {
            //var ss = test.Mapper;
            get = test;



            this.a = a;
        }

        [HttpGet]
        [Authorize(Policy = AuthorizationPolicy.RequiredAdmin)]
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
        public async Task<IActionResult> Post()
        {


            ApiResponse<bool> apiResponse = new();
            apiResponse.Test();


            SalaryRange.CreateSalaryRange(0, 1);
            TimeOnly aa = new TimeOnly(18, 30);

            return Ok(aa.ToString("HH:mm tt"));
        }
    }




    public class A
    {
        [Required, Range(10, 100)]
        public int MyProperty { get; set; }

        //[JsonConverter(typeof(TimeOnlyJsonConverter))]
        public TimeOnly MyString { get; set; }
    }
}
