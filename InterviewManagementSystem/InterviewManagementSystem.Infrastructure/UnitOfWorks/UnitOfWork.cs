using System.Data;
using System.Data.Common;
using InterviewManagementSystem.Domain.Entities.Interviews;
using InterviewManagementSystem.Domain.Entities.Jobs;
using InterviewManagementSystem.Domain.Entities.Offers;
using InterviewManagementSystem.Domain.Interfaces;
using InterviewManagementSystem.Infrastructure.Persistences;
using InterviewManagementSystem.Infrastructure.Persistences.Repositories;
using Microsoft.EntityFrameworkCore;

namespace InterviewManagementSystem.Infrastructure.UnitOfWorks;

public sealed class UnitOfWork : IDisposable, IUnitOfWork
{
    #region Fields
    private DbConnection? _connection;
    private readonly DbContext _dbContext;
    private DbTransaction? _transaction = null;
    private readonly InterviewManagementSystemContext _interviewManagementSystemContext;
    #endregion



    #region Properties
    public DbContext DbContext { get => _dbContext; }
    public DbTransaction? Transaction { get => _transaction; }
    public DbConnection Connection { get => _connection ??= _dbContext.Database.GetDbConnection(); }
    public DbContext InterviewManagementSystemContext { get => _interviewManagementSystemContext; }
    #endregion



    #region Repositories
    private readonly IBaseRepository<Offer>? _offerRepository;
    public IBaseRepository<Offer> OfferRepository
    {
        get => _offerRepository ?? new BaseRepository<Offer>(_interviewManagementSystemContext);
    }



    private readonly IBaseRepository<Job>? _jobRepository;
    public IBaseRepository<Job> JobRepository
    {
        get => _jobRepository ?? new BaseRepository<Job>(_interviewManagementSystemContext);
    }



    private readonly IBaseRepository<InterviewSchedule>? _InterviewScheduleRepository;
    public IBaseRepository<InterviewSchedule> InterviewScheduleRepository
    {
        get => _InterviewScheduleRepository ?? new BaseRepository<InterviewSchedule>(_interviewManagementSystemContext);
    }
    #endregion




    public UnitOfWork(InterviewManagementSystemContext interviewManagementSystemContext)
    {
        _dbContext = interviewManagementSystemContext;
        _interviewManagementSystemContext = interviewManagementSystemContext;
    }





    public async Task<DbTransaction> BeginTransactionAsync()
    {
        _connection ??= _dbContext.Database.GetDbConnection();

        if (_connection.State == ConnectionState.Open)
        {
            _transaction = await _connection.BeginTransactionAsync();
        }
        else
        {
            await _connection.OpenAsync();
            _transaction = await _connection.BeginTransactionAsync(); ;
        }

        return _transaction;
    }




    public async Task CommitAsync()
    {
        if (_transaction != null)
        {
            await _transaction.CommitAsync();
        }
        await DisposeAsync();
    }




    public async Task RollbackAsync()
    {
        if (_transaction != null)
        {
            await _transaction.RollbackAsync();
        }
        await DisposeAsync();
    }



    public async ValueTask DisposeAsync()
    {
        if (_transaction != null)
        {
            await _transaction.DisposeAsync();
        }
        if (_connection != null)
        {
            await _connection.DisposeAsync();
        }
    }


    public async Task<bool> SaveChangesAsync()
    {
        return (await _interviewManagementSystemContext.SaveChangesAsync() > 0);
    }



    public void Dispose()
    {
        GC.SuppressFinalize(this);
        //_interviewManagementSystemContext.Database.EnsureDeleted();
    }





    public IBaseRepository<T> GetBaseRepository<T>() where T : class
    {
        return typeof(T) switch
        {
            var t when t == typeof(Offer) => (IBaseRepository<T>)OfferRepository,
            var t when t == typeof(Job) => (IBaseRepository<T>)JobRepository,
            var t when t == typeof(InterviewSchedule) => (IBaseRepository<T>)InterviewScheduleRepository,
            _ => new BaseRepository<T>(_interviewManagementSystemContext)
        };
    }

}
