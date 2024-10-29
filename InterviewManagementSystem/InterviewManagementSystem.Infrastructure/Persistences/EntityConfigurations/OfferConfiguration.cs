using InterviewManagementSystem.Domain.Entities.Offers;
using InterviewManagementSystem.Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace InterviewManagementSystem.Infrastructure.Persistences.EntityConfigurations;

internal static class OfferConfiguration
{
    internal static void ConfigureOffer(this ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Offer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Offers_pkey");

            entity.ToTable("Offers", "IMS");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.BasicSalary).HasColumnType("money");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Note).HasColumnType("character varying");

            entity.HasOne(d => d.Approver).WithMany(p => p.OfferApprovers)
                .HasForeignKey(d => d.ApproverId)
                .HasConstraintName("Offers_ApproverId_fkey");

            entity.HasOne(d => d.Candidate).WithMany(p => p.Offers)
                .HasForeignKey(d => d.CandidateId)
                .HasConstraintName("Offers_CandidateId_fkey");

            entity.HasOne(d => d.ContractType).WithMany(p => p.Offers)
                .HasForeignKey(d => d.ContractTypeId)
                .HasConstraintName("Offers_ContractTypeId_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.OfferCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("Offers_CreatedBy_fkey");

            entity.HasOne(d => d.Department).WithMany(p => p.Offers)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("Offers_DepartmentId_fkey");

            entity.HasOne(d => d.CandidateOfferStatus).WithOne(p => p.Offer)
                .HasPrincipalKey<CandidateOfferStatus>(p => p.OfferId)
                .HasForeignKey<Offer>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Offers_Id_fkey");

            entity.HasOne(d => d.InterviewSchedule).WithMany(p => p.Offers)
                .HasForeignKey(d => d.InterviewScheduleId)
                .HasConstraintName("Offers_InterviewScheduleId_fkey");

            entity.HasOne(d => d.Level).WithMany(p => p.Offers)
                .HasForeignKey(d => d.LevelId)
                .HasConstraintName("Offers_LevelId_fkey");

            entity.HasOne(d => d.OfferStatus).WithMany(p => p.Offers)
                .HasForeignKey(d => d.OfferStatusId)
                .HasConstraintName("Offers_OfferStatusId_fkey");

            entity.HasOne(d => d.Position).WithMany(p => p.Offers)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("Offers_PositionId_fkey");

            entity.HasOne(d => d.RecruiterOwner).WithMany(p => p.OfferRecruiterOwners)
                .HasForeignKey(d => d.RecruiterOwnerId)
                .HasConstraintName("Offers_RecruiterOwnerId_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.OfferUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("Offers_UpdatedBy_fkey");


            entity.OwnsOne(d => d.DatePeriod, offer =>
            {
                offer.Property(dp => dp.StartDate).HasColumnName(nameof(DatePeriod.StartDate));
                offer.Property(dp => dp.EndDate).HasColumnName(nameof(DatePeriod.EndDate));
            });
        });



        /*
        modelBuilder.Entity<Offer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Offers_pkey");

            entity.ToTable("Offers", "IMS");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.BasicSalary).HasColumnType("money");
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.Note).HasColumnType("character varying");

            entity.HasOne(d => d.Approver).WithMany(p => p.OfferApprovers)
                .HasForeignKey(d => d.ApproverId)
                .HasConstraintName("Offers_ApproverId_fkey");

            entity.HasOne(d => d.Candidate).WithMany(p => p.Offers)
                .HasForeignKey(d => d.CandidateId)
                .HasConstraintName("Offers_CandidateId_fkey");

            entity.HasOne(d => d.ContractType).WithMany(p => p.Offers)
                .HasForeignKey(d => d.ContractTypeId)
                .HasConstraintName("Offers_ContractTypeId_fkey");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.OfferCreatedByNavigations)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("Offers_CreatedBy_fkey");

            entity.HasOne(d => d.Department).WithMany(p => p.Offers)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("Offers_DepartmentId_fkey");

            entity.HasOne(d => d.InterviewSchedule).WithMany(p => p.Offers)
                .HasForeignKey(d => d.InterviewScheduleId)
                .HasConstraintName("Offers_InterviewScheduleId_fkey");

            entity.HasOne(d => d.Level).WithMany(p => p.Offers)
                .HasForeignKey(d => d.LevelId)
                .HasConstraintName("Offers_LevelId_fkey");

            entity.HasOne(d => d.OfferStatus).WithMany(p => p.Offers)
                .HasForeignKey(d => d.OfferStatusId)
                .HasConstraintName("Offers_OfferStatusId_fkey");

            entity.HasOne(d => d.Position).WithMany(p => p.Offers)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("Offers_PositionId_fkey");

            entity.HasOne(d => d.RecruiterOwner).WithMany(p => p.OfferRecruiterOwners)
                .HasForeignKey(d => d.RecruiterOwnerId)
                .HasConstraintName("Offers_RecruiterOwnerId_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.OfferUpdatedByNavigations)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("Offers_UpdatedBy_fkey");


            entity.OwnsOne(d => d.DatePeriod, offer =>
            {
                offer.Property(dp => dp.StartDate).HasColumnName(nameof(DatePeriod.StartDate));
                offer.Property(dp => dp.EndDate).HasColumnName(nameof(DatePeriod.EndDate));
            });

        });
        */


        modelBuilder.Entity<OfferStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("OfferStatuses_pkey");
            entity.ToTable("OfferStatuses", "IMS");
            entity.Property(e => e.Name).HasColumnType("character varying");
        });
    }
}
