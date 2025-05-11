namespace InterviewManagementSystem.Application.Shared;

public sealed class ApiResponse<T>
{
    public T? Data { get; set; } = default;
    public string? Message { get; set; } = null;
}


/// <summary>
/// A builder/factory for dicrectly creating <see cref="ApiResponse{T}"/> instances.
/// </summary>
public static class ApiResponse
{
    public static ApiResponse<T> Create<T>(T? data, string? message = default)
    {
        return new ApiResponse<T>
        {
            Data = data ?? default,
            Message = message
        };
    }
}