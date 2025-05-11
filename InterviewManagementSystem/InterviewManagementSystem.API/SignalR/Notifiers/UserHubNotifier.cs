using InterviewManagementSystem.API.SignalR.Hubs.UserHub;

namespace InterviewManagementSystem.API.SignalR.Notifiers
{
    public sealed class UserHubNotifier(IHubContext<UserHub, IUserSignalEvent> hubContext)
    {
        private readonly IHubClients<IUserSignalEvent> _clients = hubContext.Clients;

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
