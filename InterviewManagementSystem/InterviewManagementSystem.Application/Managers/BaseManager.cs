using InterviewManagementSystem.Domain.Entities;

namespace InterviewManagementSystem.Application.Managers;


public abstract class BaseManager<T>(IUnitOfWork unitOfWork) where T : class
{
    protected readonly IUnitOfWork _unitOfWork = unitOfWork;
    protected readonly IBaseRepository<T> _repository = unitOfWork.GetBaseRepository<T>();



    protected virtual async Task<ApiResponse<PageResult<TPaginationDTO>>> GetListPaginationAsync<TPaginationDTO>(PaginationParameter<T> paginationParameter)
    {

        var paginationQuery = PaginationHelper.CreatePaginationQuery<T, TPaginationDTO>(paginationParameter);
        var pageResult = await _repository.GetPaginationList(paginationQuery);


        return new ApiResponse<PageResult<TPaginationDTO>>
        {
            Data = pageResult
        };
    }



    public virtual async Task<ApiResponse<TDetailDTO>> GetDetailByIdAsync<TDetailDTO>(object id)
    {

        var projection = MapperHelper.CreateProjection<T, TDetailDTO>();
        var detailObjectById = await _repository.GetByIdAsync(id, projection: projection);

        ArgumentNullException.ThrowIfNull(detailObjectById, "Data not found to view detail");

        return new ApiResponse<TDetailDTO>
        {
            Data = detailObjectById,
        };
    }



    public virtual async Task<string> DeleteAsync(object id, bool isHardDelete = false)
    {

        var idProperty = typeof(T).GetProperty(nameof(BaseEntity.Id));
        ArgumentNullException.ThrowIfNull(idProperty, "Entity has no Id to delete");


        bool deleteSuccess = false;

        if (isHardDelete)
        {
            deleteSuccess = await _repository.InstantDeleteAsync(e => EF.Property<object>(e, idProperty.Name).Equals(id));
        }
        else
        {
            deleteSuccess = await _repository
                .InstantUpdateAsync(e => EF.Property<object>(e, idProperty.Name).Equals(id),
                                    e => e.SetProperty(e => EF.Property<bool>(e, nameof(BaseEntity.IsDeleted)), true));
        }

        ApplicationException.ThrowIfOperationFail(deleteSuccess, "Fail to delete");
        return "Delete successfully";
    }




    public virtual async Task<string> UndoDeleteAsync(object id)
    {

        var idProperty = typeof(T).GetProperty(nameof(BaseEntity.Id));
        ArgumentNullException.ThrowIfNull(idProperty, "Entity has no Id to delete");

        bool undoDeleteSuccess = await _repository
            .InstantUpdateAsync(e => EF.Property<object>(e, idProperty.Name).Equals(id),
                                u => u.SetProperty(e => EF.Property<bool>(e, nameof(BaseEntity.IsDeleted)), false));

        ApplicationException.ThrowIfOperationFail(undoDeleteSuccess, "Fail to undo delete");
        return "Restore failed";
    }
}
