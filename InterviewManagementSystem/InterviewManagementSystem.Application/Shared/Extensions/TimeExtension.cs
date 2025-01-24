namespace InterviewManagementSystem.Application.Shared.Extensions
{
    public static class TimeExtension
    {

        private const string HOUR_AND_MINUTE_ONLY = "HH:mm";


        public static string ToNewFormat(this TimeOnly timeOnly)
        {
            return timeOnly.ToString(HOUR_AND_MINUTE_ONLY);
        }
    }
}
