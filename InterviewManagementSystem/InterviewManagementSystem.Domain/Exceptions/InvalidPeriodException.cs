namespace InterviewManagementSystem.Domain.Exceptions
{
    public sealed class InvalidPeriodException(string message) : DomainException(message)
    {
    }
}
