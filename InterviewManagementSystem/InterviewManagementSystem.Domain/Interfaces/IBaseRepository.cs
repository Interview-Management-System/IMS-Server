using InterviewManagementSystem.Domain.Shared.Paginations;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace InterviewManagementSystem.Domain.Interfaces;

public interface IBaseRepository<T> where T : class
{

    #region Retrieve

    /// <summary>
    /// Find by primary key
    /// </summary>
    /// <typeparam name="TId"></typeparam>
    /// <param name="id"></param>
    /// <param name="isTracking"></param>
    /// <returns></returns>
    Task<T?> GetByIdAsync<TId>(TId id, bool isTracking = false);


    Task<PageResult<TResult>> GetPaginationList<TResult>(PaginationQuery<T, TResult> paginationQuery)
        ;

    Task<PageResult<T>> GetPaginationList(PaginationParameter<T> pagingParameter, IEnumerable<string>? includeProperties = null);


    Task<TResult?> GetByIdAsync<TResult>(object id, string? idIdentifier = "Id", Func<IQueryable<T>, IQueryable<TResult>>? projection = null, bool isTracking = false);


    Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IQueryable<TResult>>? projection = null, bool isTracking = false);


    /// <summary>
    /// Include entity property but does not support ThenInclude()
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="orderBy"></param>
    /// <param name="includeProperties"></param>
    /// <param name="canLoadDeleted"></param>
    /// <returns></returns>
    IQueryable<T> GetWithInclude(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool canLoadDeleted = false, bool isTracking = false, params string[] includeProperties);
    #endregion





    #region Create
    Task AddAsync(T entity);

    Task AddRangeAsync(IEnumerable<T> entities);
    #endregion






    #region Update
    void Update(T entity);


    /// <summary>
    /// This method will update to DB instantly without save changes from DbContext
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="updateExpression"></param>
    /// <returns></returns>
    Task<bool> InstantUpdateAsync(Expression<Func<T, bool>> filter, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression);
    #endregion





    #region Delete
    /// <summary>
    /// This method will delete record(s) in DB instantly without save changes from DbContext
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="updateExpression"></param>
    /// <returns></returns>
    Task<bool> InstantDeleteAsync(Expression<Func<T, bool>> filter);
    #endregion
}


/// <summary>
/// Support ThenInclude()
/// </summary>
/// <param name="filter"></param>
/// <param name="includes"></param>
/// <returns></returns>
/// 
/*
public IQueryable<T> GetWithInclude(Expression<Func<T, bool>>? filter, Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>>[]? includes)
{
    IQueryable<T> query = _dbSet.AsNoTracking();


    if (filter != null)
    {
        query = query.Where(filter);
    }


    if (includes != null && includes.Length > 0)
    {
        foreach (var include in includes)
        {
            query = include.Compile()(query);

        }
    }

    query = query.AsSplitQuery();

    return query;

}
*/
