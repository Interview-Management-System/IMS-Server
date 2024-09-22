namespace InterviewManagementSystem.Application.Services.BaseServices.BaseInterfaces
{
    public interface ICreateEntityBaseService<TEntity, TCreateEntityDTO>
    {
        Task<ApiResponse<bool>> CreateAsync(TCreateEntityDTO entityCreate);
    }
}
