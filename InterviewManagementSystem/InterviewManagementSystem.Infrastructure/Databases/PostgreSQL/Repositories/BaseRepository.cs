using InterviewManagementSystem.Application.Shared;
using InterviewManagementSystem.Domain.Entities;
using InterviewManagementSystem.Domain.Interfaces;
using InterviewManagementSystem.Domain.Shared.Paginations;
using InterviewManagementSystem.Infrastructure.Databases.PostgreSQL.Extensions;
using InterviewManagementSystem.Infrastructure.Persistences;
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


    public async Task AddAsync(T entity)
    {
        await _dbSet.AddAsync(entity);
    }



    public async Task AddRangeAsync(IEnumerable<T> entities)
    {
        await _dbSet.AddRangeAsync(entities);
    }



    public void Delete(T entity, bool isHardDelete = false)
    {
        if (isHardDelete)
        {
            _dbSet.Remove(entity);
        }
        else
        {
            SetDeleteField(entity, true);
            _interviewManagementSystemContext.Entry(entity).State = EntityState.Modified;
        }
    }



    public void DeleteRange(IEnumerable<T> entities, bool isHardDelete = false)
    {
        if (isHardDelete)
        {
            _dbSet.RemoveRange(entities);
        }
        else
        {
            foreach (var entity in entities)
            {
                SetDeleteField(entity, true);
            }
        }
    }



    public void DeleteRangeWithConditions(Expression<Func<T, bool>> where, bool isHardDelete = false)
    {
        var entities = GetQuery(where).AsEnumerable();
        DeleteRange(entities, isHardDelete);
    }



    public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>>? filter = null, bool isTracking = false)
    {

        IQueryable<T> queryable = _dbSet;


        if (filter != null)
        {
            queryable = queryable.Where(filter);
        }


        if (isTracking)
        {
            return await queryable.ToListAsync();
        }

        return await queryable.AsNoTracking().ToListAsync();
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


    public async Task<PageResult<TResult>> GetPaginationList<TResult>(PaginationParameter<T> pagingParameter, IEnumerable<string>? includeProperties = null, Func<IQueryable<T>, IQueryable<TResult>>? projection = null)
    {

        IQueryable<T> query = _dbSet.AsNoTracking().AsSplitQuery();

        var cancellationToken = CancellationTokenProvider.CancellationToken;
        var totalRecords = await query.CountAsync(f => !EF.Property<bool>(f, nameof(BaseEntity.IsDeleted)), cancellationToken);


        int pageSize = pagingParameter.PageSize;
        int pageNumber = pagingParameter.PageIndex;


        var items = await query
           .ApplyFilter(pagingParameter.Filters)
           .IncludeProperties(includeProperties)
           .ApplySortCriteria(pagingParameter.SortCriteria)
           .ApplyPagination(pageSize, pageNumber)
           //.ApplyFreeTextSearch(pagingParameter.SearchText, ["Email"])
           .ApplyProjection(projection)
           .ToListAsync(cancellationToken);


        return new PageResult<TResult>()
        {
            TotalRecords = totalRecords,
            Items = items,
            PageIndex = pageNumber,
            PageSize = pageSize,
        };
    }


    public async Task<PageResult<T>> GetPaginationList(PaginationParameter<T> pagingParameter, IEnumerable<string>? includeProperties = null)
    {
        return await GetPaginationList<T>(pagingParameter, includeProperties);
    }


    public IQueryable<T> GetQuery()
    {
        return _dbSet;
    }



    public IQueryable<T> GetQuery(Expression<Func<T, bool>> where)
    {
        return _dbSet.Where(where);
    }



    public IQueryable<T> GetWithInclude(Expression<Func<T, bool>>? filter = null, Func<IQueryable<T>, IOrderedQueryable<T>>? orderBy = null, bool canLoadDeleted = false, bool isTracking = false, params string[] includeProperties)
    {
        IQueryable<T> query = isTracking ? _dbSet : _dbSet.AsNoTracking();


        if (filter != null)
        {
            query = query.Where(filter);
        }


        foreach (var includeProperty in includeProperties)
        {
            query = query.Include(includeProperty);
        }

        return orderBy != null ? orderBy(query) : query;

    }


    /// <summary>
    /// Support ThenInclude()
    /// </summary>
    /// <param name="filter"></param>
    /// <param name="includes"></param>
    /// <returns></returns>
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



    public void Update(T entity)
    {
        _dbSet.Update(entity);
        _interviewManagementSystemContext.Entry(entity).State = EntityState.Modified;
    }


    private static void SetDeleteField(T entity, bool isDeleted)
    {
        var propertyInfo = entity.GetType().GetProperty(nameof(BaseEntity.IsDeleted));

        if (propertyInfo?.PropertyType == typeof(bool))
        {
            propertyInfo.SetValue(entity, isDeleted);
        }
    }
}

