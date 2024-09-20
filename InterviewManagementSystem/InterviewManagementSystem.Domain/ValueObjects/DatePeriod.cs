namespace InterviewManagementSystem.Domain.ValueObjects;

public sealed record DatePeriod
{


    private static readonly DateTime MinDate = new(2000, 1, 1);
    private static readonly DateTime MaxDate = new(2100, 12, 31);


    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }



    public DatePeriod(DateTime startDate, DateTime endDate)
    {

        if (IsValidRange(startDate, endDate) == false)
            throw new ArgumentException("Invalid date range.");

        StartDate = startDate;
        EndDate = endDate;
    }



    private static bool IsValidRange(DateTime startDate, DateTime endDate)
    {
        if (startDate >= endDate)
        {
            Console.WriteLine("Error: Start date must be less than the end date.");
            return false;
        }

        if (startDate < MinDate || startDate > MaxDate)
        {
            Console.WriteLine($"Error: Start date must be between {MinDate.ToShortDateString()} and {MaxDate.ToShortDateString()}.");
            return false;
        }

        if (endDate < MinDate || endDate > MaxDate)
        {
            Console.WriteLine($"Error: End date must be between {MinDate.ToShortDateString()} and {MaxDate.ToShortDateString()}.");
            return false;
        }

        return true;
    }
}
