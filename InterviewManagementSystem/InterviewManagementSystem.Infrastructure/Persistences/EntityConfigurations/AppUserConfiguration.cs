using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.Entities.Interviews;
using InterviewManagementSystem.Domain.Entities.MasterData;
using Microsoft.EntityFrameworkCore;

namespace InterviewManagementSystem.Infrastructure.Persistences.EntityConfigurations;

internal static class AppUserConfiguration
{
    internal static void ConfigureAppUser(this ModelBuilder modelBuilder)
    {
        /*
        modelBuilder.Entity<AppUserLogin>().HasKey(a => new { a.ProviderKey, a.LoginProvider });
        modelBuilder.Entity<AppUserToken>().HasKey(e => new { e.UserId, e.LoginProvider, e.Name });


        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.ToTable("AppUsers");
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address).HasColumnType("character varying");
            entity.Property(e => e.Dob).HasColumnName("Dob");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Gender).HasDefaultValue(true);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.Note).HasColumnType("character varying");
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity
            .HasOne(d => d.Department)
            .WithMany(p => p.AppUsers)
            .HasForeignKey(d => d.DepartmentId)
            .HasConstraintName("AppUsers_DepartmentId_fkey");

            entity
            .HasOne(a => a.CandidateIdNavigation)
            .WithOne(c => c.IdNavigation)
            .HasForeignKey<Candidate>(c => c.Id);
        });

        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.ToTable("Candidates");
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.HighestLevel).WithMany(p => p.Candidates)
                .HasForeignKey(d => d.HighestLevelId)
                .HasConstraintName("Candidates_HighestLevelId_fkey");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.CandidateIdNavigation)
                .HasForeignKey<Candidate>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Candidates_Id_fkey");

            entity.HasOne(d => d.Position).WithMany(p => p.Candidates)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("Candidates_PositionId_fkey");
        });



        modelBuilder.Entity<AppRole>(entity =>
        {
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
            entity.ToTable("AspNetCustomRoles");
        });

        modelBuilder.Entity<AppRoleClaim>(entity =>
        {
            entity.HasIndex(e => e.RoleId, "IX_AppRoleClaims_RoleId");
            entity.HasOne(d => d.Role).WithMany(p => p.AppRoleClaims).HasForeignKey(d => d.RoleId);
            entity.ToTable("AppCustomRoleClaims");
        });


        modelBuilder.Entity<AppUserClaim>(entity =>
        {
            entity.HasIndex(e => e.UserId, "IX_AppUserClaims_UserId");
            entity.HasOne(d => d.User).WithMany(p => p.AppUserClaims).HasForeignKey(d => d.UserId);
            entity.ToTable("AppCustomUserClaims");
        });

        modelBuilder.Entity<AppUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            entity.HasIndex(e => e.UserId, "IX_AppUserLogins_UserId");
            entity.HasOne(d => d.User).WithMany(p => p.AppUserLogins).HasForeignKey(d => d.UserId);
            entity.ToTable("AppCustomUserLogins");
        });

        modelBuilder.Entity<AppUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            entity.HasOne(d => d.User).WithMany(p => p.AppUserTokens).HasForeignKey(d => d.UserId);
            entity.ToTable("AppCustomUserTokens");
        });

        */



        //////////////////////////////////////////////////////


        /*
        modelBuilder.Entity<AppRole>(entity =>
        {
            entity.ToTable("AspNetCustomRoles", "IMS");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AppRoleClaim>(entity =>
        {
            entity.ToTable("AppCustomRoleClaims", "IMS");
            entity.HasIndex(e => e.RoleId, "IX_AppRoleClaims_RoleId");
            entity.HasOne(d => d.Role).WithMany(p => p.AppRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.ToTable("AppUsers", "IMS");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address).HasColumnType("character varying");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Gender).HasDefaultValue(true);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.Note).HasColumnType("character varying");
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasOne(d => d.Department).WithMany(p => p.AppUsers)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("AppUsers_DepartmentId_fkey");

            entity
            .HasOne(a => a.CandidateIdNavigation)
            .WithOne(c => c.IdNavigation)
            .HasForeignKey<Candidate>(c => c.Id);



            entity.HasMany(d => d.InterviewSchedulesNavigation).WithMany(p => p.AppUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "Interviewer",
                    r => r.HasOne<InterviewSchedule>().WithMany()
                        .HasForeignKey("InterviewScheduleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Interviewers_InterviewScheduleId_fkey"),
                    l => l.HasOne<AppUser>().WithMany()
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Interviewers_AppUserId_fkey"),
                    j =>
                    {
                        j.HasKey("AppUserId", "InterviewScheduleId").HasName("Interviewers_pkey");
                        j.ToTable("Interviewers", "IMS");
                    });


            entity.HasMany(a => a.InterviewSchedules)
                .WithOne(i => i.RecruiterOwner)
                .HasForeignKey(i => i.RecruiterOwnerId);


            
            entity.HasMany(d => d.Roles).WithMany(p => p.Users)
                .UsingEntity<Dictionary<string, object>>(
                    "AppUserRole",
                    r => r.HasOne<AppRole>().WithMany().HasForeignKey("RoleId"),
                    l => l.HasOne<AppUser>().WithMany().HasForeignKey("UserId"),
                    j =>
                    {
                        j.HasKey("UserId", "RoleId");
                        j.ToTable("AppUserRoles", "IMS");
                        j.HasIndex(new[] { "RoleId" }, "IX_AppUserRoles_RoleId");
                    });

            
        });
        */


        /*
        modelBuilder.Entity<AppUserClaim>(entity =>
        {
            entity.ToTable("AppCustomUserClaims", "IMS");

            entity.HasIndex(e => e.UserId, "IX_AppUserClaims_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AppUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AppUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

            entity.ToTable("AppCustomUserLogins", "IMS");

            entity.HasIndex(e => e.UserId, "IX_AppUserLogins_UserId");

            entity.HasOne(d => d.User).WithMany(p => p.AppUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AppUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.ToTable("AppCustomUserTokens", "IMS");

            entity.HasOne(d => d.User).WithMany(p => p.AppUserTokens).HasForeignKey(d => d.UserId);
        });
        */




        /*
          modelBuilder.Entity<Candidate>(entity =>
        {
            entity.ToTable("Candidates");
            entity.Property(e => e.Id).ValueGeneratedNever();

            entity.HasOne(d => d.HighestLevel).WithMany(p => p.Candidates)
                .HasForeignKey(d => d.HighestLevelId)
                .HasConstraintName("Candidates_HighestLevelId_fkey");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.CandidateIdNavigation)
                .HasForeignKey<Candidate>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Candidates_Id_fkey");

            entity.HasOne(d => d.Position).WithMany(p => p.Candidates)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("Candidates_PositionId_fkey");
        });
         */



        /*
        modelBuilder.Entity<Candidate>(entity =>
        {

            entity.ToTable("Candidates", "IMS");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Attachment).HasColumnType("oid");

            entity.HasOne(d => d.CandidateStatus).WithMany(p => p.Candidates)
                .HasForeignKey(d => d.CandidateStatusId)
                .HasConstraintName("Candidates_CandidateStatusId_fkey");

            entity.HasOne(d => d.HighestLevel).WithMany(p => p.Candidates)
                .HasForeignKey(d => d.HighestLevelId)
                .HasConstraintName("Candidates_HighestLevelId_fkey");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.CandidateIdNavigation)
                .HasForeignKey<Candidate>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Candidates_Id_fkey");

            entity.HasOne(d => d.Position).WithMany(p => p.Candidates)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("Candidates_PositionId_fkey");

            entity.HasOne(d => d.Recruiter).WithMany(p => p.CandidateRecruiters)
                .HasForeignKey(d => d.RecruiterId)
                .HasConstraintName("Candidates_RecruiterId_fkey");

            entity.HasMany(d => d.Skills).WithMany(p => p.Candidates)
                .UsingEntity<Dictionary<string, object>>(
                    "CandidateSkill",
                    r => r.HasOne<Skill>().WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("CandidateSkills_SkillId_fkey"),
                    l => l.HasOne<Candidate>().WithMany()
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("CandidateSkills_CandidateId_fkey"),
                    j =>
                    {
                        j.HasKey("CandidateId", "SkillId").HasName("CandidateSkills_pkey");
                        j.ToTable("CandidateSkills", "IMS");
                    });


            //entity.HasMany(d => d.InterviewSchedules).WithMany(i => i.AppUsers);
        });

        modelBuilder.Entity<CandidateStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CandidateStatuses_pkey");
            entity.ToTable("CandidateStatuses", "IMS");
            entity.Property(e => e.Name).HasColumnType("character varying");
        });


        */



        ///////////////////////////////////////////////////////////////////////////////////////////


        modelBuilder.Entity<AppRole>(entity =>
        {
            entity.ToTable("AppCustomRoles", "IMS");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });

        modelBuilder.Entity<AppRoleClaim>(entity =>
        {
            entity.ToTable("AppCustomRoleClaims", "IMS");

            entity.HasIndex(e => e.RoleId, "IX_AppRoleClaims_RoleId");

            entity.HasOne(d => d.Role).WithMany(p => p.AppRoleClaims).HasForeignKey(d => d.RoleId);
        });

        modelBuilder.Entity<AppUser>(entity =>
        {
            entity.ToTable("AppUsers", "IMS");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Address).HasColumnType("character varying");
            entity.Property(e => e.Email).HasMaxLength(256);
            entity.Property(e => e.Gender).HasDefaultValue(true);
            entity.Property(e => e.IsActive).HasDefaultValue(true);
            entity.Property(e => e.IsDeleted).HasDefaultValue(false);
            entity.Property(e => e.NormalizedEmail).HasMaxLength(256);
            entity.Property(e => e.NormalizedUserName).HasMaxLength(256);
            entity.Property(e => e.Note).HasColumnType("character varying");
            entity.Property(e => e.UserName).HasMaxLength(256);

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InverseCreatedByNavigation)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("AppUsers_CreatedBy_fkey");

            entity.HasOne(d => d.Department).WithMany(p => p.AppUsers)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("AppUsers_DepartmentId_fkey");

            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.InverseUpdatedByNavigation)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("AppUsers_UpdatedBy_fkey");

