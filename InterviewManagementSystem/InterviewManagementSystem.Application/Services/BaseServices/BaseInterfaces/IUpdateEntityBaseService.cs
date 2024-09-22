namespace InterviewManagementSystem.Application.Services.BaseServices.BaseInterfaces
{
    public interface IUpdateEntityBaseService<TEntity, TUpdateEntityDTO>
    {
        Task<ApiResponse<bool>> UpdateAsync(TUpdateEntityDTO entityUpdate);
    }
}
