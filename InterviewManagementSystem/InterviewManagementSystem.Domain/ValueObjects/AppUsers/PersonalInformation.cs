namespace InterviewManagementSystem.Domain.ValueObjects.AppUsers
{
    public sealed record PersonalInformation
    {
        public bool Gender { get; set; }
        public DateTime Dob { get; set; }
        public string? Address { get; set; }
        public string? Username { get; set; }
        public string? PhoneNumber { get; set; }
        public string Email { get; set; } = default!;
    }
}
