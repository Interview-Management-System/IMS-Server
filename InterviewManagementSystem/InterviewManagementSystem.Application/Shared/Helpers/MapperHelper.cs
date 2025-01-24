using AutoMapper.QueryableExtensions;

namespace InterviewManagementSystem.Application.Shared.Helpers;

internal static class MapperHelper
{

    internal static List<T> GetListFromContext<T>(ResolutionContext context, string key)
    {
        if (context.Items.TryGetValue(key, out var value) && value is List<T> list)
        {
            return list;
        }

        return [];
    }



    internal static T GetContextItem<T>(ResolutionContext context, string key)
    {
        return context.Items.TryGetValue(key, out object? value) ? (T)value : default!;
    }



    public static Func<IQueryable<TSource>, IQueryable<TResult>> CreateProjection<TSource, TResult>(IMapper mapper)
    {
        return query => query.ProjectTo<TResult>(mapper.ConfigurationProvider);
    }

}
