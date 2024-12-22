using InterviewManagementSystem.Domain.Entities.MasterData;
using InterviewManagementSystem.Domain.Entities.Offers;
using Microsoft.EntityFrameworkCore;

namespace InterviewManagementSystem.Infrastructure.Databases.PostgreSQL.EntityConfigurations;

public static class MasterDataConfiguration
{
    public static void ConfigureMasterData(this ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Benefit>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Benefits_pkey");
            entity.ToTable("Benefits", "IMS");
            entity.Property(e => e.Name).HasColumnType("character varying");
        });


        modelBuilder.Entity<ContractType>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("ContractTypes_pkey");
            entity.ToTable("ContractTypes", "IMS");
            entity.Property(e => e.Name).HasColumnType("character varying");
        });

        modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Departments_pkey");
            entity.ToTable("Departments", "IMS");
            entity.Property(e => e.Name).HasColumnType("character varying");
        });

        modelBuilder.Entity<HighestLevel>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("HighestLevels_pkey");
            entity.ToTable("HighestLevels", "IMS");
            entity.Property(e => e.Name).HasColumnType("character varying");
        });


        modelBuilder.Entity<Level>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Levels_pkey");
            entity.ToTable("Levels", "IMS");
            entity.Property(e => e.Name).HasColumnType("character varying");
        });


        modelBuilder.Entity<Position>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Positions_pkey");
            entity.ToTable("Positions", "IMS");
            entity.Property(e => e.Name).HasColumnType("character varying");
        });

        modelBuilder.Entity<Skill>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Skills_pkey");
            entity.ToTable("Skills", "IMS");
            entity.Property(e => e.Name).HasColumnType("character varying");
        });


        modelBuilder.Entity<CandidateOfferStatus>(entity =>
        {
            entity.HasKey(e => new { e.OfferId, e.CandidateId, e.CandidateStatusId }).HasName("CandidateOfferStatus_pkey");
            entity.ToTable("CandidateOfferStatus", "IMS");
            entity.HasIndex(e => e.OfferId, "CandidateOfferStatus_OfferId_key").IsUnique();
            entity.HasOne(d => d.Candidate).WithMany(p => p.CandidateOfferStatuses)
                .HasForeignKey(d => d.CandidateId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("CandidateOfferStatus_CandidateId_fkey");
        });
    }
}
