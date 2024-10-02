using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Data.Common;

namespace InterviewManagementSystem.Infrastructure.Persistences.Interceptors;

internal class CustomCommandInterceptor : DbCommandInterceptor
{

    /*
    public Task<DbCommand> ReaderExecutingAsync(DbCommand command, CommandEventData eventData, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Executing command: {command.CommandText}");
        return base.ReaderExecutingAsync(command, eventData, cancellationToken);
    }*/


    public override ValueTask<DbDataReader> ReaderExecutedAsync(DbCommand command, CommandExecutedEventData eventData, DbDataReader result, CancellationToken cancellationToken = default)
    {
        Console.WriteLine($"Executing command: {command.CommandText}");
        return base.ReaderExecutedAsync(command, eventData, result, cancellationToken);
    }
}
