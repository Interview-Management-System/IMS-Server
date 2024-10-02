using InterviewManagementSystem.Application.CustomClasses.Attributes;

namespace InterviewManagementSystem.Application.DTOs;

public sealed record UserLoginRequest
{
    [Required]
    [StringLength(50), EmailAddress]
    public required string Email { get; set; }

    [Required]
    [StringLength(50), MinLength(6)]
    public required string Password { get; set; }
}




public sealed record ForgetPasswordRequest
{
    [Required, EmailAddress]
    public required string Email { get; set; }
}



public sealed record ResetPasswordRequest
{
    [Required]
    public required string Email { get; set; }


    [Required]
    public required string Token { get; set; }


    [Required]
    public required string NewPassword { get; set; }


    [Required, ComparePasswords(nameof(NewPassword))]
    public required string ConfirmPassword { get; set; }
}

