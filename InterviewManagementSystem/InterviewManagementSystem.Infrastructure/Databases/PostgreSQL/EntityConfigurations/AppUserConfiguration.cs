using InterviewManagementSystem.Domain.Entities.AppUsers;
using InterviewManagementSystem.Domain.Entities.Interviews;
using InterviewManagementSystem.Domain.Entities.MasterData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace InterviewManagementSystem.Infrastructure.Databases.PostgreSQL.EntityConfigurations;

internal static class AppUserConfiguration
{
    internal static void ConfigureAppUser(this ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<AppRole>(entity =>
        {
            entity.ToTable("AppRoles", "IMS");
            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.Name).HasMaxLength(256);
            entity.Property(e => e.NormalizedName).HasMaxLength(256);
        });



        modelBuilder.Entity<AppRoleClaim>(entity =>
        {
            //entity.ToTable("AppCustomRoleClaims", "IMS");
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
            entity.Property(e => e.AvatarLink).HasColumnType("character varying");

            entity.HasOne(d => d.CreatedByNavigation).WithMany(p => p.InverseCreatedByNavigation)
                .HasForeignKey(d => d.CreatedBy)
                .HasConstraintName("AppUsers_CreatedBy_fkey");


            entity.HasOne(d => d.Department).WithMany(p => p.AppUsers)
                .HasForeignKey(d => d.DepartmentId)
                .HasConstraintName("AppUsers_DepartmentId_fkey");


            entity.HasOne(d => d.UpdatedByNavigation).WithMany(p => p.InverseUpdatedByNavigation)
                .HasForeignKey(d => d.UpdatedBy)
                .HasConstraintName("AppUsers_UpdatedBy_fkey");


            entity.HasMany(d => d.InterviewSchedules).WithMany(p => p.Interviewers)
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

            entity.HasMany(u => u.Roles)
                .WithMany(r => r.Users)
                .UsingEntity<IdentityUserRole<Guid>>(
                    join => join.HasOne<AppRole>().WithMany().HasForeignKey(nameof(IdentityUserRole<Guid>.RoleId)),
                    join => join.HasOne<AppUser>().WithMany().HasForeignKey(nameof(IdentityUserRole<Guid>.UserId)),
                    join => join.ToTable("AppUserRoles")
                );

            string schema = "to_tsvector('english'::regconfig, (((((COALESCE(\"Email\", ''::character varying))::text || ' '::text) || (COALESCE(\"UserName\", ''::character varying))::text) || ' '::text) || COALESCE(\"PhoneNumber\", ''::text)))";


            const string computedColumnSql = @"
                                                to_tsvector(
                                                    'english'::regconfig, 
                                                    (
                                                        COALESCE(""Email"", ''::character varying)::text || ' '::text || 
                                                        COALESCE(""UserName"", ''::character varying)::text || ' '::text || 
                                                        COALESCE(""PhoneNumber"", ''::text)
                                                    )
                                                )";

            entity.Property(e => e.SearchVector).HasComputedColumnSql(computedColumnSql, true);
        });


        modelBuilder.Entity<AppUserClaim>(entity =>
        {
            //entity.ToTable("AppCustomUserClaim", "IMS");
            entity.HasIndex(e => e.UserId, "IX_AppUserClaims_UserId");
            entity.HasOne(d => d.User).WithMany(p => p.AppUserClaims).HasForeignKey(d => d.UserId);
        });


        modelBuilder.Entity<AppUserLogin>(entity =>
        {
            entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });
            //entity.ToTable("AppCustomUserLogins", "IMS");
            entity.HasIndex(e => e.UserId, "IX_AppUserLogins_UserId");
            entity.HasOne(d => d.User).WithMany(p => p.AppUserLogins).HasForeignKey(d => d.UserId);
        });


        modelBuilder.Entity<AppUserToken>(entity =>
        {
            //entity.ToTable("AppCustomUserTokens", "IMS");
            entity.HasKey(e => new { e.UserId, e.LoginProvider, e.Name });
            entity.HasOne(d => d.User).WithMany(p => p.AppUserTokens).HasForeignKey(d => d.UserId);
        });


        modelBuilder.Entity<Candidate>(entity =>
        {
            entity.ToTable("Candidates", "IMS");

            entity.Property(e => e.Id).ValueGeneratedNever();
            entity.Property(e => e.AttachmentLink).HasColumnType("character varying");


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


            entity.HasOne(d => d.Job).WithMany(p => p.Candidates)
                .HasForeignKey(d => d.JobId)
                .HasConstraintName("Candidates_JobId_fkey");


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
            entity.ToTable("CandidateStatuses", "IMS");
            entity.HasKey(e => e.Id).HasName("CandidateStatuses_pkey");
            entity.Property(e => e.Name).HasColumnType("character varying");
        });
    }
}

