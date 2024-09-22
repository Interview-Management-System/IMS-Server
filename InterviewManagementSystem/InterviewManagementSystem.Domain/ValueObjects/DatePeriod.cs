using InterviewManagementSystem.Domain.Exceptions;

namespace InterviewManagementSystem.Domain.ValueObjects;

public sealed record DatePeriod
{

    private static readonly DateTime MinDate = new(2000, 1, 1);
    private static readonly DateTime MaxDate = new(2100, 12, 31);


    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }



    private DatePeriod(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate;
        EndDate = endDate;
    }



    public static DatePeriod Create(DateTime startDate, DateTime endDate)
    {
        CheckValidDateRange(startDate, endDate);
        return new DatePeriod(startDate, endDate);
    }




    private static void CheckValidDateRange(DateTime startDate, DateTime endDate)
    {

        ArgumentNullException.ThrowIfNull(startDate, "Start date must not empty");
        ArgumentNullException.ThrowIfNull(endDate, "End date must not empty");

        if (startDate >= endDate)
            throw new InvalidPeriodException("Error: Start date must be less than the end date.");


        if (startDate < MinDate || startDate > MaxDate)
        {
            string errorMsg = $"Error: Start date must be between {MinDate.ToShortDateString()} and {MaxDate.ToShortDateString()}.";
            throw new InvalidPeriodException(errorMsg);
        }


        if (endDate < MinDate || endDate > MaxDate)
        {
            string errorMsg = $"Error: End date must be between {MinDate.ToShortDateString()} and {MaxDate.ToShortDateString()}.";
            throw new InvalidPeriodException(errorMsg);
        }

    }
}
