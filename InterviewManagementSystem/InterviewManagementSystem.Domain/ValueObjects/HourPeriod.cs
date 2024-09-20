namespace InterviewManagementSystem.Domain.ValueObjects
{
    public sealed record HourPeriod
    {
        public TimeOnly StartHour { get; private set; }
        public TimeOnly EndHour { get; private set; }


        public HourPeriod(TimeOnly startHour, TimeOnly endHour)
        {
            StartHour = startHour;
            EndHour = endHour;
        }

    }
}
