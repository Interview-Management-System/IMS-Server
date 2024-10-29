using InterviewManagementSystem.Application.CustomClasses;
using InterviewManagementSystem.Domain.Entities;
using InterviewManagementSystem.Domain.Interfaces;
using InterviewManagementSystem.Domain.Paginations;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace InterviewManagementSystem.Infrastructure.Persistences.Repositories;

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
            SetDeleteFieldToTrue(entity);
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
                SetDeleteFieldToTrue(entity);
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

        return entity;
    }



    public async Task<PageResult<T>> GetByPageAsync(PaginationParameter<T> pagingParameter)
    {
        IQueryable<T> query = _dbSet;



        var filters = pagingParameter.Filters;

        if (filters != null)
            foreach (var filter in filters)
                query = query.Where(filter);




        var pagingOrderBy = pagingParameter.OrderBy;

        if (pagingOrderBy != null)
            query = pagingOrderBy(query);


        CancellationToken cancellationToken = CancellationTokenProvider.CancellationToken;


        var totalRecord = await query.CountAsync(cancellationToken);

        var items = await query
            .Skip(pagingParameter.PageSize * (pagingParameter.PageIndex - 1))
            .Take(pagingParameter.PageSize)
            .ToListAsync(cancellationToken);


        return new PageResult<T>()
        {
            TotalRecords = totalRecord,
            Items = items,
            PageIndex = pagingParameter.PageIndex,
            PageSize = pagingParameter.PageSize,
        };

    }



    public async Task<PageResult<T>> GetByPageWithIncludeAsync(PaginationParameter<T> pagingParameter, IEnumerable<string>? includeProperties = null)
    {
        IQueryable<T> query = _dbSet.AsNoTracking();


        var filters = pagingParameter.Filters;

        // Apply filters
        if (filters != null)
            foreach (var filter in filters)
                query = query.Where(filter);



        // Apply includes
        if (includeProperties != null)
            foreach (var includeProperty in includeProperties)
                query = query.Include(includeProperty);



        query = query.AsSplitQuery();
        var orderBy = pagingParameter.OrderBy;


        if (orderBy != null)
            query = orderBy(query);


        CancellationToken cancellationToken = CancellationTokenProvider.CancellationToken;
        var totalRecord = await query.CountAsync(cancellationToken);


        int pageSize = pagingParameter.PageSize;
        int pageNumber = pagingParameter.PageIndex;


        var items = await query
            .Skip(pageSize * (pageNumber - 1))
            .Take(pageSize)
            .ToListAsync(cancellationToken);



        return new PageResult<T>()
        {
            TotalRecords = totalRecord,
            Items = items,
            PageIndex = pageNumber,
            PageSize = pageSize,
        };

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






    private static void SetDeleteFieldToTrue(T entity)
    {
        var propertyInfo = entity.GetType().GetProperty(nameof(BaseEntity.IsDeleted));

        if (propertyInfo != null && propertyInfo.PropertyType == typeof(bool))
        {
            propertyInfo.SetValue(entity, true);
        }
    }

}
