namespace InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs
{
    public sealed record UserForUpdateDTO : BaseUserDTO
    {
        [Required]
        public Guid Id { get; set; }
        public Guid RoleId { get; set; }
        public bool IsActive { get; set; }
        public DepartmentEnum DepartmentId { get; set; }
    }
}
