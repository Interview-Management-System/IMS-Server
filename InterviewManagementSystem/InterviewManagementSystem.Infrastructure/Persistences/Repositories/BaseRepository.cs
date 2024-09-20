using System.Linq.Expressions;
using InterviewManagementSystem.Domain.Interfaces;
using InterviewManagementSystem.Domain.Paginations;
using Microsoft.EntityFrameworkCore.Query;

namespace InterviewManagementSystem.Infrastructure.Persistences.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{



    private static readonly char[] separators = [','];




    public Task AddAsync(T entity)
    {
        throw new NotImplementedException();
    }



    public Task AddRangeAsync(IEnumerable<T> entities)
    {
        throw new NotImplementedException();
    }



    public void Delete(T entity, bool isHardDelete = false)
    {
        throw new NotImplementedException();
    }



    public void Delete(IEnumerable<T> entities, bool isHardDelete = false)
    {
        throw new NotImplementedException();
    }



    public void Delete(Expression<Func<T, bool>> where, bool isHardDelete = false)
    {
        throw new NotImplementedException();
    }



    public Task<List<T>> GetAllAsync()
    {
        throw new NotImplementedException();
    }



    public Task<T> GetByIdAsync(Guid id)
    {
        throw new NotImplementedException();
    }



    public Task<PageResult<T>> GetByPageAsync(PaginationParameter<T> pagingParameter)
    {
        throw new NotImplementedException();
    }



    public Task<PageResult<T>> GetByPageWithIncludeAsync(PaginationParameter<T> pagingParameter, IEnumerable<string>? includeProperties = null)
    {
        throw new NotImplementedException();
    }



    public IQueryable<T> GetQuery()
    {
        throw new NotImplementedException();
    }



    public IQueryable<T> GetQuery(Expression<Func<T, bool>> where)
    {
        throw new NotImplementedException();
    }



    public IQueryable<T> GetWithInclude(Expression<Func<T, bool>> filter = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeProperties = "", bool canLoadDeleted = false, bool isNoTracking = false)
    {
        throw new NotImplementedException();
    }



    public IQueryable<T> GetWithInclude(Expression<Func<T, bool>> filter = null, Expression<Func<IQueryable<T>, IIncludableQueryable<T, object>>>[] includes = null)
    {
        throw new NotImplementedException();
    }



    public void Update(T entity)
    {
        throw new NotImplementedException();
    }
}
