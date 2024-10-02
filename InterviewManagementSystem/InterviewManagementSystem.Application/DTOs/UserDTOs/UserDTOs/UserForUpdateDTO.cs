namespace InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs
{
    public sealed record UserForUpdateDTO : BaseUserDTO
    {
        [Required]
        public Guid Id { get; set; }
    }
}
