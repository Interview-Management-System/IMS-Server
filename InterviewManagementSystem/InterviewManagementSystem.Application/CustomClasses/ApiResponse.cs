namespace InterviewManagementSystem.Application.CustomClasses;

public sealed class ApiResponse<T>
{
    public T? Data { get; set; } = default;
    public string? Message { get; set; }
}
