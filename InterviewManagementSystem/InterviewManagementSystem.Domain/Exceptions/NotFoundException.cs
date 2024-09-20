namespace InterviewManagementSystem.Domain.Exceptions
{
    public sealed class NotFoundException(string message) : DomainException(message)
    {
    }
}
