using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.Entities.Interviews;
using InterviewManagementSystem.Domain.Entities.Jobs;
using InterviewManagementSystem.Domain.Entities.MasterData;
using InterviewManagementSystem.Domain.Entities.Offers;
using InterviewManagementSystem.Domain.Interfaces;
using InterviewManagementSystem.Infrastructure.Databases.PostgreSQL.Repositories;
using InterviewManagementSystem.Infrastructure.Persistences;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Data.Common;

namespace InterviewManagementSystem.Infrastructure.UnitOfWorks;

public sealed class UnitOfWork : IUnitOfWork, IDisposable
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
    public IBaseRepository<Job> JobRepository { get; set; }
    public IBaseRepository<Offer> OfferRepository { get; set; }
    public IBaseRepository<Skill> SkillRepository { get; set; }
    public IBaseRepository<Level> LevelRepository { get; set; }
    public IBaseRepository<AppUser> AppUserRepository { get; set; }
    public IBaseRepository<Candidate> CandidateRepository { get; set; }
    public IBaseRepository<Benefit> BenefitRepository { get; set; }
    public IBaseRepository<InterviewSchedule> InterviewScheduleRepository { get; set; }
    #endregion




    public UnitOfWork(InterviewManagementSystemContext interviewManagementSystemContext)
    {

        _dbContext = interviewManagementSystemContext;
        _interviewManagementSystemContext = interviewManagementSystemContext;

        JobRepository = new BaseRepository<Job>(_interviewManagementSystemContext);
        OfferRepository = new BaseRepository<Offer>(_interviewManagementSystemContext);
        SkillRepository = new BaseRepository<Skill>(_interviewManagementSystemContext);
        LevelRepository = new BaseRepository<Level>(_interviewManagementSystemContext);
        BenefitRepository = new BaseRepository<Benefit>(_interviewManagementSystemContext);
        AppUserRepository = new BaseRepository<AppUser>(_interviewManagementSystemContext);
        CandidateRepository = new BaseRepository<Candidate>(_interviewManagementSystemContext);
        InterviewScheduleRepository = new BaseRepository<InterviewSchedule>(_interviewManagementSystemContext);
    }




    #region Transaction Methods
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
    #endregion




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
            var t when t == typeof(Job) => (IBaseRepository<T>)JobRepository,
            var t when t == typeof(Skill) => (IBaseRepository<T>)SkillRepository,
            var t when t == typeof(Level) => (IBaseRepository<T>)LevelRepository,
            var t when t == typeof(Offer) => (IBaseRepository<T>)OfferRepository,
            var t when t == typeof(Benefit) => (IBaseRepository<T>)BenefitRepository,
            var t when t == typeof(InterviewSchedule) => (IBaseRepository<T>)InterviewScheduleRepository,
            _ => new BaseRepository<T>(_interviewManagementSystemContext)
        };
    }

}
