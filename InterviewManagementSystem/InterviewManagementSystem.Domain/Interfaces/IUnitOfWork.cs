using System.Data.Common;
using Microsoft.EntityFrameworkCore;

namespace InterviewManagementSystem.Domain.Interfaces
{
    public interface IUnitOfWork
    {

        #region Repository Propterties
        IBaseRepository<Offer> OfferRepository { get; }
        IBaseRepository<Job> JobRepository { get; }
        IBaseRepository<InterviewSchedule> InterviewScheduleRepository { get; }
        #endregion



        #region Db Propterties
        DbContext DbContext { get; }
        DbConnection Connection { get; }
        DbTransaction? Transaction { get; }
        DbContext InterviewManagementSystemContext { get; }
        #endregion



        IBaseRepository<T> GetBaseRepository<T>() where T : class;



        Task CommitAsync();
        Task RollbackAsync();
        Task<DbTransaction> BeginTransactionAsync();
        Task<bool> SaveChangesAsync();
    }
}
