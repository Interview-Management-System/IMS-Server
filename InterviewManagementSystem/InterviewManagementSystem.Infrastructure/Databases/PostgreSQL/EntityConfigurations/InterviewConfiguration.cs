using InterviewManagementSystem.Domain.Entities.Interviews;
using InterviewManagementSystem.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace InterviewManagementSystem.Infrastructure.Databases.PostgreSQL.EntityConfigurations;

internal static class InterviewConfiguration
{
    internal static void ConfigureInterview(this ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<InterviewResult>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("InterviewResults_pkey");
            entity.ToTable("InterviewResults", "IMS");
            entity.Property(e => e.Name).HasColumnType("character varying");
        });




        modelBuilder.Entity<InterviewSchedule>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("InterviewSchedules_pkey");

            entity.ToTable("InterviewSchedules", "IMS");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Location).HasColumnType("character varying");
            entity.Property(e => e.MeetingUrl)
                .HasColumnType("character varying")
                .HasColumnName("MeetingURL");
            entity.Property(e => e.Note).HasColumnType("character varying");
            entity.Property(e => e.ScheduleTime).HasColumnType("timestamp without time zone");
            entity.Property(e => e.Title).HasColumnType("character varying");

            entity.HasOne(d => d.Candidate).WithMany(p => p.InterviewScheduleCandidates)
                .HasForeignKey(d => d.CandidateId)
                .HasConstraintName("InterviewSchedules_CandidateId_fkey");

            entity.HasOne(d => d.CandidateStatus).WithMany(p => p.InterviewSchedules)
                .HasForeignKey(d => d.CandidateStatusId)
                .HasConstraintName("InterviewSchedules_CandidateStatusId_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InterviewScheduleCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("InterviewSchedules_CreatedBy_fkey");

            entity.HasOne(d => d.InterviewResult).WithMany(p => p.InterviewSchedules)
                .HasForeignKey(d => d.InterviewResultId)
                .HasConstraintName("InterviewSchedules_InterviewResultId_fkey");

            entity.HasOne(d => d.InterviewScheduleStatus).WithMany(p => p.InterviewSchedules)
                .HasForeignKey(d => d.InterviewScheduleStatusId)
                .HasConstraintName("InterviewSchedules_InterviewScheduleStatusId_fkey");

            entity.HasOne(d => d.Job).WithMany(p => p.InterviewSchedules)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("InterviewSchedules_JobId_fkey");

            entity.HasOne(d => d.Offer).WithMany(p => p.InterviewSchedules)
                .HasForeignKey(d => d.OfferId)
                .HasConstraintName("InterviewSchedules_OfferId_fkey");

            entity.HasOne(d => d.RecruiterOwner).WithMany(p => p.InterviewScheduleRecruiterOwners)
                .HasForeignKey(d => d.RecruiterOwnerId)
                .HasConstraintName("InterviewSchedules_RecruiterOwnerId_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.InterviewScheduleUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("InterviewSchedules_UpdatedBy_fkey");

            entity.OwnsOne(d => d.HourPeriod, i =>
            {
                i.Property(sr => sr.StartHour).HasColumnName(nameof(HourPeriod.StartHour));
                i.Property(sr => sr.EndHour).HasColumnName(nameof(HourPeriod.EndHour));
            });
        });




        modelBuilder.Entity<InterviewScheduleStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("InterviewScheduleStatuses_pkey");
            entity.ToTable("InterviewScheduleStatuses", "IMS");
            entity.Property(e => e.Name).HasColumnType("character varying");
        });
    }
}
