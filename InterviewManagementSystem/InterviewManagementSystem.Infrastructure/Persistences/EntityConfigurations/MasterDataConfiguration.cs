using InterviewManagementSystem.Domain.Entities.MasterData;
using Microsoft.EntityFrameworkCore;

namespace InterviewManagementSystem.Infrastructure.Persistences.EntityConfigurations;

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
    }
}
