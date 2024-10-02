using InterviewManagementSystem.Domain.Entities.AppUsers;

namespace InterviewManagementSystem.Application.Features.UserFeature
{
    public abstract class BaseUserUseCase : BaseUseCase
    {

        protected readonly UserManager<AppUser> _userManager;
        protected readonly RoleManager<AppRole> _roleManager;
        protected static readonly string DEFAULT_PASSWORD = "T@n75541972";



        protected BaseUserUseCase(IMapper mapper, IUnitOfWork unitOfWork, UserManager<AppUser> userManager, RoleManager<AppRole> roleManager)
            : base(mapper, unitOfWork)
        {
            _userManager = userManager;
            _roleManager = roleManager;
        }
    }
}
