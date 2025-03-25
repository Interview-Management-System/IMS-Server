using AutoMapper.QueryableExtensions;

namespace InterviewManagementSystem.Application.Shared.Helpers;

public static class MapperHelper
{

    private static IMapper? _mapper;


    public static void InitializeMapperInstance(IMapper mapper)
    {
        _mapper = mapper;
    }



    public static Func<IQueryable<TSource>, IQueryable<TResult>> CreateProjection<TSource, TResult>()
    {
        return query => query.ProjectTo<TResult>(_mapper!.ConfigurationProvider);
    }


    /// <summary>
    /// Regular map, for create or to map required properties
    /// </summary>
    /// <typeparam name="TDestination"></typeparam>
    /// <param name="source"></param>
    /// <returns></returns>
    public static TDestination Map<TDestination>(object source)
    {
        return _mapper!.Map<TDestination>(source);
    }


    /// <summary>
    /// Map from the source to the existing destination (Update).
    /// </summary>
    /// <typeparam name="TSource"></typeparam>
    /// <typeparam name="TDestination"></typeparam>
    /// <param name="source"></param>
    /// <param name="destination"></param>
    /// <returns></returns>
    public static TDestination Map<TSource, TDestination>(TSource source, TDestination destination)
    {
        return _mapper!.Map(source, destination);
    }
}
