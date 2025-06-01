using InterviewManagementSystem.Application.Shared;
using InterviewManagementSystem.Domain.Entities;
using InterviewManagementSystem.Domain.Interfaces;
using InterviewManagementSystem.Domain.Shared.Paginations;
using InterviewManagementSystem.Infrastructure.Databases.PostgreSQL.Extensions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace InterviewManagementSystem.Infrastructure.Databases.PostgreSQL.Repositories;

public class BaseRepository<T> : IBaseRepository<T> where T : class
{

    protected readonly DbSet<T> _dbSet;
    protected readonly InterviewManagementSystemContext _interviewManagementSystemContext;


    public BaseRepository(InterviewManagementSystemContext interviewManagementSystemContext)
    {
        _dbSet = interviewManagementSystemContext.Set<T>();
        _interviewManagementSystemContext = interviewManagementSystemContext;
    }


    #region Create
    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity, CancellationTokenProvider.CancellationToken);
    }



    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities, CancellationTokenProvider.CancellationToken);
    }
    #endregion



    #region Update
    public void Update(T entity)
    {
        _dbSet.Update(entity);
        _interviewManagementSystemContext.Entry(entity).State = EntityState.Modified;
    }


    public async Task<(bool, int)> BulkUpdateAsync(Expression<Func<T, bool>> filter, Expression<Func<SetPropertyCalls<T>, SetPropertyCalls<T>>> updateExpression)
    {
        int rowAffected = await _dbSet
            .ApplyFilter(filter)
            .ExecuteUpdateAsync(updateExpression, CancellationTokenProvider.CancellationToken);

        return (rowAffected > 0, rowAffected);
    }
    #endregion



    #region Retrieve
    public async Task<List<TResult>> GetAllAsync<TResult>(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IQueryable<TResult>>? projection = null, bool isTracking = false)
    {
        IQueryable<T> queryable = isTracking ? _dbSet : _dbSet.AsNoTracking();

        return await queryable.ApplyFilter([filter!]).ApplyProjection(projection).ToListAsync();
    }



    public async Task<T?> GetByIdAsync<TId>(TId id, bool isTracking = false)
    {
        var entity = await _dbSet.FindAsync(id);

        if (isTracking == false && entity is not null)
        {
            _interviewManagementSystemContext.Entry(entity).State = EntityState.Detached;
        }

        return entity ?? default;
    }


    public async Task<TResult?> GetByIdAsync<TResult>(object id, string? idIdentifier = "Id", Func<IQueryable<T>, IQueryable<TResult>>? projection = null, bool isTracking = false)
    {

        var query = isTracking ? _dbSet : _dbSet.AsNoTracking();

        return await query
            .Where(e => EF.Property<object>(e, idIdentifier!).Equals(id))
            .ApplyProjection(projection)
            .SingleOrDefaultAsync();
    }


    public async Task<PageResult<TResult>> GetPaginationList<TResult>(PaginationQuery<T, TResult> paginationQuery)
    {

        IQueryable<T> query = _dbSet.AsNoTracking().AsSplitQuery();
        var cancellationToken = CancellationTokenProvider.CancellationToken;


        var pagingParameter = paginationQuery.PagingParameter;

        query = query
            .Where(f => !EF.Property<bool>(f, nameof(BaseEntity.IsDeleted)))
            .ApplyFilter([.. pagingParameter.Filters])
            .ApplyFullTextSearch(pagingParameter.SearchText);


        var totalRecords = await query.CountAsync(cancellationToken);


        int pageSize = pagingParameter.PageSize;
        int pageIndex = pagingParameter.PageIndex;


        var items = await query
           .ApplySortCriteria(pagingParameter.SortCriteria)
           .ApplyPagination(pageSize, pageIndex)
           .ApplyProjection(paginationQuery.Projection)
           .ToListAsync(cancellationToken);


        return new PageResult<TResult>()
        {
            Items = items,
            PageSize = pageSize,
            PageIndex = pageIndex,
            TotalRecords = totalRecords,
        };
    }


    public async Task<PageResult<T>> GetPaginationList(PaginationParameter<T> pagingParameter, IEnumerable<string>? includeProperties = null)
    {
        var paginationQuery = new PaginationQuery<T, T>() { PagingParameter = pagingParameter, IncludeProperties = includeProperties };
        return await GetPaginationList(paginationQuery);
    }


    public IQueryable<T> GetWithInclude(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool canLoadDeleted = false, bool isTracking = false, params string[] includeProperties)
    {
        IQueryable<T> query = isTracking ? _dbSet : _dbSet.AsNoTracking();
        query.ApplyFilter(filter!).IncludeProperties(includeProperties);

        return orderBy != null ? orderBy(query) : query;
    }
    #endregion



    public async Task<(bool, int)> InstantDeleteAsync(Expression<Func<T, bool>> filter)
    {
        int rowAffected = await _dbSet
            .ApplyFilter([filter])
            .ExecuteDeleteAsync(CancellationTokenProvider.CancellationToken);

        return (rowAffected > 0, rowAffected);
    }
}

