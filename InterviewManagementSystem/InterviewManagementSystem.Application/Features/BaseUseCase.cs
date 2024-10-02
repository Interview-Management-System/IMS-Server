namespace InterviewManagementSystem.Application.Features
{
    public abstract class BaseUseCase(IMapper mapper, IUnitOfWork unitOfWork)
    {
        protected readonly IMapper _mapper = mapper;
        protected readonly IUnitOfWork _unitOfWork = unitOfWork;


    }
}
