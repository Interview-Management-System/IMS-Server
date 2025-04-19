using InterviewManagementSystem.API.SignalR.Hubs.UserHub;

namespace InterviewManagementSystem.API.SignalR.Services
{
    public sealed class UserHubService
    {

        private readonly IHubContext<UserHub, IUserSignalEvent> _hubContext;
        private readonly IHubClients<IUserSignalEvent> _clients = default!;


        public UserHubService(IHubContext<UserHub, IUserSignalEvent> hubContext)
        {
            _hubContext = hubContext;
            _clients = hubContext.Clients;
        }

        public async Task NotifyUserDeletedAsync()
        {
            //await _clients.All.SendMessage("UserDelete");
        }


        public async Task NotifyUserChangeAsync()
        {
            await _clients.All.UserChange();
        }
    }
}
