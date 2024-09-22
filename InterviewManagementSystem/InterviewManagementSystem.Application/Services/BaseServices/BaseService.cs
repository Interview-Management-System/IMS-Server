namespace InterviewManagementSystem.Application.Services.BaseServices
{
    public abstract class BaseService(IMapper mapper, IUnitOfWork unitOfWork)
    {
        protected readonly IMapper _mapper = mapper;
        protected readonly IUnitOfWork _unitOfWork = unitOfWork;
    }
}
