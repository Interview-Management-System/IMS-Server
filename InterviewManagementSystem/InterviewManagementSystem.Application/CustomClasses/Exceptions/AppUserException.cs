using InterviewManagementSystem.Domain.Entities.AppUsers;

namespace InterviewManagementSystem.Application.CustomClasses.Exceptions;

internal sealed class AppUserException(string errorMessage) : ApplicationException(errorMessage)
{


    internal static void ThrowIfUserNotActive(AppUser appUser)
    {
        if (appUser.IsActive == false)
            throw new AppUserException("Account is de-activated");
    }


    internal static void ThrowIfEmailNotConfirmed(AppUser appUser)
    {
        if (appUser.EmailConfirmed == false)
            throw new AppUserException("Email is not confirmed");
    }


    internal static void ThrowIfWrongPassword(bool isValidPassword)
    {
        if (isValidPassword == false)
            throw new AppUserException("Wrong password");
    }


    internal static void ThrowIfResetPasswordsNotEqual(string newPassword, string confirmPassword)
    {
        if (newPassword != confirmPassword)
            throw new AppUserException("New password and confirm password are not equal");
    }



    internal static void ThrowIfResetPasswordFail(bool isResetSuccess)
    {
        if (isResetSuccess == false)
            throw new AppUserException("Reset password fail");
    }


    internal static void ThrowIfUserExist(AppUser? appUser, string? message = null)
    {
        if (appUser is not null)
            throw new AppUserException(message ?? $"User with email {appUser.Email} existed");
    }


    /// <summary>
    /// If user isDeleted is false then throw error
    /// </summary>
    /// <param name="isDeleted"></param>
    /// <exception cref="AppUserException"></exception>
    internal static void ThrowIfUnDoDeleteUnDeletedUser(bool isDeleted)
    {
        ThrowIfGetDeletedRecord(isDeleted == false);
    }
}
