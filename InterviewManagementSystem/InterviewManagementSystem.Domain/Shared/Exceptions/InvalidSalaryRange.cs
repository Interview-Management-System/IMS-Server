namespace InterviewManagementSystem.Domain.Shared.Exceptions
{
    internal sealed class InvalidSalaryRange(string message) : DomainException(message)
    {
    }
}
