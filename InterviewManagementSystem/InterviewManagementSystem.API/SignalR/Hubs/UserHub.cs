using Microsoft.AspNetCore.SignalR;

namespace InterviewManagementSystem.API.SignalR.Hubs;

public sealed class UserHub : Hub<IClientMessage>
{
    public async override Task OnConnectedAsync()
    {

        var userId = Context.UserIdentifier;

        if (!string.IsNullOrEmpty(userId))
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }

        await base.OnConnectedAsync();
    }


    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }
}
