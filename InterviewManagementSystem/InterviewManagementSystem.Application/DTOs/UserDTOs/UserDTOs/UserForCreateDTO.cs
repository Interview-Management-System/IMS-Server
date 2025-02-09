namespace InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs
{
    public record UserForCreateDTO : BaseUserDTO
    {
        public Guid RoleId { get; set; }
        public bool IsActive { get; set; }
        public DepartmentEnum DepartmentId { get; set; }
    }
}
