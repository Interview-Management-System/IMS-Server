using InterviewManagementSystem.API.SignalR.Hubs;

namespace InterviewManagementSystem.API.Configurations;

internal static class HubConfiguration
{

    internal static void UseHubs(this WebApplication webApplication)
    {
        webApplication.MapHub<UserHub>("/user-hub");
    }
}
