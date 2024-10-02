using InterviewManagementSystem.Domain.Entities.AppUsers;
using Microsoft.AspNetCore.Identity;

namespace InterviewManagementSystem.Application.Features.AuthenticationFeatures
{
    public abstract class BaseAuthenticationUseCase
    {

        protected readonly UserManager<AppUser> _userManager;

        protected BaseAuthenticationUseCase(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
    }
}