            entity.HasMany(d => d.InterviewSchedules).WithMany(p => p.AppUsers)
                .UsingEntity<Dictionary<string, object>>(
                    "Interviewer",
                    r => r.HasOne<InterviewSchedule>().WithMany()
                        .HasForeignKey("InterviewScheduleId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Interviewers_InterviewScheduleId_fkey"),
                    l => l.HasOne<AppUser>().WithMany()
                        .HasForeignKey("AppUserId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Interviewers_AppUserId_fkey"),
                    j =>
                    {
                        j.HasKey("AppUserId", "InterviewScheduleId").HasName("Interviewers_pkey");
                        j.ToTable("Interviewers", "IMS");
                    });
        });

        modelBuilder.Entity<AppUserClaim>(entity =>
        {
            entity.ToTable("AppCustomUserClaims", "IMS");
            entity.HasIndex(e => e.UserId, "IX_AppUserClaims_UserId");
            entity.HasOne(d => d.User).WithMany(p => p.AppUserClaims).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AppUserLogin>(entity =>
        {

            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            entity.ToTable("AppCustomUserLogins", "IMS");
            entity.HasIndex(e => e.UserId, "IX_AppUserLogins_UserId");
            entity.HasOne(d => d.User).WithMany(p => p.AppUserLogins).HasForeignKey(d => d.UserId);
        });

        modelBuilder.Entity<AppUserToken>(entity =>
        {
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });

            entity.ToTable("AppCustomUserTokens", "IMS");

            entity.HasOne(d => d.User).WithMany(p => p.AppUserTokens).HasForeignKey(d => d.UserId);
        });




        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.ToTable("Candidates", "IMS");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Attachment).HasColumnType("oid");

            entity.HasOne(d => d.CandidateStatus).WithMany(p => p.Candidates)
                .HasForeignKey(d => d.CandidateStatusId)
                .HasConstraintName("Candidates_CandidateStatusId_fkey");

            entity.HasOne(d => d.HighestLevel).WithMany(p => p.Candidates)
                .HasForeignKey(d => d.HighestLevelId)
                .HasConstraintName("Candidates_HighestLevelId_fkey");

            entity.HasOne(d => d.IdNavigation).WithOne(p => p.CandidateIdNavigation)
                .HasForeignKey<Candidate>(d => d.Id)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Candidates_Id_fkey");

            entity.HasOne(d => d.Position).WithMany(p => p.Candidates)
                .HasForeignKey(d => d.PositionId)
                .HasConstraintName("Candidates_PositionId_fkey");

            entity.HasOne(d => d.Recruiter).WithMany(p => p.CandidateRecruiters)
                .HasForeignKey(d => d.RecruiterId)
                .HasConstraintName("Candidates_RecruiterId_fkey");

            entity.HasMany(d => d.Skills).WithMany(p => p.Candidates)
                .UsingEntity<Dictionary<string, object>>(
                    "CandidateSkill",
                    r => r.HasOne<Skill>().WithMany()
                        .HasForeignKey("SkillId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("CandidateSkills_SkillId_fkey"),
                    l => l.HasOne<Candidate>().WithMany()
                        .HasForeignKey("CandidateId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("CandidateSkills_CandidateId_fkey"),
                    j =>
                    {
                        j.HasKey("CandidateId", "SkillId").HasName("CandidateSkills_pkey");
                        j.ToTable("CandidateSkills", "IMS");
                    });
        });

        modelBuilder.Entity<CandidateStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("CandidateStatuses_pkey");

            entity.ToTable("CandidateStatuses", "IMS");

            entity.Property(e => e.Name).HasColumnType("character varying");
        });
    }
}

