namespace InterviewManagementSystem.API.SignalR.Hubs;

public class BaseHub<T> : Hub<T> where T : class
{

    public async override Task OnConnectedAsync()
    {
        await base.OnConnectedAsync();
    }


    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        await base.OnDisconnectedAsync(exception);
    }


    /// <summary>
    /// Use for cancel all query/execution when connection is aborted. This can apply for HTTP request (DB query will also cancel)
    /// </summary>
    /// <typeparam name="TResult">The type of the result returned by the action.</typeparam>
    /// <param name="action">A function representing the asynchronous action to execute.</param>
    /// <returns>A task that represents the asynchronous operation, containing the result of the action.</returns>
    /// <exception cref="OperationCanceledException">Thrown if the connection is aborted during execution.</exception>
    protected async Task<TResult> ExecuteWithCancellationAsync<TResult>(Func<Task<TResult>> action)
    {
        Context.ConnectionAborted.ThrowIfCancellationRequested();
        return await action();
    }
}



public class CancellationFilter : IHubFilter
{
    public async ValueTask<object?> InvokeMethodAsync(HubInvocationContext ctx, Func<HubInvocationContext, ValueTask<object?>> next)
    {
        ctx.Context.ConnectionAborted.ThrowIfCancellationRequested();
        return await next(ctx);
    }
}
