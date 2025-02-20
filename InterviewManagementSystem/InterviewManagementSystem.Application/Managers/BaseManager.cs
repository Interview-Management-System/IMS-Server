using InterviewManagementSystem.Domain.Entities;

namespace InterviewManagementSystem.Application.Managers;


public abstract class BaseManager<T>(IMapper mapper, IUnitOfWork unitOfWork) where T : class
{
    protected readonly IMapper _mapper = mapper;
    protected readonly IUnitOfWork _unitOfWork = unitOfWork;
    protected readonly IBaseRepository<T> _repository = unitOfWork.GetBaseRepository<T>();



    protected async Task<ApiResponse<PageResult<TPaginationDTO>>> GetListPaginationAsync<TPaginationDTO>(PaginationParameter<T> paginationParameter)
    {

        // Create projection for mapping entity to DTO
        var projection = MapperHelper.CreateProjection<T, TPaginationDTO>(_mapper);
        var pageResult = await _repository.GetPaginationList(paginationParameter, projection: projection);

        return new ApiResponse<PageResult<TPaginationDTO>>
        {
            Data = _mapper.Map<PageResult<TPaginationDTO>>(pageResult)
        };
    }



    public async Task<ApiResponse<TDetailDTO>> GetDetailByIdAsync<TDetailDTO>(object id)
    {

        var projection = MapperHelper.CreateProjection<T, TDetailDTO>(_mapper);
        var detailObjectById = await _repository.GetByIdAsync(id, projection: projection);

        ArgumentNullException.ThrowIfNull(detailObjectById, "Data not found to view detail");

        return new ApiResponse<TDetailDTO>
        {
            Data = detailObjectById,
        };
    }



    public async Task<string> DeleteAsync(object id, bool isHardDelete = false)
    {
        var entityFoundById = await _repository.GetByIdAsync(id);

        ArgumentNullException.ThrowIfNull(entityFoundById, "Data not found to delete");
        var propertyInfo = entityFoundById.GetType().GetProperty(nameof(BaseEntity.IsDeleted));


        if (propertyInfo != null && propertyInfo.PropertyType == typeof(bool))
        {
            bool currentStatus = (bool)propertyInfo.GetValue(entityFoundById)!;
            ApplicationException.ThrowIfInvalidOperation(currentStatus != false, "Data is not deleted to restore");

            _repository.Delete(entityFoundById, isHardDelete);

            bool deleteSuccess = await _unitOfWork.SaveChangesAsync();
            ApplicationException.ThrowIfOperationFail(deleteSuccess, "Fail to delete");

            return "Delete successfully";
        }

        return "Delete failed";
    }



    public async Task<string> UndoDeleteAsync(object id)
    {
        var entity = await _repository.GetByIdAsync(id, isTracking: true);
        ArgumentNullException.ThrowIfNull(entity, $"Data not found to restore");


        var propertyInfo = entity.GetType().GetProperty(nameof(BaseEntity.IsDeleted));

        if (propertyInfo != null && propertyInfo.PropertyType == typeof(bool))
        {
            bool currentStatus = (bool)propertyInfo.GetValue(entity)!;
            ApplicationException.ThrowIfInvalidOperation(currentStatus == false, "Data is not deleted to restore");

            propertyInfo.SetValue(entity, false);

            bool success = await _unitOfWork.SaveChangesAsync();
            ApplicationException.ThrowIfOperationFail(success, $"Failed to restore");

            return "Restored successfully";
        }

        return "Restore failed";
    }
}
