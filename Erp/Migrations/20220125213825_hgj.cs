using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Erp.Migrations
{
    public partial class hgj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.EnsureSchema(
                name: "Tbo");

            migrationBuilder.CreateTable(
                name: "AllowancePolicy",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AllowancePolicy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AspNetUsers",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(nullable: true),
                    LastName = table.Column<string>(nullable: true),
                    UsernameChangeLimit = table.Column<int>(nullable: false),
                    ProfilePicture = table.Column<byte[]>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: false),
                    RoleId = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUsers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Asset_type",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Asset_Type = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset_type", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceExcuses",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ExcuseName = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceExcuses", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuctionMember",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuctionNumber = table.Column<string>(nullable: false),
                    TinNumber = table.Column<string>(nullable: false),
                    CompanyName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    Specification = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    Status = table.Column<bool>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Feedback = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionMember", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "AuctionOwner",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AuctionNumber = table.Column<string>(nullable: false),
                    AssetName = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Quantity = table.Column<int>(nullable: false),
                    Specification = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AuctionOwner", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "BonusPolicy",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Amount = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BonusPolicy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "CalendarEvent",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Start = table.Column<DateTime>(nullable: false),
                    End = table.Column<DateTime>(nullable: false),
                    Text = table.Column<string>(nullable: true),
                    Color = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CalendarEvent", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DeductionPolicy",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    Amount = table.Column<decimal>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DeductionPolicy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Disciplinary",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    DisciplinaryTypeId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Approved = table.Column<bool>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    Detention = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Disciplinary", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "DisciplinaryType",
                schema: "Tbo",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(nullable: true),
                    Describtion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DisciplinaryType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Division",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Summary = table.Column<string>(nullable: false),
                    OfficeNumber = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Division", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EmergencyContact",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    Relation = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmergencyContact", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Employment_Types",
                schema: "Tbo",
                columns: table => new
                {
                    Employment_TypeId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(250)", nullable: false),
                    Descrbtion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employment_Types", x => x.Employment_TypeId);
                });

            migrationBuilder.CreateTable(
                name: "FaultyAssetType",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FaultyType = table.Column<string>(nullable: true),
                    Reason = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaultyAssetType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LateCome",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MorningLate = table.Column<TimeSpan>(nullable: true),
                    AfternoonLate = table.Column<TimeSpan>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LateCome", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveType",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    DefaultDays = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LeaveTypeVM",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    DefaultDays = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveTypeVM", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LoanPolicy",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Type = table.Column<string>(nullable: true),
                    MaxAmount = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanPolicy", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Parent",
                schema: "Tbo",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ParentName = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Parent", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "PlanType",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PlanType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Position",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Summary = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Position", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProjectType",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Decription = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProjectType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "QualificationType",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_QualificationType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "RiskType",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RiskType", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Role",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Role", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Route",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartPoint = table.Column<string>(nullable: true),
                    Destination = table.Column<string>(nullable: true),
                    Distance = table.Column<decimal>(nullable: false),
                    Expenses = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Route", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Socials",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Url = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Socials", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(nullable: true),
                    Mobile = table.Column<int>(nullable: false),
                    Email = table.Column<string>(nullable: true),
                    Address = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tax",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    MinAmount = table.Column<decimal>(nullable: false),
                    MaxAmount = table.Column<decimal>(nullable: false),
                    TaxPercent = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tax", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Units",
                schema: "Tbo",
                columns: table => new
                {
                    UnitId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DepartmentId = table.Column<int>(nullable: false),
                    PostionId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Units", x => x.UnitId);
                });

            migrationBuilder.CreateTable(
                name: "User",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    UserName = table.Column<string>(nullable: true),
                    NormalizedUserName = table.Column<string>(nullable: true),
                    Email = table.Column<string>(nullable: true),
                    NormalizedEmail = table.Column<string>(nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "vechiclesType",
                schema: "Tbo",
                columns: table => new
                {
                    id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<string>(nullable: true),
                    description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vechiclesType", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Driver",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverId = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    ExperienceInYears = table.Column<int>(nullable: false),
                    Sex = table.Column<string>(nullable: true),
                    Nationality = table.Column<string>(nullable: true),
                    Date_of_birth = table.Column<DateTime>(nullable: false),
                    Mobile = table.Column<string>(maxLength: 13, nullable: false),
                    Region = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    Subcity = table.Column<string>(nullable: true),
                    Woreda = table.Column<string>(nullable: false),
                    Status = table.Column<bool>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: true),
                    LicenseNumber = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    LicensePictureName = table.Column<string>(nullable: true),
                    ProfilePictureName = table.Column<string>(nullable: true),
                    LicensePicture = table.Column<byte[]>(nullable: true),
                    ProfilePicture = table.Column<byte[]>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Driver", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Driver_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Tbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UserClaims",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UserClaims_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Tbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserLogins",
                schema: "Tbo",
                columns: table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserLogins", x => new { x.LoginProvider, x.ProviderKey });
                    table.ForeignKey(
                        name: "FK_UserLogins_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Tbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserTokens",
                schema: "Tbo",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserTokens", x => new { x.UserId, x.LoginProvider, x.Name });
                    table.ForeignKey(
                        name: "FK_UserTokens_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Tbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Summary = table.Column<string>(nullable: true),
                    DivisionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Departments_Division_DivisionId",
                        column: x => x.DivisionId,
                        principalSchema: "Tbo",
                        principalTable: "Division",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "hierarchical",
                schema: "Tbo",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Pid = table.Column<int>(nullable: true),
                    Parentid = table.Column<int>(nullable: true),
                    hierarchicalID = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_hierarchical", x => x.ID);
                    table.ForeignKey(
                        name: "FK_hierarchical_Parent_Parentid",
                        column: x => x.Parentid,
                        principalSchema: "Tbo",
                        principalTable: "Parent",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_hierarchical_hierarchical_hierarchicalID",
                        column: x => x.hierarchicalID,
                        principalSchema: "Tbo",
                        principalTable: "hierarchical",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Risk",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    Priority = table.Column<decimal>(nullable: false),
                    AnnualOccurrence = table.Column<decimal>(nullable: false),
                    Likelyhood = table.Column<decimal>(nullable: false),
                    Created_at = table.Column<DateTime>(nullable: false),
                    Imapact = table.Column<string>(nullable: true),
                    Status = table.Column<string>(nullable: true),
                    RiskTypeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Risk", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Risk_RiskType_RiskTypeId",
                        column: x => x.RiskTypeId,
                        principalSchema: "Tbo",
                        principalTable: "RiskType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "RoleClaims",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RoleId = table.Column<string>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RoleClaims", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RoleClaims_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Tbo",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "UserRoles",
                schema: "Tbo",
                columns: table => new
                {
                    UserId = table.Column<string>(nullable: false),
                    RoleId = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserRoles", x => new { x.UserId, x.RoleId });
                    table.ForeignKey(
                        name: "FK_UserRoles_Role_RoleId",
                        column: x => x.RoleId,
                        principalSchema: "Tbo",
                        principalTable: "Role",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_UserRoles_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Tbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Asset",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Asset_Name = table.Column<string>(nullable: false),
                    factory_number = table.Column<string>(nullable: false),
                    serial_number = table.Column<string>(nullable: true),
                    Price = table.Column<decimal>(nullable: false),
                    SupplierId = table.Column<int>(nullable: false),
                    Asset_typeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Asset_Asset_type_Asset_typeId",
                        column: x => x.Asset_typeId,
                        principalSchema: "Tbo",
                        principalTable: "Asset_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Asset_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalSchema: "Tbo",
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Payment",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    Price = table.Column<decimal>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    SupplierId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Payment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Payment_Supplier_SupplierId",
                        column: x => x.SupplierId,
                        principalSchema: "Tbo",
                        principalTable: "Supplier",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "vechicles",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Model = table.Column<string>(nullable: true),
                    vechiclesTypeId = table.Column<int>(nullable: false),
                    PlateNumber = table.Column<string>(nullable: true),
                    IsInsured = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_vechicles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_vechicles_vechiclesType_vechiclesTypeId",
                        column: x => x.vechiclesTypeId,
                        principalSchema: "Tbo",
                        principalTable: "vechiclesType",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Team",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Team", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Team_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "Tbo",
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AnnualLossExpectancy",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RiskId = table.Column<int>(nullable: false),
                    ExposureFactor = table.Column<decimal>(nullable: false),
                    SingleLossExpectancy = table.Column<decimal>(nullable: false),
                    Value = table.Column<decimal>(nullable: false),
                    AnnualLossExpectancys = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AnnualLossExpectancy", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AnnualLossExpectancy_Risk_RiskId",
                        column: x => x.RiskId,
                        principalSchema: "Tbo",
                        principalTable: "Risk",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetTypeRisk",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Asset = table.Column<string>(nullable: true),
                    Asset_typeId = table.Column<int>(nullable: false),
                    FailureProbability = table.Column<decimal>(nullable: false),
                    Loss = table.Column<decimal>(nullable: false),
                    RiskId = table.Column<int>(nullable: true),
                    TotalAssetTypeRisk = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetTypeRisk", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetTypeRisk_Asset_type_Asset_typeId",
                        column: x => x.Asset_typeId,
                        principalSchema: "Tbo",
                        principalTable: "Asset_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetTypeRisk_Risk_RiskId",
                        column: x => x.RiskId,
                        principalSchema: "Tbo",
                        principalTable: "Risk",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "AssetDesposal",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetId = table.Column<int>(nullable: false),
                    Reason = table.Column<string>(nullable: true),
                    TransferDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetDesposal", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetDesposal_Asset_AssetId",
                        column: x => x.AssetId,
                        principalSchema: "Tbo",
                        principalTable: "Asset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetLoan",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CompanyName = table.Column<string>(nullable: true),
                    AssetId = table.Column<int>(nullable: false),
                    Reason = table.Column<string>(nullable: true),
                    TransferDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetLoan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetLoan_Asset_AssetId",
                        column: x => x.AssetId,
                        principalSchema: "Tbo",
                        principalTable: "Asset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetRisk",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    AssetId = table.Column<int>(nullable: false),
                    Loss = table.Column<decimal>(nullable: false),
                    FailureProbability = table.Column<decimal>(nullable: false),
                    RiskId = table.Column<int>(nullable: false),
                    AssetRisks = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetRisk", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetRisk_Asset_AssetId",
                        column: x => x.AssetId,
                        principalSchema: "Tbo",
                        principalTable: "Asset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetRisk_Risk_RiskId",
                        column: x => x.RiskId,
                        principalSchema: "Tbo",
                        principalTable: "Risk",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Depersation",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Salvage_Value = table.Column<int>(nullable: false),
                    Useful_Life_Time = table.Column<int>(nullable: false),
                    Annual_Depersation_Amount = table.Column<int>(nullable: false),
                    AssetId = table.Column<int>(nullable: false),
                    PaymentId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Depersation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Depersation_Asset_AssetId",
                        column: x => x.AssetId,
                        principalSchema: "Tbo",
                        principalTable: "Asset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Donaition",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DonaitedTo = table.Column<string>(nullable: true),
                    AssetId = table.Column<int>(nullable: false),
                    Reason = table.Column<string>(nullable: true),
                    TransferDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donaition", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Donaition_Asset_AssetId",
                        column: x => x.AssetId,
                        principalSchema: "Tbo",
                        principalTable: "Asset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "FaultyAsset",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reason = table.Column<string>(nullable: true),
                    AssetId = table.Column<int>(nullable: false),
                    FaultyAssetTypeId = table.Column<int>(nullable: false),
                    status = table.Column<bool>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FaultyAsset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FaultyAsset_Asset_AssetId",
                        column: x => x.AssetId,
                        principalSchema: "Tbo",
                        principalTable: "Asset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FaultyAsset_FaultyAssetType_FaultyAssetTypeId",
                        column: x => x.FaultyAssetTypeId,
                        principalSchema: "Tbo",
                        principalTable: "FaultyAssetType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FaultyAsset_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Tbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DMaintainance",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vechiclesId = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: true),
                    Feedback = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    DriverId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DMaintainance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DMaintainance_Driver_DriverId",
                        column: x => x.DriverId,
                        principalSchema: "Tbo",
                        principalTable: "Driver",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DMaintainance_vechicles_vechiclesId",
                        column: x => x.vechiclesId,
                        principalSchema: "Tbo",
                        principalTable: "vechicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "DriverAllocation",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverId = table.Column<int>(nullable: false),
                    VehicleId = table.Column<int>(nullable: false),
                    RouteId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DriverAllocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DriverAllocation_Driver_DriverId",
                        column: x => x.DriverId,
                        principalSchema: "Tbo",
                        principalTable: "Driver",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DriverAllocation_Route_RouteId",
                        column: x => x.RouteId,
                        principalSchema: "Tbo",
                        principalTable: "Route",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_DriverAllocation_vechicles_VehicleId",
                        column: x => x.VehicleId,
                        principalSchema: "Tbo",
                        principalTable: "vechicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeCode = table.Column<string>(nullable: true),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Sex = table.Column<string>(nullable: false),
                    Nationality = table.Column<string>(nullable: false),
                    DateOfBirth = table.Column<DateTime>(nullable: false),
                    Mobile = table.Column<string>(maxLength: 13, nullable: false),
                    HomeTelephone = table.Column<string>(maxLength: 13, nullable: true),
                    WorkTelephone = table.Column<string>(maxLength: 13, nullable: true),
                    Fax = table.Column<int>(nullable: true),
                    POBox = table.Column<int>(nullable: true),
                    Country = table.Column<string>(nullable: false),
                    Region = table.Column<string>(nullable: false),
                    City = table.Column<string>(nullable: false),
                    Subcity = table.Column<string>(nullable: true),
                    Woreda = table.Column<string>(nullable: false),
                    Status = table.Column<bool>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    AboutMe = table.Column<string>(nullable: true),
                    AreaOfExpertise = table.Column<string>(nullable: true),
                    AccountNumber = table.Column<int>(nullable: false),
                    Approve = table.Column<bool>(nullable: true),
                    Isin = table.Column<bool>(nullable: false),
                    DivisionId = table.Column<int>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false),
                    Employment_typeId = table.Column<int>(nullable: false),
                    PositionId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Field = table.Column<string>(nullable: true),
                    Institution = table.Column<string>(nullable: true),
                    QstartYear = table.Column<DateTime>(nullable: false),
                    QendYear = table.Column<DateTime>(nullable: false),
                    QualificationType = table.Column<string>(nullable: true),
                    sName = table.Column<string>(nullable: true),
                    sUrl = table.Column<string>(nullable: true),
                    EName = table.Column<string>(nullable: true),
                    EAddress = table.Column<string>(nullable: true),
                    EPhoneNumber = table.Column<string>(nullable: true),
                    ERelation = table.Column<string>(nullable: true),
                    SocialsId = table.Column<int>(nullable: true),
                    EmergencyContactId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Employees_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "Tbo",
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Division_DivisionId",
                        column: x => x.DivisionId,
                        principalSchema: "Tbo",
                        principalTable: "Division",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employees_EmergencyContact_EmergencyContactId",
                        column: x => x.EmergencyContactId,
                        principalSchema: "Tbo",
                        principalTable: "EmergencyContact",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Employment_Types_Employment_typeId",
                        column: x => x.Employment_typeId,
                        principalSchema: "Tbo",
                        principalTable: "Employment_Types",
                        principalColumn: "Employment_TypeId",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Position_PositionId",
                        column: x => x.PositionId,
                        principalSchema: "Tbo",
                        principalTable: "Position",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Employees_Socials_SocialsId",
                        column: x => x.SocialsId,
                        principalSchema: "Tbo",
                        principalTable: "Socials",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Employees_Team_TeamId",
                        column: x => x.TeamId,
                        principalSchema: "Tbo",
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_Employees_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Tbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Asset_Allocation",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    AssetId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset_Allocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Asset_Allocation_Asset_AssetId",
                        column: x => x.AssetId,
                        principalSchema: "Tbo",
                        principalTable: "Asset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Asset_Allocation_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Asset_Allocation_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Tbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Asset_Exchange",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    from = table.Column<int>(nullable: false),
                    status = table.Column<bool>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: false),
                    AssetId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Asset_Exchange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Asset_Exchange_Asset_AssetId",
                        column: x => x.AssetId,
                        principalSchema: "Tbo",
                        principalTable: "Asset",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Asset_Exchange_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AssetRequest",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Asset = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    RequestedDate = table.Column<DateTime>(nullable: false),
                    Approved = table.Column<bool>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    AssetRequestId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AssetRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AssetRequest_AssetRequest_AssetRequestId",
                        column: x => x.AssetRequestId,
                        principalSchema: "Tbo",
                        principalTable: "AssetRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AssetRequest_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AssetRequest_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Tbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "BasicSalary",
                schema: "Tbo",
                columns: table => new
                {
                    BasicSalaryId = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    TotalSalary = table.Column<decimal>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BasicSalary", x => x.BasicSalaryId);
                    table.ForeignKey(
                        name: "FK_BasicSalary_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Complaint",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TeamId = table.Column<int>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: false),
                    DriverId = table.Column<int>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: true),
                    SentDate = table.Column<DateTime>(nullable: false),
                    Subject = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: false),
                    status = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Complaint", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Complaint_Driver_DriverId",
                        column: x => x.DriverId,
                        principalSchema: "Tbo",
                        principalTable: "Driver",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Complaint_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DepartmentChange",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    from = table.Column<int>(nullable: false),
                    DepartmentId = table.Column<int>(nullable: false),
                    sentDate = table.Column<DateTime>(nullable: false),
                    status = table.Column<bool>(nullable: true),
                    reason = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DepartmentChange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DepartmentChange_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "Tbo",
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_DepartmentChange_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                    table.ForeignKey(
                        name: "FK_DepartmentChange_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Tbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "DirectorPlan",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    FiscalYear = table.Column<int>(nullable: false),
                    Period = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ProjectNumber = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: true),
                    OperationExpense = table.Column<decimal>(nullable: false),
                    EffectiveDate = table.Column<DateTime>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DirectorPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_DirectorPlan_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAllocation_1",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    vechiclesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAllocation_1", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeAllocation_1_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeAllocation_1_vechicles_vechiclesId",
                        column: x => x.vechiclesId,
                        principalSchema: "Tbo",
                        principalTable: "vechicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeAllowance",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    AllowanceId = table.Column<int>(nullable: false),
                    EffectiveDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeAllowance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeAllowance_AllowancePolicy_AllowanceId",
                        column: x => x.AllowanceId,
                        principalSchema: "Tbo",
                        principalTable: "AllowancePolicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeAllowance_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeBonus",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    BonusId = table.Column<int>(nullable: false),
                    EffectiveDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeBonus", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeBonus_BonusPolicy_BonusId",
                        column: x => x.BonusId,
                        principalSchema: "Tbo",
                        principalTable: "BonusPolicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeBonus_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "EmployeeDeduction",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    DeductionPolicyId = table.Column<int>(nullable: false),
                    EffectiveDate = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EmployeeDeduction", x => x.Id);
                    table.ForeignKey(
                        name: "FK_EmployeeDeduction_DeductionPolicy_DeductionPolicyId",
                        column: x => x.DeductionPolicyId,
                        principalSchema: "Tbo",
                        principalTable: "DeductionPolicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_EmployeeDeduction_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Experience",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true),
                    status = table.Column<bool>(nullable: true),
                    sentDate = table.Column<DateTime>(nullable: false),
                    reason = table.Column<string>(nullable: true),
                    BasicSalaryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Experience", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Experience_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Experience_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Tbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeaveAllocation",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NumberOfDays = table.Column<int>(nullable: false),
                    DateCreated = table.Column<DateTime>(nullable: false),
                    EmployeeId1 = table.Column<int>(nullable: true),
                    EmployeeId = table.Column<string>(nullable: true),
                    LeaveTypeId = table.Column<int>(nullable: false),
                    Period = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveAllocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveAllocation_Employees_EmployeeId1",
                        column: x => x.EmployeeId1,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveAllocation_LeaveType_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalSchema: "Tbo",
                        principalTable: "LeaveType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "LeaveRequests",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    Reason = table.Column<string>(nullable: true),
                    Feedback = table.Column<string>(nullable: true),
                    LeaveTypeId = table.Column<int>(nullable: false),
                    DateRequested = table.Column<DateTime>(nullable: false),
                    DateActioned = table.Column<DateTime>(nullable: true),
                    Approved = table.Column<bool>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: false),
                    ApprovedById = table.Column<int>(nullable: false),
                    hasRequested = table.Column<bool>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequests", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_LeaveType_LeaveTypeId",
                        column: x => x.LeaveTypeId,
                        principalSchema: "Tbo",
                        principalTable: "LeaveType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveRequests_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Tbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LeaveRequestVM",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    LeaveTypeId1 = table.Column<string>(nullable: true),
                    LeaveTypeId = table.Column<int>(nullable: false),
                    Reason = table.Column<string>(nullable: true),
                    Feedback = table.Column<string>(nullable: true),
                    DateRequested = table.Column<DateTime>(nullable: false),
                    DateActioned = table.Column<DateTime>(nullable: false),
                    Approved = table.Column<bool>(nullable: true),
                    Cancelled = table.Column<bool>(nullable: false),
                    ApprovedById = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    LeaveRequestVMId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LeaveRequestVM", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LeaveRequestVM_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LeaveRequestVM_LeaveRequestVM_LeaveRequestVMId",
                        column: x => x.LeaveRequestVMId,
                        principalSchema: "Tbo",
                        principalTable: "LeaveRequestVM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_LeaveRequestVM_LeaveTypeVM_LeaveTypeId1",
                        column: x => x.LeaveTypeId1,
                        principalSchema: "Tbo",
                        principalTable: "LeaveTypeVM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Maintenance",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    vechiclesId = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Feedback = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Maintenance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Maintenance_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Maintenance_vechicles_vechiclesId",
                        column: x => x.vechiclesId,
                        principalSchema: "Tbo",
                        principalTable: "vechicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "OrderAsset",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    AssetTypeId = table.Column<int>(nullable: false),
                    Quantity = table.Column<int>(nullable: false),
                    EstimatedPrice = table.Column<decimal>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: true),
                    Specification = table.Column<string>(nullable: true),
                    OrderedDate = table.Column<DateTime>(nullable: false),
                    Asset_typeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderAsset", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OrderAsset_Asset_type_Asset_typeId",
                        column: x => x.Asset_typeId,
                        principalSchema: "Tbo",
                        principalTable: "Asset_type",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_OrderAsset_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Plan",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(nullable: true),
                    FiscalYear = table.Column<int>(nullable: false),
                    PlanTypeId = table.Column<int>(nullable: false),
                    Period = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ProjectNumber = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: true),
                    OperationExpense = table.Column<decimal>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: true),
                    DepartmentId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Plan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Plan_Departments_DepartmentId",
                        column: x => x.DepartmentId,
                        principalSchema: "Tbo",
                        principalTable: "Departments",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Plan_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Plan_PlanType_PlanTypeId",
                        column: x => x.PlanTypeId,
                        principalSchema: "Tbo",
                        principalTable: "PlanType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Project",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProjectName = table.Column<string>(nullable: true),
                    ProjectDescription = table.Column<string>(nullable: true),
                    ProjectBudget = table.Column<decimal>(nullable: false),
                    ProjectStartDate = table.Column<DateTime>(nullable: false),
                    ProjectDuration = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    DirectorId = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Project", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Project_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Qualification",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Field = table.Column<string>(nullable: true),
                    Institution = table.Column<string>(nullable: true),
                    StartYear = table.Column<DateTime>(nullable: false),
                    EndYear = table.Column<DateTime>(nullable: false),
                    QualificationType = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Qualification", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Qualification_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestVehicle",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    Status = table.Column<bool>(nullable: true),
                    RouteId = table.Column<int>(nullable: true),
                    Destination = table.Column<string>(nullable: true),
                    Feedback = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestVehicle", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestVehicle_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestVehicle_Route_RouteId",
                        column: x => x.RouteId,
                        principalSchema: "Tbo",
                        principalTable: "Route",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RequestVehicleChange",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VehicleId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: true),
                    Feedback = table.Column<string>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    VechiclesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequestVehicleChange", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RequestVehicleChange_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RequestVehicleChange_vechicles_VechiclesId",
                        column: x => x.VechiclesId,
                        principalSchema: "Tbo",
                        principalTable: "vechicles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Resignation",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reason = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: true),
                    DriverId = table.Column<int>(nullable: true),
                    Date = table.Column<DateTime>(nullable: false),
                    status = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Resignation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Resignation_Driver_DriverId",
                        column: x => x.DriverId,
                        principalSchema: "Tbo",
                        principalTable: "Driver",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Resignation_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Resignation_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Tbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Salary",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GrossSalary = table.Column<decimal>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    TaxedSalary = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Salary", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Salary_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ServiceAllocation",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DriverAllocationId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ServiceAllocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ServiceAllocation_DriverAllocation_DriverAllocationId",
                        column: x => x.DriverAllocationId,
                        principalSchema: "Tbo",
                        principalTable: "DriverAllocation",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ServiceAllocation_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Termination",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Reason = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Termination", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Termination_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "WeeelyReport",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    WeeklyRecap = table.Column<string>(nullable: true),
                    TaskRecap = table.Column<string>(nullable: true),
                    TaskUnfinshed = table.Column<string>(nullable: true),
                    From = table.Column<DateTime>(nullable: false),
                    To = table.Column<DateTime>(nullable: false),
                    Challenge = table.Column<string>(nullable: true),
                    status = table.Column<bool>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: false),
                    TeamId = table.Column<int>(nullable: false),
                    ChallengeOvercome = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WeeelyReport", x => x.Id);
                    table.ForeignKey(
                        name: "FK_WeeelyReport_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_WeeelyReport_Team_TeamId",
                        column: x => x.TeamId,
                        principalSchema: "Tbo",
                        principalTable: "Team",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "AdvancedScheme",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Date = table.Column<DateTime>(nullable: false),
                    PercentageAmount = table.Column<decimal>(nullable: false),
                    DeductionEachMonth = table.Column<decimal>(nullable: false),
                    DurationRecover = table.Column<string>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: false),
                    SalarId = table.Column<int>(nullable: false),
                    BasicSalaryId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AdvancedScheme", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AdvancedScheme_BasicSalary_BasicSalaryId",
                        column: x => x.BasicSalaryId,
                        principalSchema: "Tbo",
                        principalTable: "BasicSalary",
                        principalColumn: "BasicSalaryId",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AdvancedScheme_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Budget",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DirectorPlanId = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    OverheadCosts = table.Column<double>(nullable: false),
                    TimeCosts = table.Column<double>(nullable: false),
                    ExternalCost = table.Column<double>(nullable: false),
                    TotalCost = table.Column<double>(nullable: false),
                    Status = table.Column<bool>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Budget", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Budget_DirectorPlan_DirectorPlanId",
                        column: x => x.DirectorPlanId,
                        principalSchema: "Tbo",
                        principalTable: "DirectorPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Budget_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Attendance",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    MorningCheckin = table.Column<TimeSpan>(nullable: false),
                    MorningCheckout = table.Column<TimeSpan>(nullable: false),
                    AfternoonCheckin = table.Column<TimeSpan>(nullable: false),
                    AfternoonCheckout = table.Column<TimeSpan>(nullable: false),
                    Status = table.Column<string>(nullable: true),
                    MorningWorkingHour = table.Column<TimeSpan>(nullable: false),
                    AfternoonWorkingHour = table.Column<TimeSpan>(nullable: false),
                    WorkHour = table.Column<TimeSpan>(nullable: false),
                    LateComeId = table.Column<int>(nullable: true),
                    LeaveRequestId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Attendance", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Attendance_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Attendance_LateCome_LateComeId",
                        column: x => x.LateComeId,
                        principalSchema: "Tbo",
                        principalTable: "LateCome",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Attendance_LeaveRequests_LeaveRequestId",
                        column: x => x.LeaveRequestId,
                        principalSchema: "Tbo",
                        principalTable: "LeaveRequests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "LoanRequest",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Status = table.Column<bool>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    StartDate = table.Column<DateTime>(nullable: false),
                    EndDate = table.Column<DateTime>(nullable: false),
                    EachMonthDeductionAmount = table.Column<decimal>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    TotalLoanAmount = table.Column<decimal>(nullable: false),
                    LeftLoanAmount = table.Column<decimal>(nullable: false),
                    LoanPolicyId = table.Column<int>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: false),
                    leaveRequestId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LoanRequest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LoanRequest_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanRequest_LoanPolicy_LoanPolicyId",
                        column: x => x.LoanPolicyId,
                        principalSchema: "Tbo",
                        principalTable: "LoanPolicy",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_LoanRequest_LeaveRequestVM_leaveRequestId",
                        column: x => x.leaveRequestId,
                        principalSchema: "Tbo",
                        principalTable: "LeaveRequestVM",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "FieldWork",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Place = table.Column<string>(nullable: true),
                    Duration = table.Column<int>(nullable: false),
                    StartDate = table.Column<DateTime>(nullable: false),
                    ReturnDate = table.Column<DateTime>(nullable: false),
                    PerDay = table.Column<decimal>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: true),
                    ProjectId = table.Column<int>(nullable: true),
                    DriverId = table.Column<int>(nullable: true),
                    Status = table.Column<bool>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldWork", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldWork_Driver_DriverId",
                        column: x => x.DriverId,
                        principalSchema: "Tbo",
                        principalTable: "Driver",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FieldWork_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FieldWork_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "Tbo",
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "Taskss",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TaskName = table.Column<string>(nullable: false),
                    Duration = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    TaskCost = table.Column<decimal>(nullable: false),
                    TaskProgress = table.Column<int>(nullable: false),
                    PercentCoverage = table.Column<decimal>(nullable: false),
                    Status = table.Column<bool>(nullable: true),
                    ProjectId = table.Column<int>(nullable: false),
                    AssignId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Taskss", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Taskss_Project_ProjectId",
                        column: x => x.ProjectId,
                        principalSchema: "Tbo",
                        principalTable: "Project",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "AttendanceSupExcuses",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmployeeId = table.Column<int>(nullable: false),
                    AttendanceExcusesId = table.Column<int>(nullable: false),
                    AttendanceId = table.Column<int>(nullable: false),
                    Date = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: true),
                    Reason = table.Column<string>(nullable: true),
                    AttendancesId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AttendanceSupExcuses", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AttendanceSupExcuses_AttendanceExcuses_AttendanceExcusesId",
                        column: x => x.AttendanceExcusesId,
                        principalSchema: "Tbo",
                        principalTable: "AttendanceExcuses",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_AttendanceSupExcuses_Attendance_AttendancesId",
                        column: x => x.AttendancesId,
                        principalSchema: "Tbo",
                        principalTable: "Attendance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_AttendanceSupExcuses_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "IssueSalery",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    IssueDate = table.Column<DateTime>(nullable: false),
                    Status = table.Column<bool>(nullable: true),
                    NetAmount = table.Column<decimal>(nullable: false),
                    TotalDeduction = table.Column<decimal>(nullable: true),
                    TotalAlllowance = table.Column<decimal>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: false),
                    LoanRequestId = table.Column<int>(nullable: true),
                    EmployeeBonusId = table.Column<int>(nullable: true),
                    EmployeeAllowanceId = table.Column<int>(nullable: true),
                    SalaryId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_IssueSalery", x => x.Id);
                    table.ForeignKey(
                        name: "FK_IssueSalery_EmployeeAllowance_EmployeeAllowanceId",
                        column: x => x.EmployeeAllowanceId,
                        principalSchema: "Tbo",
                        principalTable: "EmployeeAllowance",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IssueSalery_EmployeeBonus_EmployeeBonusId",
                        column: x => x.EmployeeBonusId,
                        principalSchema: "Tbo",
                        principalTable: "EmployeeBonus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IssueSalery_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_IssueSalery_LoanRequest_LoanRequestId",
                        column: x => x.LoanRequestId,
                        principalSchema: "Tbo",
                        principalTable: "LoanRequest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_IssueSalery_Salary_SalaryId",
                        column: x => x.SalaryId,
                        principalSchema: "Tbo",
                        principalTable: "Salary",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "FieldWorkDriverAllocation",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FieldWorkId = table.Column<int>(nullable: false),
                    StartDay = table.Column<DateTime>(nullable: false),
                    DriverId = table.Column<int>(nullable: true),
                    EmployeeId = table.Column<int>(nullable: false),
                    PerDay = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_FieldWorkDriverAllocation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_FieldWorkDriverAllocation_Driver_DriverId",
                        column: x => x.DriverId,
                        principalSchema: "Tbo",
                        principalTable: "Driver",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_FieldWorkDriverAllocation_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_FieldWorkDriverAllocation_FieldWork_FieldWorkId",
                        column: x => x.FieldWorkId,
                        principalSchema: "Tbo",
                        principalTable: "FieldWork",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Assigns",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EmpolyeeId = table.Column<int>(nullable: false),
                    TaskId = table.Column<int>(nullable: false),
                    ProjectId = table.Column<int>(nullable: false),
                    Status = table.Column<decimal>(nullable: true),
                    FromEmp = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Assigns", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Assigns_Employees_EmpolyeeId",
                        column: x => x.EmpolyeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Assigns_Taskss_TaskId",
                        column: x => x.TaskId,
                        principalSchema: "Tbo",
                        principalTable: "Taskss",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AdvancedScheme_BasicSalaryId",
                schema: "Tbo",
                table: "AdvancedScheme",
                column: "BasicSalaryId");

            migrationBuilder.CreateIndex(
                name: "IX_AdvancedScheme_EmployeeId",
                schema: "Tbo",
                table: "AdvancedScheme",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AnnualLossExpectancy_RiskId",
                schema: "Tbo",
                table: "AnnualLossExpectancy",
                column: "RiskId");

            migrationBuilder.CreateIndex(
                name: "EmailIndex",
                schema: "Tbo",
                table: "AspNetUsers",
                column: "NormalizedEmail");

            migrationBuilder.CreateIndex(
                name: "UserNameIndex",
                schema: "Tbo",
                table: "AspNetUsers",
                column: "NormalizedUserName",
                unique: true,
                filter: "[NormalizedUserName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_Asset_typeId",
                schema: "Tbo",
                table: "Asset",
                column: "Asset_typeId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_SupplierId",
                schema: "Tbo",
                table: "Asset",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_Allocation_AssetId",
                schema: "Tbo",
                table: "Asset_Allocation",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_Allocation_EmployeeId",
                schema: "Tbo",
                table: "Asset_Allocation",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_Allocation_UserId",
                schema: "Tbo",
                table: "Asset_Allocation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_Exchange_AssetId",
                schema: "Tbo",
                table: "Asset_Exchange",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Asset_Exchange_EmployeeId",
                schema: "Tbo",
                table: "Asset_Exchange",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetDesposal_AssetId",
                schema: "Tbo",
                table: "AssetDesposal",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetLoan_AssetId",
                schema: "Tbo",
                table: "AssetLoan",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetRequest_AssetRequestId",
                schema: "Tbo",
                table: "AssetRequest",
                column: "AssetRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetRequest_EmployeeId",
                schema: "Tbo",
                table: "AssetRequest",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetRequest_UserId",
                schema: "Tbo",
                table: "AssetRequest",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetRisk_AssetId",
                schema: "Tbo",
                table: "AssetRisk",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetRisk_RiskId",
                schema: "Tbo",
                table: "AssetRisk",
                column: "RiskId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetTypeRisk_Asset_typeId",
                schema: "Tbo",
                table: "AssetTypeRisk",
                column: "Asset_typeId");

            migrationBuilder.CreateIndex(
                name: "IX_AssetTypeRisk_RiskId",
                schema: "Tbo",
                table: "AssetTypeRisk",
                column: "RiskId");

            migrationBuilder.CreateIndex(
                name: "IX_Assigns_EmpolyeeId",
                schema: "Tbo",
                table: "Assigns",
                column: "EmpolyeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Assigns_TaskId",
                schema: "Tbo",
                table: "Assigns",
                column: "TaskId",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_EmployeeId",
                schema: "Tbo",
                table: "Attendance",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_LateComeId",
                schema: "Tbo",
                table: "Attendance",
                column: "LateComeId");

            migrationBuilder.CreateIndex(
                name: "IX_Attendance_LeaveRequestId",
                schema: "Tbo",
                table: "Attendance",
                column: "LeaveRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceSupExcuses_AttendanceExcusesId",
                schema: "Tbo",
                table: "AttendanceSupExcuses",
                column: "AttendanceExcusesId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceSupExcuses_AttendancesId",
                schema: "Tbo",
                table: "AttendanceSupExcuses",
                column: "AttendancesId");

            migrationBuilder.CreateIndex(
                name: "IX_AttendanceSupExcuses_EmployeeId",
                schema: "Tbo",
                table: "AttendanceSupExcuses",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_BasicSalary_EmployeeId",
                schema: "Tbo",
                table: "BasicSalary",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Budget_DirectorPlanId",
                schema: "Tbo",
                table: "Budget",
                column: "DirectorPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_Budget_EmployeeId",
                schema: "Tbo",
                table: "Budget",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaint_DriverId",
                schema: "Tbo",
                table: "Complaint",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Complaint_EmployeeId",
                schema: "Tbo",
                table: "Complaint",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentChange_DepartmentId",
                schema: "Tbo",
                table: "DepartmentChange",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentChange_EmployeeId",
                schema: "Tbo",
                table: "DepartmentChange",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DepartmentChange_UserId",
                schema: "Tbo",
                table: "DepartmentChange",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Departments_DivisionId",
                schema: "Tbo",
                table: "Departments",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Depersation_AssetId",
                schema: "Tbo",
                table: "Depersation",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_DirectorPlan_EmployeeId",
                schema: "Tbo",
                table: "DirectorPlan",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_DMaintainance_DriverId",
                schema: "Tbo",
                table: "DMaintainance",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_DMaintainance_vechiclesId",
                schema: "Tbo",
                table: "DMaintainance",
                column: "vechiclesId");

            migrationBuilder.CreateIndex(
                name: "IX_Donaition_AssetId",
                schema: "Tbo",
                table: "Donaition",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_Driver_UserId",
                schema: "Tbo",
                table: "Driver",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverAllocation_DriverId",
                schema: "Tbo",
                table: "DriverAllocation",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverAllocation_RouteId",
                schema: "Tbo",
                table: "DriverAllocation",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_DriverAllocation_VehicleId",
                schema: "Tbo",
                table: "DriverAllocation",
                column: "VehicleId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAllocation_1_EmployeeId",
                schema: "Tbo",
                table: "EmployeeAllocation_1",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAllocation_1_vechiclesId",
                schema: "Tbo",
                table: "EmployeeAllocation_1",
                column: "vechiclesId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAllowance_AllowanceId",
                schema: "Tbo",
                table: "EmployeeAllowance",
                column: "AllowanceId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeAllowance_EmployeeId",
                schema: "Tbo",
                table: "EmployeeAllowance",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBonus_BonusId",
                schema: "Tbo",
                table: "EmployeeBonus",
                column: "BonusId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeBonus_EmployeeId",
                schema: "Tbo",
                table: "EmployeeBonus",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDeduction_DeductionPolicyId",
                schema: "Tbo",
                table: "EmployeeDeduction",
                column: "DeductionPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_EmployeeDeduction_EmployeeId",
                schema: "Tbo",
                table: "EmployeeDeduction",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DepartmentId",
                schema: "Tbo",
                table: "Employees",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_DivisionId",
                schema: "Tbo",
                table: "Employees",
                column: "DivisionId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_EmergencyContactId",
                schema: "Tbo",
                table: "Employees",
                column: "EmergencyContactId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Employment_typeId",
                schema: "Tbo",
                table: "Employees",
                column: "Employment_typeId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_PositionId",
                schema: "Tbo",
                table: "Employees",
                column: "PositionId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_SocialsId",
                schema: "Tbo",
                table: "Employees",
                column: "SocialsId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_TeamId",
                schema: "Tbo",
                table: "Employees",
                column: "TeamId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_UserId",
                schema: "Tbo",
                table: "Employees",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Experience_EmployeeId",
                schema: "Tbo",
                table: "Experience",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Experience_UserId",
                schema: "Tbo",
                table: "Experience",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FaultyAsset_AssetId",
                schema: "Tbo",
                table: "FaultyAsset",
                column: "AssetId");

            migrationBuilder.CreateIndex(
                name: "IX_FaultyAsset_FaultyAssetTypeId",
                schema: "Tbo",
                table: "FaultyAsset",
                column: "FaultyAssetTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_FaultyAsset_UserId",
                schema: "Tbo",
                table: "FaultyAsset",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldWork_DriverId",
                schema: "Tbo",
                table: "FieldWork",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldWork_EmployeeId",
                schema: "Tbo",
                table: "FieldWork",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldWork_ProjectId",
                schema: "Tbo",
                table: "FieldWork",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldWorkDriverAllocation_DriverId",
                schema: "Tbo",
                table: "FieldWorkDriverAllocation",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldWorkDriverAllocation_EmployeeId",
                schema: "Tbo",
                table: "FieldWorkDriverAllocation",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_FieldWorkDriverAllocation_FieldWorkId",
                schema: "Tbo",
                table: "FieldWorkDriverAllocation",
                column: "FieldWorkId");

            migrationBuilder.CreateIndex(
                name: "IX_hierarchical_Parentid",
                schema: "Tbo",
                table: "hierarchical",
                column: "Parentid");

            migrationBuilder.CreateIndex(
                name: "IX_hierarchical_hierarchicalID",
                schema: "Tbo",
                table: "hierarchical",
                column: "hierarchicalID");

            migrationBuilder.CreateIndex(
                name: "IX_IssueSalery_EmployeeAllowanceId",
                schema: "Tbo",
                table: "IssueSalery",
                column: "EmployeeAllowanceId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueSalery_EmployeeBonusId",
                schema: "Tbo",
                table: "IssueSalery",
                column: "EmployeeBonusId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueSalery_EmployeeId",
                schema: "Tbo",
                table: "IssueSalery",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueSalery_LoanRequestId",
                schema: "Tbo",
                table: "IssueSalery",
                column: "LoanRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_IssueSalery_SalaryId",
                schema: "Tbo",
                table: "IssueSalery",
                column: "SalaryId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocation_EmployeeId1",
                schema: "Tbo",
                table: "LeaveAllocation",
                column: "EmployeeId1");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveAllocation_LeaveTypeId",
                schema: "Tbo",
                table: "LeaveAllocation",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_EmployeeId",
                schema: "Tbo",
                table: "LeaveRequests",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_LeaveTypeId",
                schema: "Tbo",
                table: "LeaveRequests",
                column: "LeaveTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequests_UserId",
                schema: "Tbo",
                table: "LeaveRequests",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequestVM_EmployeeId",
                schema: "Tbo",
                table: "LeaveRequestVM",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequestVM_LeaveRequestVMId",
                schema: "Tbo",
                table: "LeaveRequestVM",
                column: "LeaveRequestVMId");

            migrationBuilder.CreateIndex(
                name: "IX_LeaveRequestVM_LeaveTypeId1",
                schema: "Tbo",
                table: "LeaveRequestVM",
                column: "LeaveTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_LoanRequest_EmployeeId",
                schema: "Tbo",
                table: "LoanRequest",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanRequest_LoanPolicyId",
                schema: "Tbo",
                table: "LoanRequest",
                column: "LoanPolicyId");

            migrationBuilder.CreateIndex(
                name: "IX_LoanRequest_leaveRequestId",
                schema: "Tbo",
                table: "LoanRequest",
                column: "leaveRequestId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_EmployeeId",
                schema: "Tbo",
                table: "Maintenance",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Maintenance_vechiclesId",
                schema: "Tbo",
                table: "Maintenance",
                column: "vechiclesId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderAsset_Asset_typeId",
                schema: "Tbo",
                table: "OrderAsset",
                column: "Asset_typeId");

            migrationBuilder.CreateIndex(
                name: "IX_OrderAsset_EmployeeId",
                schema: "Tbo",
                table: "OrderAsset",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Payment_SupplierId",
                schema: "Tbo",
                table: "Payment",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_DepartmentId",
                schema: "Tbo",
                table: "Plan",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_EmployeeId",
                schema: "Tbo",
                table: "Plan",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Plan_PlanTypeId",
                schema: "Tbo",
                table: "Plan",
                column: "PlanTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Project_EmployeeId",
                schema: "Tbo",
                table: "Project",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Qualification_EmployeeId",
                schema: "Tbo",
                table: "Qualification",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestVehicle_EmployeeId",
                schema: "Tbo",
                table: "RequestVehicle",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestVehicle_RouteId",
                schema: "Tbo",
                table: "RequestVehicle",
                column: "RouteId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestVehicleChange_EmployeeId",
                schema: "Tbo",
                table: "RequestVehicleChange",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_RequestVehicleChange_VechiclesId",
                schema: "Tbo",
                table: "RequestVehicleChange",
                column: "VechiclesId");

            migrationBuilder.CreateIndex(
                name: "IX_Resignation_DriverId",
                schema: "Tbo",
                table: "Resignation",
                column: "DriverId");

            migrationBuilder.CreateIndex(
                name: "IX_Resignation_EmployeeId",
                schema: "Tbo",
                table: "Resignation",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Resignation_UserId",
                schema: "Tbo",
                table: "Resignation",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Risk_RiskTypeId",
                schema: "Tbo",
                table: "Risk",
                column: "RiskTypeId");

            migrationBuilder.CreateIndex(
                name: "RoleNameIndex",
                schema: "Tbo",
                table: "Role",
                column: "NormalizedName",
                unique: true,
                filter: "[NormalizedName] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_RoleClaims_RoleId",
                schema: "Tbo",
                table: "RoleClaims",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_Salary_EmployeeId",
                schema: "Tbo",
                table: "Salary",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAllocation_DriverAllocationId",
                schema: "Tbo",
                table: "ServiceAllocation",
                column: "DriverAllocationId");

            migrationBuilder.CreateIndex(
                name: "IX_ServiceAllocation_EmployeeId",
                schema: "Tbo",
                table: "ServiceAllocation",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_Taskss_ProjectId",
                schema: "Tbo",
                table: "Taskss",
                column: "ProjectId");

            migrationBuilder.CreateIndex(
                name: "IX_Team_DepartmentId",
                schema: "Tbo",
                table: "Team",
                column: "DepartmentId");

            migrationBuilder.CreateIndex(
                name: "IX_Termination_EmployeeId",
                schema: "Tbo",
                table: "Termination",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_UserClaims_UserId",
                schema: "Tbo",
                table: "UserClaims",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserLogins_UserId",
                schema: "Tbo",
                table: "UserLogins",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_UserRoles_RoleId",
                schema: "Tbo",
                table: "UserRoles",
                column: "RoleId");

            migrationBuilder.CreateIndex(
                name: "IX_vechicles_vechiclesTypeId",
                schema: "Tbo",
                table: "vechicles",
                column: "vechiclesTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_WeeelyReport_EmployeeId",
                schema: "Tbo",
                table: "WeeelyReport",
                column: "EmployeeId");

            migrationBuilder.CreateIndex(
                name: "IX_WeeelyReport_TeamId",
                schema: "Tbo",
                table: "WeeelyReport",
                column: "TeamId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "AdvancedScheme",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "AnnualLossExpectancy",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Asset_Allocation",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Asset_Exchange",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "AssetDesposal",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "AssetLoan",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "AssetRequest",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "AssetRisk",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "AssetTypeRisk",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Assigns",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "AttendanceSupExcuses",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "AuctionMember",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "AuctionOwner",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Budget",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "CalendarEvent",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Complaint",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "DepartmentChange",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Depersation",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Disciplinary",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "DisciplinaryType",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "DMaintainance",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Donaition",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "EmployeeAllocation_1",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "EmployeeDeduction",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Experience",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "FaultyAsset",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "FieldWorkDriverAllocation",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "hierarchical",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "IssueSalery",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "LeaveAllocation",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Maintenance",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "OrderAsset",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Payment",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Plan",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "ProjectType",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Qualification",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "QualificationType",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "RequestVehicle",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "RequestVehicleChange",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Resignation",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "RoleClaims",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "ServiceAllocation",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Tax",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Termination",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Units",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "User",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "UserClaims",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "UserLogins",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "UserRoles",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "UserTokens",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "WeeelyReport",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "BasicSalary",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Risk",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Taskss",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "AttendanceExcuses",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Attendance",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "DirectorPlan",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "DeductionPolicy",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Asset",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "FaultyAssetType",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "FieldWork",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Parent",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "EmployeeAllowance",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "EmployeeBonus",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "LoanRequest",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Salary",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "PlanType",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "DriverAllocation",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Role",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "RiskType",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "LateCome",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "LeaveRequests",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Asset_type",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Supplier",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Project",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "AllowancePolicy",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "BonusPolicy",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "LoanPolicy",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "LeaveRequestVM",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Driver",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Route",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "vechicles",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "LeaveType",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Employees",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "LeaveTypeVM",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "vechiclesType",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "EmergencyContact",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Employment_Types",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Position",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Socials",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Team",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "AspNetUsers",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Departments",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Division",
                schema: "Tbo");
        }
    }
}
