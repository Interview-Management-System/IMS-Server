using Microsoft.AspNetCore.Mvc;

namespace InterviewManagementSystem.API.Controllers
{


    public class ValuesController(HttpClient httpClient) : ControllerBase
    {
        [HttpGet("test")]
        public async Task<IActionResult> Get()
        {
            return Ok(await GetPublicIpAddressAsync());
        }


        public async Task<string> GetPublicIpAddressAsync()
        {
            return await httpClient.GetStringAsync("https://api.ipify.org");
        }
    }



}


