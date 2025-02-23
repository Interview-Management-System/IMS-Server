namespace InterviewManagementSystem.Application.DTOs.UserDTOs
{
    public abstract record BaseUserDTO
    {
        public string? Note { get; set; }
        public PersonalInformation PersonalInformation { get; set; } = new();
    }


    public sealed record PersonalInformation
    {
        public bool Gender { get; set; }
        public DateTime? Dob { get; set; }
        public string? Email { get; set; }
        public string? Address { get; set; }
        public string? Username { get; set; }
        public string? AvatarLink { get; set; }
        public string? PhoneNumber { get; set; }
    }



    public sealed record UserStatus
    {
        public bool IsActive { get; set; }
        public string? StatusText => UserStatusHelper.GetUserStatusText(IsActive);
    }


    public record UserIdentityRetrieveDTO
    {
        public Guid Id { get; set; }
        public string? Username { get; set; }
    }
}
