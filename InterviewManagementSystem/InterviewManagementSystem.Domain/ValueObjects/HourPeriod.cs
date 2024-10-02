using InterviewManagementSystem.Domain.Exceptions;

namespace InterviewManagementSystem.Domain.ValueObjects
{
    public sealed record HourPeriod
    {
        public TimeOnly StartHour { get; private set; }
        public TimeOnly EndHour { get; private set; }


        private HourPeriod(TimeOnly startHour, TimeOnly endHour)
        {
            StartHour = startHour;
            EndHour = endHour;
        }


        public static HourPeriod CreatePeriod(TimeOnly startHour, TimeOnly endHour)
        {
            InvalidPeriodException.ThrowIfInvalidHourPeriod(startHour, endHour);
            return new HourPeriod(startHour, endHour);
        }

    }
}
