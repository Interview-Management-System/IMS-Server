using InterviewManagementSystem.Domain.Shared.Exceptions;

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


        public static HourPeriod CreatePeriod(string startHourString, string endHourString)
        {
            TimeOnly startHour = TimeOnly.Parse(startHourString);
            TimeOnly endHour = TimeOnly.Parse(endHourString);

            PeriodException.ThrowIfInvalidHourPeriod(startHour, endHour);
            return new HourPeriod(startHour, endHour);
        }

    }
}
