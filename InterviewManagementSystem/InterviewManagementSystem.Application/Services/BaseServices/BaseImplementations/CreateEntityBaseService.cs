
namespace InterviewManagementSystem.Application.Services.BaseServices.BaseImplementations;

public class CreateEntityBaseService<TEntity, TCreateEntityDTO> : BaseService, ICreateEntityBaseService<TEntity, TCreateEntityDTO> where TEntity : class
{

    public CreateEntityBaseService(IMapper mapper, IUnitOfWork unitOfWork) : base(mapper, unitOfWork)
    {
    }


    public async Task<ApiResponse<bool>> CreateAsync(TCreateEntityDTO entityCreate)
    {

        //ArgumentNullException.ThrowIfNull(entityCreate, "");
        ApiResponse<bool> apiResponse = new();

        TEntity entity = _mapper.Map<TEntity>(entityCreate);
        await _unitOfWork.GetBaseRepository<TEntity>().AddAsync(entity);


        bool createSuccessful = await _unitOfWork.SaveChangesAsync();



        if (createSuccessful)
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
