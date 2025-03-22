using InterviewManagementSystem.Domain.Entities;
using InterviewManagementSystem.Domain.Shared.Paginations;
using Microsoft.EntityFrameworkCore;
using NpgsqlTypes;
using System.Linq.Expressions;
using System.Reflection;

namespace InterviewManagementSystem.Infrastructure.Databases.PostgreSQL.Extensions;

internal static class QueryExtension
{

    internal static IQueryable<TOut> ApplyProjection<TIn, TOut>(this IQueryable<TIn> query, Func<IQueryable<TIn>, IQueryable<TOut>>? transform = null)
    {
        return transform == null ? (transform = q => q.Cast<TOut>())(query) : transform(query);
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



    internal static IQueryable<T> ApplyFullTextSearch<T>(this IQueryable<T> query, string? searchText)
    {
        if (string.IsNullOrWhiteSpace(searchText))
        {
            return query;
        }

        const string searchVectorPropertyName = nameof(ISearchable.SearchVector);


        var searchVectorProperty = typeof(T).GetProperty(searchVectorPropertyName);
        if (searchVectorProperty?.PropertyType != typeof(NpgsqlTsVector))
        {
            return query;
        }

        return query.Where(e => EF.Property<NpgsqlTsVector>(e!, searchVectorPropertyName).Matches(EF.Functions.ToTsQuery("english", $"{searchText}")));
    }
}
