using InterviewManagementSystem.Domain.Exceptions;

namespace InterviewManagementSystem.Domain.ValueObjects
{
    public sealed record HourPeriod
    {
        public TimeOnly StartHour { get; private set; }
        public TimeOnly EndHour { get; private set; }


        private HourPeriod(TimeOnly startHour, TimeOnly endHour)
        {
            CheckValidHourPeriod(startHour, endHour);

            StartHour = startHour;
            EndHour = endHour;
        }


        public static HourPeriod CreatePeriod(TimeOnly startHour, TimeOnly endHour)
        {
            return new HourPeriod(startHour, endHour);
        }



        private static void CheckValidHourPeriod(TimeOnly startHour, TimeOnly endHour)
        {
            if (startHour >= endHour)
                throw new InvalidPeriodException("Start hour must be less than end hour.");


            if (startHour < TimeOnly.FromTimeSpan(TimeSpan.FromHours(0)) && startHour > TimeOnly.FromTimeSpan(TimeSpan.FromHours(24)))
                throw new InvalidPeriodException("Start hour must be within 00:00 to 23:59.");

            if (endHour < TimeOnly.FromTimeSpan(TimeSpan.FromHours(0)) && endHour > TimeOnly.FromTimeSpan(TimeSpan.FromHours(24)))
                throw new InvalidPeriodException("End hour must be within 00:00 to 23:59.");
        }

    }
}
