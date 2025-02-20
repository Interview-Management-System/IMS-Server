using InterviewManagementSystem.Domain.Entities;
using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.Entities.Interviews;
using InterviewManagementSystem.Domain.Entities.Jobs;
using InterviewManagementSystem.Domain.Entities.MasterData;
using InterviewManagementSystem.Domain.Entities.Offers;
using InterviewManagementSystem.Infrastructure.Databases.PostgreSQL.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InterviewManagementSystem.Infrastructure.Persistences;


public partial class InterviewManagementSystemContext : IdentityDbContext<AppUser, AppRole, Guid>
{

    public InterviewManagementSystemContext()
    {
    }

    public InterviewManagementSystemContext(DbContextOptions<InterviewManagementSystemContext> options) : base(options)
    {
    }

    #region Db Sets
    public virtual DbSet<AppRole> AppRoles { get; set; }

    public virtual DbSet<AppRoleClaim> AppRoleClaims { get; set; }

    public virtual DbSet<AppUser> AppUsers { get; set; }

    public virtual DbSet<AppUserClaim> AppUserClaims { get; set; }

    public virtual DbSet<AppUserLogin> AppUserLogins { get; set; }

    public virtual DbSet<AppUserToken> AppUserTokens { get; set; }

    public virtual DbSet<Benefit> Benefits { get; set; }

    public virtual DbSet<Candidate> Candidates { get; set; }

    public virtual DbSet<CandidateOfferStatus> CandidateOfferStatuses { get; set; }

    public virtual DbSet<CandidateStatus> CandidateStatuses { get; set; }

    public virtual DbSet<ContractType> ContractTypes { get; set; }

    public virtual DbSet<Department> Departments { get; set; }

    public virtual DbSet<HighestLevel> HighestLevels { get; set; }

    public virtual DbSet<InterviewResult> InterviewResults { get; set; }

    public virtual DbSet<InterviewSchedule> InterviewSchedules { get; set; }

    public virtual DbSet<InterviewScheduleStatus> InterviewScheduleStatuses { get; set; }

    public virtual DbSet<Job> Jobs { get; set; }

    public virtual DbSet<JobStatus> JobStatuses { get; set; }

    public virtual DbSet<Level> Levels { get; set; }

    public virtual DbSet<Offer> Offers { get; set; }

    public virtual DbSet<OfferStatus> OfferStatuses { get; set; }

    public virtual DbSet<Position> Positions { get; set; }

    public virtual DbSet<Skill> Skills { get; set; }

    #endregion


}




/// <summary>
/// For methods
/// </summary>
public partial class InterviewManagementSystemContext
{

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ConfigureJob();
        modelBuilder.ConfigureOffer();
        modelBuilder.ConfigureAppUser();
        modelBuilder.ConfigureIdentity();
        modelBuilder.ConfigureInterview();
        modelBuilder.ConfigureMasterData();

        OnModelCreatingPartial(modelBuilder);
    }




    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {

        var editedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified).ToList();


        const string updateAtPropertyName = nameof(BaseEntity.UpdateAt);

        var entitiesWithUpdateAtProperty = editedEntities
            .Where(ee => ee.Entity.GetType().GetProperty(updateAtPropertyName)?.Name == updateAtPropertyName)
            .ToList();


        if (entitiesWithUpdateAtProperty.Count > 0)
        {
            entitiesWithUpdateAtProperty.ForEach(E => E.Property(updateAtPropertyName).CurrentValue = DateTime.Now);
        }


        return base.SaveChangesAsync(cancellationToken);
    }



    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}