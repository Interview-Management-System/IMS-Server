using Microsoft.EntityFrameworkCore;
using System.Data.Common;

namespace InterviewManagementSystem.Domain.Interfaces
{
    public interface IUnitOfWork
    {

        #region Repository Propterties
        IBaseRepository<Job> JobRepository { get; protected set; }
        IBaseRepository<Level> LevelRepository { get; protected set; }
        IBaseRepository<Skill> SkillRepository { get; protected set; }
        IBaseRepository<Offer> OfferRepository { get; protected set; }
        IBaseRepository<Benefit> BenefitRepository { get; protected set; }
        IBaseRepository<AppUser> AppUserRepository { get; protected set; }
        IBaseRepository<Candidate> CandidateRepository { get; protected set; }
        IBaseRepository<InterviewSchedule> InterviewScheduleRepository { get; protected set; }
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
