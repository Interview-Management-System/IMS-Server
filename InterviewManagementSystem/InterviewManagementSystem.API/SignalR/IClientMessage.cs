namespace InterviewManagementSystem.API.SignalR
{
    public interface IClientMessage
    {
        Task SendMessage<T>(T? message = default);
        Task ReceiveMessage(string message);
    }
}
