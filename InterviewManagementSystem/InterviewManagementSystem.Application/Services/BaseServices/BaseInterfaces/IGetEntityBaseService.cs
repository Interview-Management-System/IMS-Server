namespace InterviewManagementSystem.Application.Services.BaseServices.BaseInterfaces
{
    public interface IGetEntityBaseService<TEntity, TEntityDTO>
    {
        Task<ApiResponse<TEntityDTO>> GetByIdAsync(object id, bool isTracking = false);
        Task<ApiResponse<List<TEntityDTO>>> GetAllAsync(bool isGetDeleted = false);
    }
}
