namespace InterviewManagementSystem.Application.Managers
{
    public abstract class BaseManager(IMapper mapper, IUnitOfWork unitOfWork)
    {
        protected readonly IMapper _mapper = mapper;
        protected readonly IUnitOfWork _unitOfWork = unitOfWork;
    }
}
