using InterviewManagementSystem.API.SignalR.Hubs;
using Microsoft.AspNetCore.SignalR;

namespace InterviewManagementSystem.API.SignalR.Services
{
    public sealed class UserHubService
    {
        private readonly IHubContext<UserHub> _hubContext;

        public UserHubService(IHubContext<UserHub> hubContext)
        {
            _hubContext = hubContext;
        }

        public async Task NotifyUserDeletedAsync()
        {

            await _hubContext.Clients.All.SendAsync("UserDelete");
        }


        public async Task NotifyUserStatusChangeAsync()
        {
            await _hubContext.Clients.All.SendAsync("UserStatusChange");
        }
    }
}
