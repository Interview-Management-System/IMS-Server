using InterviewManagementSystem.Domain.Shared.Paginations;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace InterviewManagementSystem.Infrastructure.Databases.PostgreSQL.Extensions;

internal static class QueryExtension
{
    internal static IQueryable<TOut> ApplyProjection<TIn, TOut>(this IQueryable<TIn> query, Func<IQueryable<TIn>, IQueryable<TOut>>? transform = null)
    {
        return transform == null ? (transform = q => q.Cast<TOut>())(query) : transform(query);
    }


    public static IQueryable<TResult> Pipe1<T, TResult>(this DbSet<T> dbSet, Func<IQueryable<T>, IQueryable<TResult>>? queryModifier = null) where T : class
    {
        // If a query modifier (projection or other query transformation) is provided, apply it to the DbSet
        if (queryModifier != null)
        {
            return queryModifier(dbSet.AsQueryable());
        }

        // If no query modifier is provided, return the DbSet casted to TResult
        return dbSet.Cast<TResult>();
    }




    internal static IQueryable<T> ApplyFilter<T>(this IQueryable<T> query, List<Expression<Func<T, bool>>> filters)
    {
        if (filters != null)
        {
            foreach (var filter in filters)
            {
                query = query.Where(filter);
            }
        }
        return query;
    }



    internal static IQueryable<T> ApplySortCriteria<T>(this IQueryable<T> query, SortCriteria? sortCriteria)
    {
        if (sortCriteria == null || string.IsNullOrWhiteSpace(sortCriteria.SortProperty))
            return query;


        const BindingFlags bindingFlags = BindingFlags.IgnoreCase | BindingFlags.Public | BindingFlags.Instance;


        var propertyInfo = typeof(T).GetProperty(sortCriteria.SortProperty, bindingFlags);
        ArgumentNullException.ThrowIfNull(propertyInfo, $"'{sortCriteria.SortProperty}' does not exist on type '{typeof(T).Name}'");


        var parameter = Expression.Parameter(typeof(T), "x");
        var property = Expression.PropertyOrField(parameter, sortCriteria.SortProperty);
        var propertyAccess = Expression.Lambda(property, parameter);


        var methodName = sortCriteria.IsAscending ? "OrderBy" : "OrderByDescending";

        var method = typeof(Queryable)
            .GetMethods()
            .First(m => m.Name == methodName && m.GetParameters().Length == 2);


        var genericMethod = method.MakeGenericMethod(typeof(T), property.Type);

        return (IQueryable<T>)genericMethod.Invoke(null, [query, propertyAccess])!;
    }



    internal static IQueryable<T> IncludeProperties<T>(this IQueryable<T> query, IEnumerable<string>? includeProperties = null) where T : class
    {
        if (includeProperties != null)
        {
            foreach (var includeProperty in includeProperties)
            {
                query = query.Include(includeProperty);
            }
        }
        return query;
    }



    internal static IQueryable<T> ApplyPagination<T>(this IQueryable<T> query, int pageSize, int pageNumber)
    {
        return query.Skip(pageSize * (pageNumber - 1)).Take(pageSize);
    }


    internal static IQueryable<T> ApplyFreeTextSearch<T>(this IQueryable<T> query, string? searchText, params string[] columns)
    {
        if (string.IsNullOrEmpty(searchText))
            throw new ArgumentException("Search term cannot be null or empty.", nameof(searchText));

        if (columns == null || columns.Length == 0)
            throw new ArgumentNullException(nameof(columns));

        // Parameter for the entity (e.g., `entity =>`)
        var parameter = Expression.Parameter(typeof(T), "entity");

        // Get the `EF.Functions.Contains` method reference
        var containsMethod = typeof(DbFunctionsExtensions)
            .GetMethod("Contains", [typeof(DbFunctions), typeof(string), typeof(string)]);

        if (containsMethod == null)
            throw new InvalidOperationException("EF.Functions.Contains method not found.");

        // Create the `EF.Functions` parameter
        var efFunctions = Expression.Property(null, typeof(EF).GetProperty(nameof(EF.Functions))!);

        // Build a condition for each column
        Expression? combinedCondition = null;

        foreach (var columnName in columns)
        {
            // Access the column (e.g., `entity.ColumnName`)
            var property = Expression.Property(parameter, columnName);

            // Build the `EF.Functions.Contains(property, searchTerm)` expression
            var containsExpression = Expression.Call(
                containsMethod,
                efFunctions,
                property,
                Expression.Constant(searchText)
            );

            // Combine conditions using OR
            combinedCondition = combinedCondition == null
                ? containsExpression
                : Expression.OrElse(combinedCondition, containsExpression);
        }

        if (combinedCondition == null)
            throw new InvalidOperationException("No valid conditions were created.");

        // Create the final lambda expression
        var lambda = Expression.Lambda<Func<T, bool>>(combinedCondition, parameter);


        return query.Where(lambda);
    }
}
