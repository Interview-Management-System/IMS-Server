namespace InterviewManagementSystem.Application.DTOs.UserDTOs
{
    public abstract record BaseUserDTO
    {
        public string? Username { get; set; }
        public string Email { get; set; } = default!;
        public DateTime Dob { get; set; }
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public bool Gender { get; set; }
        public Guid RoleId { get; set; }
        public Guid CreatedBy { get; set; }
        public DepartmentEnum DepartmentId { get; set; }
        public bool IsActive { get; set; } = true;
        public string? Note { get; set; }
    }
}
