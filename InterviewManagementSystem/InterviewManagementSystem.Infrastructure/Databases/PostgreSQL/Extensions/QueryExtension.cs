namespace InterviewManagementSystem.Infrastructure.Databases.PostgreSQL.Extensions;

internal static class QueryExtension
{
    internal static IQueryable<TOut> Pipe<TIn, TOut>(this IQueryable<TIn> query, Func<IQueryable<TIn>, IQueryable<TOut>>? transform = null)
    {
        return transform == null ? (transform = q => q.Cast<TOut>())(query) : transform(query);
    }
}
