namespace InterviewManagementSystem.Domain.ValueObjects;

public sealed record DatePeriod
{


    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }




    public DatePeriod()
    {

    }


    private DatePeriod(DateTime startDate, DateTime endDate)
    {
        StartDate = startDate.ToUniversalTime();
        EndDate = endDate.ToUniversalTime();
    }



    public static DatePeriod Create(DateTime startDate, DateTime endDate)
    {
        //InvalidPeriodException.ThrowIfInvalidDatePeriod(startDate, endDate);
        return new DatePeriod(startDate, endDate);
    }

}
