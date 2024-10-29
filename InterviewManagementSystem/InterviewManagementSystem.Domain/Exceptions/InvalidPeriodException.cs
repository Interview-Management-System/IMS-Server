namespace InterviewManagementSystem.Domain.Exceptions;

internal sealed class InvalidPeriodException(string message) : DomainException(message)
{


    private static readonly DateTime MinDate = new(2000, 1, 1);
    private static readonly DateTime MaxDate = new(2100, 12, 31);



    internal static void ThrowIfInvalidHourPeriod(TimeOnly startHour, TimeOnly endHour)
    {
        if (startHour >= endHour)
            throw new InvalidPeriodException("Start hour must be less than end hour.");


        if (startHour < TimeOnly.FromTimeSpan(TimeSpan.FromHours(0)) && startHour > TimeOnly.FromTimeSpan(TimeSpan.FromHours(24)))
            throw new InvalidPeriodException("Start hour must be within 00:00 to 23:59.");


        if (endHour < TimeOnly.FromTimeSpan(TimeSpan.FromHours(0)) && endHour > TimeOnly.FromTimeSpan(TimeSpan.FromHours(24)))
            throw new InvalidPeriodException("End hour must be within 00:00 to 23:59.");
    }




    internal static void ThrowIfInvalidDatePeriod(DateTime startDate, DateTime endDate)
    {

        string errorMessage = string.Empty;

        if (startDate >= endDate)
            errorMessage = "Error: Start date must be less than the end date.";



        if (startDate < MinDate || startDate > MaxDate)
            errorMessage = $"Error: Start date must be between {MinDate.ToShortDateString()} and {MaxDate.ToShortDateString()}.";



        if (endDate < MinDate || endDate > MaxDate)
            errorMessage = $"Error: End date must be between {MinDate.ToShortDateString()} and {MaxDate.ToShortDateString()}.";


        throw new InvalidPeriodException(errorMessage);
    }
}
