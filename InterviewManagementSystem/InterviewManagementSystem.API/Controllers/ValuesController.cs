using InterviewManagementSystem.API.SignalR.Hubs;
using InterviewManagementSystem.API.SignalR.Services;
using InterviewManagementSystem.Application.Services;
using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace InterviewManagementSystem.API.Controllers
{
    public class ValuesController : ControllerBase
    {
        ICloudinaryService _cloudinaryService;
        UserManager<AppUser> _userManager;
        private readonly IHubContext<UserHub> _hubContext;

        private readonly UserHubService userHubService;
        //private readonly CloudinaryService cloudinaryService;
        private readonly IUnitOfWork unitOfWork;

        public ValuesController(ICloudinaryService _cloudinaryService, UserManager<AppUser> a, IHubContext<UserHub> hubContext, UserHubService userHubService, IUnitOfWork unitOfWork)
        {
            this._cloudinaryService = _cloudinaryService;
            this.unitOfWork = unitOfWork;
            this._userManager = a;
            _hubContext = hubContext;
            this.userHubService = userHubService;
        }



        [HttpPost("download-pdf")]
        public async Task<IActionResult> DownloadPdf(IFormFile file)
        {

            var rs = await _cloudinaryService.UploadFileAsync(file);

            //var ss = await MasterDataUtility.GetListSkillByIdListAsync([SkillsEnum.CPlus]);

            string sss = rs.SecureUrl.ToString();
            return Ok(sss);
        }


    }



}


