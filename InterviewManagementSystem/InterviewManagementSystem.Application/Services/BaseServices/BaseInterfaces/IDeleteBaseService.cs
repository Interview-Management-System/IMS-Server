namespace InterviewManagementSystem.Application.Services.BaseServices.BaseInterfaces
{
    public interface IDeleteBaseService<TEntity> where TEntity : class
    {
        Task<ApiResponse<bool>> DeleteAsync(object id, bool isHardDelete = false);
    }
}
