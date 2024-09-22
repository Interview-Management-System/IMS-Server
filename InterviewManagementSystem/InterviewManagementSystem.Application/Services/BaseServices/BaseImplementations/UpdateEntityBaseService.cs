
namespace InterviewManagementSystem.Application.Services.BaseServices.BaseImplementations;

public class UpdateEntityBaseService<TEntity, TUpdateEntityDTO> : BaseService, IUpdateEntityBaseService<TEntity, TUpdateEntityDTO> where TEntity : class
{


    public UpdateEntityBaseService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }



    public async Task<ApiResponse<bool>> UpdateAsync(TUpdateEntityDTO entityUpdate)
    {
        ApiResponse<bool> apiResponse = new();

        var entityToUpdate = _mapper.Map<TEntity>(entityUpdate);
        _unitOfWork.GetBaseRepository<TEntity>().Update(entityToUpdate);


        bool updateSuccessful = await _unitOfWork.SaveChangesAsync();

        if (updateSuccessful)
        {
            apiResponse.StatusCode = (int)HttpStatusCode.Created;
            apiResponse.Message = "Created successfully";
        }
        else
        {
            apiResponse.StatusCode = (int)HttpStatusCode.BadRequest;
            apiResponse.Message = "Create failed";
        }

        return apiResponse;
    }
}
