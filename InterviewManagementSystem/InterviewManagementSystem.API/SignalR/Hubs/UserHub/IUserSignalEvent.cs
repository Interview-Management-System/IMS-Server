namespace InterviewManagementSystem.API.SignalR.Hubs.UserHub
{
    public interface IUserSignalEvent
    {
        /// <summary>
        /// Notify to all clients if active, de-active or delete a user
        /// </summary>
        /// <returns></returns>
        Task UserChange();
    }
}
