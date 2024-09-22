namespace InterviewManagementSystem.Application.Services.BaseServices.BaseImplementations;



public class GetEntityBaseService<TEntity, TEntityDTO> : BaseService, IGetEntityBaseService<TEntity, TEntityDTO> where TEntity : class
{


    public GetEntityBaseService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }



    public async Task<ApiResponse<List<TEntityDTO>>> GetAllAsync(bool isGetDeleted = false)
    {
        ApiResponse<List<TEntityDTO>> apiResponse = new();
        var listEntity = await _unitOfWork.GetBaseRepository<TEntity>().GetAllAsync();


        ArgumentNullException.ThrowIfNull(listEntity, "Unable to access list");


        if (listEntity != null || listEntity?.Count > 0)
        {
            apiResponse.StatusCode = (int)HttpStatusCode.Found;
            apiResponse.Message = "Data found";
            apiResponse.Data = _mapper.Map<List<TEntityDTO>>(listEntity);
        }
        else
        {
            apiResponse.StatusCode = (int)HttpStatusCode.BadRequest;
            apiResponse.Message = "List is empty";
        }
        return apiResponse;

    }




    public async Task<ApiResponse<TEntityDTO>> GetByIdAsync(object id, bool isTracking = false)
    {
        ApiResponse<TEntityDTO> apiResponse = new();
        TEntity? entity = await _unitOfWork.GetBaseRepository<TEntity>().GetByIdAsync(id, isTracking);


        ArgumentNullException.ThrowIfNull(entity, "Data not found");


        apiResponse.Data = _mapper.Map<TEntityDTO>(entity);
        apiResponse.StatusCode = (int)HttpStatusCode.Found;
        apiResponse.Message = "Data found";

        return apiResponse;
    }


}
