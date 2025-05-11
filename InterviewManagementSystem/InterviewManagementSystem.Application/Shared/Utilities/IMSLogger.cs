using Serilog;
using Serilog.Events;

namespace InterviewManagementSystem.Application.Shared.Utilities;

public static class IMSLogger
{
    public static void Info(string message)
    {
        Log.Information(message);
    }


    public static void Warn(string message)
    {
        Log.Warning(message);
    }


    public static void Error(string message)
    {
        Log.Error(message);
    }


    public static void Fatal(string message)
    {
        Log.Fatal(message);
    }


    public static void Success(string message, params object[] propertyValues)
    {
        Log.Logger.ForContext("IsSuccess", true).Write(LogEventLevel.Information, $"{message}", propertyValues);
    }
}
