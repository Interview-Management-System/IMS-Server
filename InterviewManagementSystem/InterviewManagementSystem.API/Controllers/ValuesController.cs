using InterviewManagementSystem.Domain.Interfaces;
using InterviewManagementSystem.Infrastructure.Databases.MongoDB.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace InterviewManagementSystem.API.Controllers
{
    public class ValuesController(IMongoRepository<User> userRepo) : ControllerBase
    {
        [HttpGet("test")]
        public async Task<IActionResult> Get()
        {
            //UserRepo userRepo = new();

            //var sd = await userRepo.GetAllAsync();




            return Ok("");
        }



    }



}


