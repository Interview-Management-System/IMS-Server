using InterviewManagementSystem.Domain.Entities.Jobs;
using InterviewManagementSystem.Domain.Entities.MasterData;
using InterviewManagementSystem.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace InterviewManagementSystem.Infrastructure.Databases.PostgreSQL.EntityConfigurations;

internal static class JobConfiguration
{
    internal static void ConfigureJob(this ModelBuilder modelBuilder)
    {

        /*
        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Jobs_pkey");
            entity.ToTable("Jobs", "IMS");


            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasColumnType("character varying");
            entity.Property(e => e.Title).HasColumnType("character varying");
            entity.Property(e => e.WorkingAddress).HasColumnType("character varying");


            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.JobCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("Jobs_CreatedBy_fkey");


            entity.HasOne(d => d.JobStatus).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.JobStatusId)
                .HasConstraintName("Jobs_JobStatusId_fkey");


            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.JobUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("Jobs_UpdatedBy_fkey");


            entity.HasMany(d => d.Benefits).WithMany(p => p.Jobs)
                .UsingEntity<Dictionary<string, object>>(
                    "JobBenefit",
                    r => r.HasOne<Benefit>().WithMany()
                        .HasForeignKey("BenefitId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("JobBenefits_BenefitId_fkey"),
                    l => l.HasOne<Job>().WithMany()
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("JobBenefits_JobId_fkey"),
                    j =>
                    {
                        j.HasKey("JobId", "BenefitId").HasName("JobBenefits_pkey");
                        j.ToTable("JobBenefits", "IMS");
                    });


            entity.HasMany(d => d.Levels).WithMany(p => p.Jobs)
                .UsingEntity<Dictionary<string, object>>(
                    "JobLevel",
                    r => r.HasOne<Level>().WithMany()
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("JobLevels_LevelId_fkey"),
                    l => l.HasOne<Job>().WithMany()
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("JobLevels_JobId_fkey"),
                    j =>
                    {
                        j.HasKey("JobId", "LevelId").HasName("JobLevels_pkey");
                        j.ToTable("JobLevels", "IMS");
                    });


            entity.HasMany(d => d.Skills).WithMany(p => p.Jobs)
                .UsingEntity<Dictionary<string, object>>(
                    "JobSkill",
                    r => r.HasOne<Skill>().WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("JobSkills_SkillId_fkey"),
                    l => l.HasOne<Job>().WithMany()
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("JobSkills_JobId_fkey"),
                    j =>
                    {
                        j.HasKey("JobId", "SkillId").HasName("JobSkills_pkey");
                        j.ToTable("JobSkills", "IMS");
                    });


            entity.OwnsOne(d => d.DatePeriod, offer =>
            {
                offer.Property(dp => dp.StartDate).HasColumnName(nameof(DatePeriod.StartDate));
                offer.Property(dp => dp.EndDate).HasColumnName(nameof(DatePeriod.EndDate));
            });

            entity.OwnsOne(d => d.SalaryRange, offer =>
            {
                offer.Property(sr => sr.From).HasColumnName(nameof(SalaryRange.From)).HasColumnType("money");
                offer.Property(sr => sr.To).HasColumnName(nameof(SalaryRange.To)).HasColumnType("money");
            });
        });
        */



        modelBuilder.Entity<Job>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Jobs_pkey");

            entity.ToTable("Jobs", "IMS");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Description).HasColumnType("character varying");
            entity.Property(e => e.Title).HasColumnType("character varying");
            entity.Property(e => e.WorkingAddress).HasColumnType("character varying");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.JobCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("Jobs_CreatedBy_fkey");

            entity.HasOne(d => d.JobStatus).WithMany(p => p.Jobs)
                .HasForeignKey(d => d.JobStatusId)
                .HasConstraintName("Jobs_JobStatusId_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.JobUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("Jobs_UpdatedBy_fkey");

            entity.HasMany(d => d.Benefits).WithMany(p => p.Jobs)
                .UsingEntity<Dictionary<string, object>>(
                    "JobBenefit",
                    r => r.HasOne<Benefit>().WithMany()
                        .HasForeignKey("BenefitId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("JobBenefits_BenefitId_fkey"),
                    l => l.HasOne<Job>().WithMany()
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("JobBenefits_JobId_fkey"),
                    j =>
                    {
                        j.HasKey("JobId", "BenefitId").HasName("JobBenefits_pkey");
                        j.ToTable("JobBenefits", "IMS");
                    });

            entity.HasMany(d => d.Levels).WithMany(p => p.Jobs)
                .UsingEntity<Dictionary<string, object>>(
                    "JobLevel",
                    r => r.HasOne<Level>().WithMany()
                        .HasForeignKey("LevelId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("JobLevels_LevelId_fkey"),
                    l => l.HasOne<Job>().WithMany()
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("JobLevels_JobId_fkey"),
                    j =>
                    {
                        j.HasKey("JobId", "LevelId").HasName("JobLevels_pkey");
                        j.ToTable("JobLevels", "IMS");
                    });

            entity.HasMany(d => d.Skills).WithMany(p => p.Jobs)
                .UsingEntity<Dictionary<string, object>>(
                    "JobSkill",
                    r => r.HasOne<Skill>().WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("JobSkills_SkillId_fkey"),
                    l => l.HasOne<Job>().WithMany()
                        .HasForeignKey("JobId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("JobSkills_JobId_fkey"),
                    j =>
                    {
                        j.HasKey("JobId", "SkillId").HasName("JobSkills_pkey");
                        j.ToTable("JobSkills", "IMS");
                    });

            entity.OwnsOne(d => d.DatePeriod, offer =>
            {
                offer.Property(dp => dp.StartDate).HasColumnName(nameof(DatePeriod.StartDate));
                offer.Property(dp => dp.EndDate).HasColumnName(nameof(DatePeriod.EndDate));
            });

            entity.OwnsOne(d => d.SalaryRange, offer =>
            {
                offer.Property(sr => sr.From).HasColumnName(nameof(SalaryRange.From)).HasColumnType("money");
                offer.Property(sr => sr.To).HasColumnName(nameof(SalaryRange.To)).HasColumnType("money");
            });
        });


        modelBuilder.Entity<JobStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("JobStatuses_pkey");
            entity.ToTable("JobStatuses", "IMS");
            entity.Property(e => e.Name).HasColumnType("character varying");
        });
    }
}
