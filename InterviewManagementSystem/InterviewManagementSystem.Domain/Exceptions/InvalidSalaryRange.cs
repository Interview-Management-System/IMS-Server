namespace InterviewManagementSystem.Domain.Exceptions
{
    internal sealed class InvalidSalaryRange(string message) : DomainException(message)
    {
    }
}
