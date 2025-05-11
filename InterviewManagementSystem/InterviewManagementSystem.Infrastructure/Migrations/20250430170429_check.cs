using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using NpgsqlTypes;

#nullable disable

namespace InterviewManagementSystem.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class check : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "IMS");

            migrationBuilder.CreateTable(
                name: "AppRoles",
                schema: "IMS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Name = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppRoles", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Benefits",
                schema: "IMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Benefits_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CandidateStatuses",
                schema: "IMS",
                columns: table => new
                {
                    Id = table.Column<short>(type: "smallint", nullable: false),
                    Name = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CandidateStatuses_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ContractTypes",
                schema: "IMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("ContractTypes_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                schema: "IMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Departments_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HighestLevels",
                schema: "IMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("HighestLevels_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InterviewResults",
                schema: "IMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("InterviewResults_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "InterviewScheduleStatuses",
                schema: "IMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("InterviewScheduleStatuses_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "JobStatuses",
                schema: "IMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("JobStatuses_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Levels",
                schema: "IMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Levels_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "OfferStatuses",
                schema: "IMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("OfferStatuses_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Positions",
                schema: "IMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Positions_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Skills",
                schema: "IMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false),
                    Name = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Skills_pkey", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AppCustomRoleClaims",
                schema: "IMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppCustomRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppCustomRoleClaims_AppRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "IMS",
                        principalTable: "AppRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AspNetRoleClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AspNetRoleClaims_AppRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "IMS",
                        principalTable: "AppRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUsers",
                schema: "IMS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Dob = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Gender = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    Address = table.Column<string>(type: "character varying", nullable: true),
                    Note = table.Column<string>(type: "character varying", nullable: true),
                    IsActive = table.Column<bool>(type: "boolean", nullable: false, defaultValue: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false),
                    DepartmentId = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    AvatarLink = table.Column<string>(type: "character varying", nullable: true),
                    SearchVector = table.Column<NpgsqlTsVector>(type: "tsvector", nullable: false, computedColumnSql: "\r\n                                                to_tsvector(\r\n                                                    'english'::regconfig, \r\n                                                    (\r\n                                                        COALESCE(\"Email\", ''::character varying)::text || ' '::text || \r\n                                                        COALESCE(\"UserName\", ''::character varying)::text || ' '::text || \r\n                                                        COALESCE(\"PhoneNumber\", ''::text)\r\n                                                    )\r\n                                                )", stored: true),
                    UserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    Email = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(type: "character varying(256)", maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    PasswordHash = table.Column<string>(type: "text", nullable: true),
                    SecurityStamp = table.Column<string>(type: "text", nullable: true),
                    ConcurrencyStamp = table.Column<string>(type: "text", nullable: true),
                    PhoneNumber = table.Column<string>(type: "text", nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(type: "boolean", nullable: false),
                    TwoFactorEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(type: "timestamp with time zone", nullable: true),
                    LockoutEnabled = table.Column<bool>(type: "boolean", nullable: false),
                    AccessFailedCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUsers", x => x.Id);
                    table.ForeignKey(
                        name: "AppUsers_CreatedBy_fkey",
                        column: x => x.CreatedBy,
                        principalSchema: "IMS",
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "AppUsers_DepartmentId_fkey",
                        column: x => x.DepartmentId,
                        principalSchema: "IMS",
                        principalTable: "Departments",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "AppUsers_UpdatedBy_fkey",
                        column: x => x.UpdatedBy,
                        principalSchema: "IMS",
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserClaims_AppUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "IMS",
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserClaims",
                schema: "IMS",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    ClaimType = table.Column<string>(type: "text", nullable: true),
                    ClaimValue = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppUserClaims_AppUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "IMS",
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AppUserLogins_AppUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "IMS",
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserLogins",
                schema: "IMS",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    ProviderKey = table.Column<string>(type: "text", nullable: false),
                    ProviderDisplayName = table.Column<string>(type: "text", nullable: true),
                    UserId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_AppUserLogins_AppUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "IMS",
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserRoles",
                schema: "IMS",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    RoleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_AppUserRoles_AppRoles_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "IMS",
                        principalTable: "AppRoles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AppUserRoles_AppUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "IMS",
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AppUserTokens_AppUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "IMS",
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AppUserTokens",
                schema: "IMS",
                columns: table => new
                {
                    UserId = table.Column<Guid>(type: "uuid", nullable: false),
                    LoginProvider = table.Column<string>(type: "text", nullable: false),
                    Name = table.Column<string>(type: "text", nullable: false),
                    Value = table.Column<string>(type: "text", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppUserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_AppUserTokens_AppUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "IMS",
                        principalTable: "AppUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Jobs",
                schema: "IMS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying", nullable: true),
                    WorkingAddress = table.Column<string>(type: "character varying", nullable: true),
                    Description = table.Column<string>(type: "character varying", nullable: true),
                    From = table.Column<decimal>(type: "money", nullable: true),
                    To = table.Column<decimal>(type: "money", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    JobStatusId = table.Column<int>(type: "integer", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Jobs_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "Jobs_CreatedBy_fkey",
                        column: x => x.CreatedBy,
                        principalSchema: "IMS",
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Jobs_JobStatusId_fkey",
                        column: x => x.JobStatusId,
                        principalSchema: "IMS",
                        principalTable: "JobStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Jobs_UpdatedBy_fkey",
                        column: x => x.UpdatedBy,
                        principalSchema: "IMS",
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Candidates",
                schema: "IMS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    YearsOfExperience = table.Column<short>(type: "smallint", nullable: true),
                    RecruiterId = table.Column<Guid>(type: "uuid", nullable: true),
                    PositionId = table.Column<int>(type: "integer", nullable: false),
                    HighestLevelId = table.Column<int>(type: "integer", nullable: false),
                    JobId = table.Column<Guid>(type: "uuid", nullable: true),
                    CandidateStatusId = table.Column<short>(type: "smallint", nullable: true),
                    AttachmentLink = table.Column<string>(type: "character varying", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Candidates", x => x.Id);
                    table.ForeignKey(
                        name: "Candidates_CandidateStatusId_fkey",
                        column: x => x.CandidateStatusId,
                        principalSchema: "IMS",
                        principalTable: "CandidateStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Candidates_HighestLevelId_fkey",
                        column: x => x.HighestLevelId,
                        principalSchema: "IMS",
                        principalTable: "HighestLevels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Candidates_Id_fkey",
                        column: x => x.Id,
                        principalSchema: "IMS",
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Candidates_JobId_fkey",
                        column: x => x.JobId,
                        principalSchema: "IMS",
                        principalTable: "Jobs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Candidates_PositionId_fkey",
                        column: x => x.PositionId,
                        principalSchema: "IMS",
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Candidates_RecruiterId_fkey",
                        column: x => x.RecruiterId,
                        principalSchema: "IMS",
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobBenefits",
                schema: "IMS",
                columns: table => new
                {
                    JobId = table.Column<Guid>(type: "uuid", nullable: false),
                    BenefitId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("JobBenefits_pkey", x => new { x.JobId, x.BenefitId });
                    table.ForeignKey(
                        name: "JobBenefits_BenefitId_fkey",
                        column: x => x.BenefitId,
                        principalSchema: "IMS",
                        principalTable: "Benefits",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "JobBenefits_JobId_fkey",
                        column: x => x.JobId,
                        principalSchema: "IMS",
                        principalTable: "Jobs",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobLevels",
                schema: "IMS",
                columns: table => new
                {
                    JobId = table.Column<Guid>(type: "uuid", nullable: false),
                    LevelId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("JobLevels_pkey", x => new { x.JobId, x.LevelId });
                    table.ForeignKey(
                        name: "JobLevels_JobId_fkey",
                        column: x => x.JobId,
                        principalSchema: "IMS",
                        principalTable: "Jobs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "JobLevels_LevelId_fkey",
                        column: x => x.LevelId,
                        principalSchema: "IMS",
                        principalTable: "Levels",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "JobSkills",
                schema: "IMS",
                columns: table => new
                {
                    JobId = table.Column<Guid>(type: "uuid", nullable: false),
                    SkillId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("JobSkills_pkey", x => new { x.JobId, x.SkillId });
                    table.ForeignKey(
                        name: "JobSkills_JobId_fkey",
                        column: x => x.JobId,
                        principalSchema: "IMS",
                        principalTable: "Jobs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "JobSkills_SkillId_fkey",
                        column: x => x.SkillId,
                        principalSchema: "IMS",
                        principalTable: "Skills",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateOfferStatus",
                schema: "IMS",
                columns: table => new
                {
                    CandidateId = table.Column<Guid>(type: "uuid", nullable: false),
                    OfferId = table.Column<Guid>(type: "uuid", nullable: false),
                    CandidateStatusId = table.Column<short>(type: "smallint", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CandidateOfferStatus_pkey", x => new { x.OfferId, x.CandidateId, x.CandidateStatusId });
                    table.UniqueConstraint("AK_CandidateOfferStatus_OfferId", x => x.OfferId);
                    table.ForeignKey(
                        name: "CandidateOfferStatus_CandidateId_fkey",
                        column: x => x.CandidateId,
                        principalSchema: "IMS",
                        principalTable: "Candidates",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CandidateSkills",
                schema: "IMS",
                columns: table => new
                {
                    CandidateId = table.Column<Guid>(type: "uuid", nullable: false),
                    SkillId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("CandidateSkills_pkey", x => new { x.CandidateId, x.SkillId });
                    table.ForeignKey(
                        name: "CandidateSkills_CandidateId_fkey",
                        column: x => x.CandidateId,
                        principalSchema: "IMS",
                        principalTable: "Candidates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "CandidateSkills_SkillId_fkey",
                        column: x => x.SkillId,
                        principalSchema: "IMS",
                        principalTable: "Skills",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Interviewers",
                schema: "IMS",
                columns: table => new
                {
                    AppUserId = table.Column<Guid>(type: "uuid", nullable: false),
                    InterviewScheduleId = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Interviewers_pkey", x => new { x.AppUserId, x.InterviewScheduleId });
                    table.ForeignKey(
                        name: "Interviewers_AppUserId_fkey",
                        column: x => x.AppUserId,
                        principalSchema: "IMS",
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "InterviewSchedules",
                schema: "IMS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Title = table.Column<string>(type: "character varying", nullable: true),
                    ScheduleTime = table.Column<DateTime>(type: "timestamp without time zone", nullable: true),
                    StartHour = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    EndHour = table.Column<TimeOnly>(type: "time without time zone", nullable: true),
                    Location = table.Column<string>(type: "character varying", nullable: true),
                    Note = table.Column<string>(type: "character varying", nullable: true),
                    MeetingURL = table.Column<string>(type: "character varying", nullable: true),
                    RecruiterOwnerId = table.Column<Guid>(type: "uuid", nullable: true),
                    CandidateId = table.Column<Guid>(type: "uuid", nullable: true),
                    JobId = table.Column<Guid>(type: "uuid", nullable: true),
                    InterviewScheduleStatusId = table.Column<int>(type: "integer", nullable: true),
                    InterviewResultId = table.Column<int>(type: "integer", nullable: true),
                    OfferId = table.Column<Guid>(type: "uuid", nullable: true),
                    CandidateStatusId = table.Column<short>(type: "smallint", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("InterviewSchedules_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "InterviewSchedules_CandidateId_fkey",
                        column: x => x.CandidateId,
                        principalSchema: "IMS",
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "InterviewSchedules_CandidateStatusId_fkey",
                        column: x => x.CandidateStatusId,
                        principalSchema: "IMS",
                        principalTable: "CandidateStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "InterviewSchedules_CreatedBy_fkey",
                        column: x => x.CreatedBy,
                        principalSchema: "IMS",
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "InterviewSchedules_InterviewResultId_fkey",
                        column: x => x.InterviewResultId,
                        principalSchema: "IMS",
                        principalTable: "InterviewResults",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "InterviewSchedules_InterviewScheduleStatusId_fkey",
                        column: x => x.InterviewScheduleStatusId,
                        principalSchema: "IMS",
                        principalTable: "InterviewScheduleStatuses",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "InterviewSchedules_JobId_fkey",
                        column: x => x.JobId,
                        principalSchema: "IMS",
                        principalTable: "Jobs",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "InterviewSchedules_RecruiterOwnerId_fkey",
                        column: x => x.RecruiterOwnerId,
                        principalSchema: "IMS",
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "InterviewSchedules_UpdatedBy_fkey",
                        column: x => x.UpdatedBy,
                        principalSchema: "IMS",
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "Offers",
                schema: "IMS",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Note = table.Column<string>(type: "character varying", nullable: true),
                    BasicSalary = table.Column<decimal>(type: "money", nullable: true),
                    DueDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    StartDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    EndDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    OfferStatusId = table.Column<int>(type: "integer", nullable: false),
                    PositionId = table.Column<int>(type: "integer", nullable: false),
                    CandidateId = table.Column<Guid>(type: "uuid", nullable: true),
                    ApproverId = table.Column<Guid>(type: "uuid", nullable: true),
                    RecruiterOwnerId = table.Column<Guid>(type: "uuid", nullable: true),
                    ContractTypeId = table.Column<int>(type: "integer", nullable: false),
                    LevelId = table.Column<int>(type: "integer", nullable: false),
                    DepartmentId = table.Column<int>(type: "integer", nullable: false),
                    InterviewScheduleId = table.Column<Guid>(type: "uuid", nullable: true),
                    CreatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    UpdatedBy = table.Column<Guid>(type: "uuid", nullable: true),
                    CreateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    UpdateAt = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    IsDeleted = table.Column<bool>(type: "boolean", nullable: false, defaultValue: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("Offers_pkey", x => x.Id);
                    table.ForeignKey(
                        name: "Offers_ApproverId_fkey",
                        column: x => x.ApproverId,
                        principalSchema: "IMS",
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Offers_CandidateId_fkey",
                        column: x => x.CandidateId,
                        principalSchema: "IMS",
                        principalTable: "Candidates",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Offers_ContractTypeId_fkey",
                        column: x => x.ContractTypeId,
                        principalSchema: "IMS",
                        principalTable: "ContractTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Offers_CreatedBy_fkey",
                        column: x => x.CreatedBy,
                        principalSchema: "IMS",
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Offers_DepartmentId_fkey",
                        column: x => x.DepartmentId,
                        principalSchema: "IMS",
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Offers_Id_fkey",
                        column: x => x.Id,
                        principalSchema: "IMS",
                        principalTable: "CandidateOfferStatus",
                        principalColumn: "OfferId");
                    table.ForeignKey(
                        name: "Offers_InterviewScheduleId_fkey",
                        column: x => x.InterviewScheduleId,
                        principalSchema: "IMS",
                        principalTable: "InterviewSchedules",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Offers_LevelId_fkey",
                        column: x => x.LevelId,
                        principalSchema: "IMS",
                        principalTable: "Levels",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Offers_OfferStatusId_fkey",
                        column: x => x.OfferStatusId,
                        principalSchema: "IMS",
                        principalTable: "OfferStatuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Offers_PositionId_fkey",
                        column: x => x.PositionId,
                        principalSchema: "IMS",
                        principalTable: "Positions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "Offers_RecruiterOwnerId_fkey",
                        column: x => x.RecruiterOwnerId,
                        principalSchema: "IMS",
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "Offers_UpdatedBy_fkey",
                        column: x => x.UpdatedBy,
                        principalSchema: "IMS",
                        principalTable: "AppUsers",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppRoleClaims_RoleId",
                schema: "IMS",
                table: "AppCustomRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "IMS",
                table: "AppRoles",
                column: "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AppUserClaims_UserId",
                table: "AppUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserClaims_UserId1",
                schema: "IMS",
                table: "AppUserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserLogins_UserId",
                table: "AppUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserLogins_UserId1",
                schema: "IMS",
                table: "AppUserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUserRoles_RoleId",
                schema: "IMS",
                table: "AppUserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "IMS",
                table: "AppUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_CreatedBy",
                schema: "IMS",
                table: "AppUsers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_DepartmentId",
                schema: "IMS",
                table: "AppUsers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_AppUsers_UpdatedBy",
                schema: "IMS",
                table: "AppUsers",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "IMS",
                table: "AppUsers",
                column: "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetRoleClaims_RoleId",
                table: "AspNetRoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "CandidateOfferStatus_OfferId_key",
                schema: "IMS",
                table: "CandidateOfferStatus",
                column: "OfferId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_CandidateOfferStatus_CandidateId",
                schema: "IMS",
                table: "CandidateOfferStatus",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_CandidateStatusId",
                schema: "IMS",
                table: "Candidates",
                column: "CandidateStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_HighestLevelId",
                schema: "IMS",
                table: "Candidates",
                column: "HighestLevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_JobId",
                schema: "IMS",
                table: "Candidates",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_PositionId",
                schema: "IMS",
                table: "Candidates",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Candidates_RecruiterId",
                schema: "IMS",
                table: "Candidates",
                column: "RecruiterId");

            migrationBuilder.CreateIndex(
                name: "IX_CandidateSkills_SkillId",
                schema: "IMS",
                table: "CandidateSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Interviewers_InterviewScheduleId",
                schema: "IMS",
                table: "Interviewers",
                column: "InterviewScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewSchedules_CandidateId",
                schema: "IMS",
                table: "InterviewSchedules",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewSchedules_CandidateStatusId",
                schema: "IMS",
                table: "InterviewSchedules",
                column: "CandidateStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewSchedules_CreatedBy",
                schema: "IMS",
                table: "InterviewSchedules",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewSchedules_InterviewResultId",
                schema: "IMS",
                table: "InterviewSchedules",
                column: "InterviewResultId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewSchedules_InterviewScheduleStatusId",
                schema: "IMS",
                table: "InterviewSchedules",
                column: "InterviewScheduleStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewSchedules_JobId",
                schema: "IMS",
                table: "InterviewSchedules",
                column: "JobId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewSchedules_OfferId",
                schema: "IMS",
                table: "InterviewSchedules",
                column: "OfferId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewSchedules_RecruiterOwnerId",
                schema: "IMS",
                table: "InterviewSchedules",
                column: "RecruiterOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_InterviewSchedules_UpdatedBy",
                schema: "IMS",
                table: "InterviewSchedules",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_JobBenefits_BenefitId",
                schema: "IMS",
                table: "JobBenefits",
                column: "BenefitId");

            migrationBuilder.CreateIndex(
                name: "IX_JobLevels_LevelId",
                schema: "IMS",
                table: "JobLevels",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_CreatedBy",
                schema: "IMS",
                table: "Jobs",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_JobStatusId",
                schema: "IMS",
                table: "Jobs",
                column: "JobStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Jobs_UpdatedBy",
                schema: "IMS",
                table: "Jobs",
                column: "UpdatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_JobSkills_SkillId",
                schema: "IMS",
                table: "JobSkills",
                column: "SkillId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_ApproverId",
                schema: "IMS",
                table: "Offers",
                column: "ApproverId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_CandidateId",
                schema: "IMS",
                table: "Offers",
                column: "CandidateId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_ContractTypeId",
                schema: "IMS",
                table: "Offers",
                column: "ContractTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_CreatedBy",
                schema: "IMS",
                table: "Offers",
                column: "CreatedBy");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_DepartmentId",
                schema: "IMS",
                table: "Offers",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_InterviewScheduleId",
                schema: "IMS",
                table: "Offers",
                column: "InterviewScheduleId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_LevelId",
                schema: "IMS",
                table: "Offers",
                column: "LevelId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_OfferStatusId",
                schema: "IMS",
                table: "Offers",
                column: "OfferStatusId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_PositionId",
                schema: "IMS",
                table: "Offers",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_RecruiterOwnerId",
                schema: "IMS",
                table: "Offers",
                column: "RecruiterOwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Offers_UpdatedBy",
                schema: "IMS",
                table: "Offers",
                column: "UpdatedBy");

            migrationBuilder.AddForeignKey(
                name: "Interviewers_InterviewScheduleId_fkey",
                schema: "IMS",
                table: "Interviewers",
                column: "InterviewScheduleId",
                principalSchema: "IMS",
                principalTable: "InterviewSchedules",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "InterviewSchedules_OfferId_fkey",
                schema: "IMS",
                table: "InterviewSchedules",
                column: "OfferId",
                principalSchema: "IMS",
                principalTable: "Offers",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "Candidates_Id_fkey",
                schema: "IMS",
                table: "Candidates");

            migrationBuilder.DropForeignKey(
                name: "Candidates_RecruiterId_fkey",
                schema: "IMS",
                table: "Candidates");

            migrationBuilder.DropForeignKey(
                name: "InterviewSchedules_CandidateId_fkey",
                schema: "IMS",
                table: "InterviewSchedules");

            migrationBuilder.DropForeignKey(
                name: "InterviewSchedules_CreatedBy_fkey",
                schema: "IMS",
                table: "InterviewSchedules");

            migrationBuilder.DropForeignKey(
                name: "InterviewSchedules_RecruiterOwnerId_fkey",
                schema: "IMS",
                table: "InterviewSchedules");

            migrationBuilder.DropForeignKey(
                name: "InterviewSchedules_UpdatedBy_fkey",
                schema: "IMS",
                table: "InterviewSchedules");

            migrationBuilder.DropForeignKey(
                name: "Jobs_CreatedBy_fkey",
                schema: "IMS",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "Jobs_UpdatedBy_fkey",
                schema: "IMS",
                table: "Jobs");

            migrationBuilder.DropForeignKey(
                name: "Offers_ApproverId_fkey",
                schema: "IMS",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "Offers_CreatedBy_fkey",
                schema: "IMS",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "Offers_RecruiterOwnerId_fkey",
                schema: "IMS",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "Offers_UpdatedBy_fkey",
                schema: "IMS",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "Offers_DepartmentId_fkey",
                schema: "IMS",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "CandidateOfferStatus_CandidateId_fkey",
                schema: "IMS",
                table: "CandidateOfferStatus");

            migrationBuilder.DropForeignKey(
                name: "Offers_CandidateId_fkey",
                schema: "IMS",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "InterviewSchedules_CandidateStatusId_fkey",
                schema: "IMS",
                table: "InterviewSchedules");

            migrationBuilder.DropForeignKey(
                name: "InterviewSchedules_JobId_fkey",
                schema: "IMS",
                table: "InterviewSchedules");

            migrationBuilder.DropForeignKey(
                name: "Offers_PositionId_fkey",
                schema: "IMS",
                table: "Offers");

            migrationBuilder.DropForeignKey(
                name: "Offers_InterviewScheduleId_fkey",
                schema: "IMS",
                table: "Offers");

            migrationBuilder.DropTable(
                name: "AppCustomRoleClaims",
                schema: "IMS");

            migrationBuilder.DropTable(
                name: "AppUserClaims");

            migrationBuilder.DropTable(
                name: "AppUserClaims",
                schema: "IMS");

            migrationBuilder.DropTable(
                name: "AppUserLogins");

            migrationBuilder.DropTable(
                name: "AppUserLogins",
                schema: "IMS");

            migrationBuilder.DropTable(
                name: "AppUserRoles",
                schema: "IMS");

            migrationBuilder.DropTable(
                name: "AppUserTokens");

            migrationBuilder.DropTable(
                name: "AppUserTokens",
                schema: "IMS");

            migrationBuilder.DropTable(
                name: "AspNetRoleClaims");

            migrationBuilder.DropTable(
                name: "CandidateSkills",
                schema: "IMS");

            migrationBuilder.DropTable(
                name: "Interviewers",
                schema: "IMS");

            migrationBuilder.DropTable(
                name: "JobBenefits",
                schema: "IMS");

            migrationBuilder.DropTable(
                name: "JobLevels",
                schema: "IMS");

            migrationBuilder.DropTable(
                name: "JobSkills",
                schema: "IMS");

            migrationBuilder.DropTable(
                name: "AppRoles",
                schema: "IMS");

            migrationBuilder.DropTable(
                name: "Benefits",
                schema: "IMS");

            migrationBuilder.DropTable(
                name: "Skills",
                schema: "IMS");

            migrationBuilder.DropTable(
                name: "AppUsers",
                schema: "IMS");

            migrationBuilder.DropTable(
                name: "Departments",
                schema: "IMS");

            migrationBuilder.DropTable(
                name: "Candidates",
                schema: "IMS");

            migrationBuilder.DropTable(
                name: "HighestLevels",
                schema: "IMS");

            migrationBuilder.DropTable(
                name: "CandidateStatuses",
                schema: "IMS");

            migrationBuilder.DropTable(
                name: "Jobs",
                schema: "IMS");

            migrationBuilder.DropTable(
                name: "JobStatuses",
                schema: "IMS");

            migrationBuilder.DropTable(
                name: "Positions",
                schema: "IMS");

            migrationBuilder.DropTable(
                name: "InterviewSchedules",
                schema: "IMS");

            migrationBuilder.DropTable(
                name: "InterviewResults",
                schema: "IMS");

            migrationBuilder.DropTable(
                name: "InterviewScheduleStatuses",
                schema: "IMS");

            migrationBuilder.DropTable(
                name: "Offers",
                schema: "IMS");

            migrationBuilder.DropTable(
                name: "ContractTypes",
                schema: "IMS");

            migrationBuilder.DropTable(
                name: "CandidateOfferStatus",
                schema: "IMS");

            migrationBuilder.DropTable(
                name: "Levels",
                schema: "IMS");

            migrationBuilder.DropTable(
                name: "OfferStatuses",
                schema: "IMS");
        }
    }
}
