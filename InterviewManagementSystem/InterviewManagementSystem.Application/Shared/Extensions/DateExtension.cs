namespace InterviewManagementSystem.Application.Shared.Extensions;

public static class DateExtension
{

    private const string TIME_ONLY = "HH:mm:ss";
    private const string VIE_DATE_FORMAT = "dd/MM/yyyy";
    private const string VIE_DATE_FORMAT_WITH_TIME = "dd/MM/yyyy HH:mm:ss";


    public static string ToVieFormat(this DateTime date)
    {
        return date.ToString(VIE_DATE_FORMAT);
    }


    public static string ToVieFormatWithTime(this DateTime date)
    {
        return date.ToString(VIE_DATE_FORMAT_WITH_TIME);
    }


    public static string GetTime(this DateTime date)
    {
        return date.ToString(TIME_ONLY);
    }
}
