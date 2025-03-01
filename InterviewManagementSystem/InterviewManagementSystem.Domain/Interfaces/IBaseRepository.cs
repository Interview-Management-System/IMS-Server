using InterviewManagementSystem.Domain.Shared.Paginations;
using System.Linq.Expressions;

namespace InterviewManagementSystem.Domain.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IQueryable<TResult>>? projection = null, bool isTracking = false);

    Task<T?> GetByIdAsync<TId>(TId id, bool isTracking = false);

    Task<TResult?> GetByIdAsync<TResult>(object id, string? idIdentifier = "Id", Func<IQueryable<T>, IQueryable<TResult>>? projection = null, bool isTracking = false);

    Task AddAsync(T entity);

    Task AddRangeAsync(IEnumerable<T> entities);

    void Update(T entity);

    void Delete(T entity, bool isHardDelete = false);

    void DeleteRange(IEnumerable<T> entities, bool isHardDelete = false);

    void DeleteRangeWithConditions(Expression<Func<T, bool>> where, bool isHardDelete = false);

    IQueryable<T> GetQuery();

    IQueryable<T> GetQuery(Expression<Func<T, bool>> where);


    /// <summary>
    /// Include entity property but does not support ThenInclude()
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="orderBy"></param>
    /// <param name="includeProperties"></param>
    /// <param name="canLoadDeleted"></param>
    /// <returns></returns>
    IQueryable<T> GetWithInclude(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool canLoadDeleted = false, bool isTracking = false, params string[] includeProperties);


    Task<PageResult<T>> GetPaginationList(PaginationParameter<T> pagingParameter, IEnumerable<string>? includeProperties = null);


    Task<PageResult<TResult>> GetPaginationList<TResult>(PaginationParameter<T> pagingParameter, IEnumerable<string>? includeProperties = null, Func<IQueryable<T>, IQueryable<TResult>>? projection = null);


    /// <summary>
    /// This method support ThenInclude(). Use this method if need to multiple Include or ThenInclude. NOTE: if want to use this method, must provide includes = [] for overloading the GetWithInclude() above.
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="orderBy"></param>
    /// <param name="includes"></param>
    /// <param name="canLoadDeleted"></param>
    /// <returns>List of type T</returns>
    //IQueryable<T> GetWithInclude(Expression<Func<T, bool>>? filter, Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>>[]? includes);
}
