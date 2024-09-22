

namespace InterviewManagementSystem.Application.Services.BaseServices.BaseImplementations;

public class DeleteBaseService<TEntity> : IDeleteBaseService<TEntity> where TEntity : class
{

    private readonly IUnitOfWork _unitOfWork;



    public DeleteBaseService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }



    public async Task<ApiResponse<bool>> DeleteAsync(object id, bool isHardDelete = false)
    {
        var entityRepository = _unitOfWork.GetBaseRepository<TEntity>();
        var entity = await entityRepository.GetByIdAsync(id);

        ArgumentNullException.ThrowIfNull(entity, "Data not found");

        entityRepository.Delete(entity, isHardDelete);
        bool deleteSuccessful = await _unitOfWork.SaveChangesAsync();

        ApiResponse<bool> apiResponse = new()
        {
            StatusCode = deleteSuccessful ? (int)HttpStatusCode.BadRequest : (int)HttpStatusCode.OK,
            Message = deleteSuccessful ? "Delete successfully" : "Delete failed"
        };

        return apiResponse;
    }
}
