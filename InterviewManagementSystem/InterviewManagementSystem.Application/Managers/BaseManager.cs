using InterviewManagementSystem.Domain.Entities;
using InterviewManagementSystem.Domain.Shared.Exceptions;

namespace InterviewManagementSystem.Application.Managers;


public abstract class BaseManager<T>(IUnitOfWork unitOfWork) where T : class
{
    protected readonly IUnitOfWork _unitOfWork = unitOfWork;
    protected readonly IBaseRepository<T> _repository = unitOfWork.GetBaseRepository<T>();



    protected virtual async Task<ApiResponse<PageResult<TPaginationDTO>>> GetListPaginationAsync<TPaginationDTO>(PaginationParameter<T> paginationParameter)
    {

        var paginationQuery = PaginationHelper.CreatePaginationQuery<T, TPaginationDTO>(paginationParameter);
        var pageResult = await _repository.GetPaginationList(paginationQuery);

        return ApiResponse.Create(pageResult);
    }



    public virtual async Task<ApiResponse<TDetailDTO>> GetDetailByIdAsync<TDetailDTO>(object id)
    {

        var projection = MapperHelper.CreateProjection<T, TDetailDTO>();
        var detailObjectById = await _repository.GetByIdAsync(id, projection: projection);

        ImsError.ThrowIfNullOrEmpty(detailObjectById, "Data not found to view detail");
        return ApiResponse.Create(detailObjectById);
    }



    public virtual async Task<string> DeleteAsync(object id, bool isHardDelete = false)
    {

        var idProperty = typeof(T).GetProperty(nameof(BaseEntity.Id));
        ImsError.ThrowIfNullOrEmpty(idProperty, "Entity has no Id to delete");

        /*
        bool deleteSuccess = false;

        if (isHardDelete)
        {
            var (deleteSuccess, a) = await _repository.InstantDeleteAsync(e => EF.Property<object>(e, idProperty.Name).Equals(id));
        }
        else
        {
            deleteSuccess = await _repository
                .BulkUpdateAsync(e => EF.Property<object>(e, idProperty.Name).Equals(id),
                                 e => e.SetProperty(e => EF.Property<bool>(e, nameof(BaseEntity.IsDeleted)), true));
        }

        ImsError.ThrowIfInvalidOperation(deleteSuccess, "Fail to delete");
        */
        return "Delete successfully";
    }




    public virtual async Task<string> UndoDeleteAsync(List<object> idList)
    {

        var idProperty = typeof(T).GetProperty(nameof(BaseEntity.Id));
        ImsError.ThrowIfNullOrEmpty(idProperty, "Entity has no Id to delete");


        var (undoDeleteSuccess, deletedCount) = await _repository
            .BulkUpdateAsync(e => idList.Contains(EF.Property<object>(e, idProperty.Name)),
                             e => e.SetProperty(e => EF.Property<bool>(e, nameof(BaseEntity.IsDeleted)), false));

        ImsError.ThrowIfInvalidOperation(undoDeleteSuccess, "Fail to undo delete");
        return $"Restore ({deletedCount}) deleted and fail ({idList.Count - deletedCount})";
    }
}
