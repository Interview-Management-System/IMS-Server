namespace InterviewManagementSystem.Application.DTOs.UserDTOs.UserDTOs;

public record UserRetrieveDTO : BaseUserDTO
{
    public Guid Id { get; set; }
    public string? Role { get; set; }
    public UserStatus? UserStatus { get; set; }
}



public sealed record UserDetailRetrieveDTO : UserRetrieveDTO
{
    public DepartmentEnum DepartmentId { get; set; }
    public string? Department => DepartmentId.GetEnumName();
    public string? Gender => GenderHelper.GetGenderText(PersonalInformation!.Gender);
}



public sealed record UserPaginationRetrieveDTO
{
    public Guid Id { get; set; }
    public string? Role { get; set; }
    public string? Email { get; set; }
    public bool IsDeleted { get; set; }
    public string? Username { get; set; }
    public string? PhoneNumber { get; set; }
    public UserStatus UserStatus { get; set; } = new();
}
