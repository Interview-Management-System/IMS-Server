using InterviewManagementSystem.Domain.Entities;
using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.Entities.Interviews;
using InterviewManagementSystem.Domain.Entities.Jobs;
using InterviewManagementSystem.Domain.Entities.MasterData;
using InterviewManagementSystem.Domain.Entities.Offers;
using InterviewManagementSystem.Infrastructure.Databases.PostgreSQL.EntityConfigurations;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace InterviewManagementSystem.Infrastructure.Databases.PostgreSQL;


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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        base.OnConfiguring(optionsBuilder);
    }


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
        this.HanldeUpdateAt();
        //HandleSoftDelete();
        return base.SaveChangesAsync(cancellationToken);
    }



    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);



    private void HanldeUpdateAt()
    {
        const string updateAtPropertyName = nameof(BaseEntity.UpdateAt);
        var editedEntities = ChangeTracker.Entries().Where(E => E.State == EntityState.Modified);


        var entitiesWithUpdateAtProperty = editedEntities
            .Where(ee => ee.Entity.GetType().GetProperty(updateAtPropertyName)?.Name == updateAtPropertyName);


        if (entitiesWithUpdateAtProperty.Any())
        {

            foreach (var entry in entitiesWithUpdateAtProperty)
            {
                var entity = entry.Entity;
                var updateAtProperty = entity.GetType().GetProperty(updateAtPropertyName);

                updateAtProperty?.SetValue(entity, DateTime.Now);
            }

            //entitiesWithUpdateAtProperty.ForEach(E => E.Property(updateAtPropertyName).CurrentValue = DateTime.Now);
        }
    }




    private void HandleSoftDelete()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            var entity = entry.Entity;
            var isDeletedProperty = entity.GetType().GetProperty(nameof(BaseEntity.IsDeleted));

            if (isDeletedProperty != null && entry.State == EntityState.Deleted)
            {
                entry.State = EntityState.Modified; // Prevent actual deletion
                isDeletedProperty.SetValue(entity, true); // Set IsDeleted = true
            }
        }
    }
}