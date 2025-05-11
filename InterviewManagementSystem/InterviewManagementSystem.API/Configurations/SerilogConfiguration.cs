using Serilog;
using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting;
using Serilog.Formatting.Display;

namespace InterviewManagementSystem.API.Configurations;


public static class SerilogConfiguration
{
    public static void ConfigureSerilog()
    {
        Log.Logger = new LoggerConfiguration().WriteTo.ColoredConsole().CreateLogger();
    }
}




public static class SeriLogExtensions
{

    public static LoggerConfiguration ColoredConsole(
        this LoggerSinkConfiguration loggerConfiguration,
        LogEventLevel minimumLevel = LogEventLevel.Verbose,
        string outputTemplate = "[{Timestamp:HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}",
        IFormatProvider formatProvider = null!)
    {
        return loggerConfiguration
            .Sink(new ColoredConsoleSink(new MessageTemplateTextFormatter(outputTemplate, formatProvider)), minimumLevel);
    }


    public static void Success(this Serilog.ILogger logger, string messageTemplate, params object[] propertyValues)
    {
        logger.ForContext("IsSuccess", true).Write(LogEventLevel.Information, messageTemplate, propertyValues);
    }
}



internal sealed class ColoredConsoleSink(ITextFormatter formatter) : ILogEventSink
{

    private readonly ConsoleColor _defaultForeground = Console.ForegroundColor;
    private readonly ConsoleColor _defaultBackground = Console.BackgroundColor;


    public void Emit(LogEvent logEvent)
    {
        if (logEvent.Level >= LogEventLevel.Fatal)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.BackgroundColor = ConsoleColor.Red;
        }
        else if (logEvent.Level >= LogEventLevel.Error)
        {
            Console.ForegroundColor = ConsoleColor.Red;
        }
        else if (logEvent.Level >= LogEventLevel.Warning)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
        }
        else if (logEvent.Properties.ContainsKey("IsSuccess"))
        {
            Console.ForegroundColor = ConsoleColor.Green;
        }

        formatter.Format(logEvent, Console.Out);
        Console.Out.Flush();

        Console.ForegroundColor = _defaultForeground;
        Console.BackgroundColor = _defaultBackground;
    }
}
