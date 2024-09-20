﻿using System.Linq.Expressions;
using InterviewManagementSystem.Domain.Paginations;
using Microsoft.EntityFrameworkCore.Query;

namespace InterviewManagementSystem.Domain.Interfaces;

public interface IBaseRepository<T> where T : class
{
    Task<List<T>> GetAllAsync();

    Task<T> GetByIdAsync(Guid id);

    Task AddAsync(T entity);

    Task AddRangeAsync(IEnumerable<T> entities);

    void Update(T entity);

    void Delete(T entity, bool isHardDelete = false);

    void Delete(IEnumerable<T> entities, bool isHardDelete = false);

    void Delete(Expression<Func<T, bool>> where, bool isHardDelete = false);

    IQueryable<T> GetQuery();

    IQueryable<T> GetQuery(Expression<Func<T, bool>> where);



    /// <summary>
    /// Get entities by condition
    /// </summary>
    /// <param name="condition">Condition to get entities</param>
    /// <param name="size">Number of entity to return for each page</param>
    /// <param name="page">Page index</param>
    /// <returns></returns>
    Task<PageResult<T>> GetByPageAsync(PaginationParameter<T> pagingParameter);



    /// <summary>
    /// Include entity property but does not support ThenInclude()
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="orderBy"></param>
    /// <param name="includeProperties"></param>
    /// <param name="canLoadDeleted"></param>
    /// <returns></returns>
    IQueryable<T> GetWithInclude(Expression<Func<T, bool>> filter = null!, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null!, string includeProperties = "", bool canLoadDeleted = false, bool isNoTracking = false);




    Task<PageResult<T>> GetByPageWithIncludeAsync(PaginationParameter<T> pagingParameter, IEnumerable<string>? includeProperties = null);




    /// <summary>
    /// This method support ThenInclude(). Use this method if need to multiple Include or ThenInclude. NOTE: if want to use this method, must provide includes = [] for overloading the GetWithInclude() above.
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="orderBy"></param>
    /// <param name="includes"></param>
    /// <param name="canLoadDeleted"></param>
    /// <returns>List of type T</returns>
    IQueryable<T> GetWithInclude(Expression<Func<T, bool>> filter = null!, Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>>[] includes = null!);
}
