 using Erp.Areas.Identity.Data;
using Erp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Erp.Data { 
    public class EmployeeContext : IdentityDbContext<User>
    {
        public EmployeeContext(DbContextOptions<EmployeeContext> options) : base(options)
        {
        }

        public DbSet<Employee> Employees { get; set; }
        public DbSet<Complaint> Complaint { get; set; }
        public DbSet<Position> Position { get; set; }
        public DbSet<Employment_Type> Employment_Types { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Department> Departments { get; set; }

        public DbSet<SendFile> SendFile { get; set; }
        public DbSet<AttendanceExcuses> AttendanceExcuses { get; set; }
        public DbSet<Attendances> Attendance { get; set; }
        public DbSet<LeaveType> LeaveType { get; set; }
        public DbSet<LeaveRequest> LeaveRequests { get; set; }
        public DbSet<LeaveAllocation> LeaveAllocation { get; set; }
        public DbSet<Socials> Socials { get; set; }
        public DbSet<LateCome> LateCome { get; set; }

        public DbSet<EmergencyContact> EmergencyContact { get; set; }
        public DbSet<AllowancePolicy> AllowancePolicy { get; set; }
        public DbSet<hierarchical> hierarchical { get; set; }
        public DbSet<Termination> Termination { get; set; }
        public DbSet<LeaveTypeVM> LeaveTypeVM { get; set; }
        public DbSet<LeaveRequestVM> LeaveRequestVM { get; set; }
        public DbSet<Experience> Experience { get; set; }
        public DbSet<Division> Division { get; set; }
        public DbSet<DepartmentChange> DepartmentChange { get; set; }
        public DbSet<CalendarEvent> CalendarEvent { get; set; }
        public DbSet<Asset> Asset { get; set; }
        public DbSet<Asset_Allocation> Asset_Allocation { get; set; }
        public DbSet<Asset_Exchange> Asset_Exchange { get; set; }
        public DbSet<Asset_type> Asset_type { get; set; }
        public DbSet<Depersation> Depersation { get; set; }
        public DbSet<Payment> Payment { get; set; }
        public DbSet<Supplier> Supplier { get; set; }
        public DbSet<AssetRequest> AssetRequest { get; set; }
        public DbSet<Qualification> Qualification { get; set; }
        public DbSet<Team> Team { get; set; }
        public DbSet<QualificationType> QualificationType { get; set; }
        public DbSet<Resignation> Resignation { get; set; }
        public DbSet<DisciplinaryType> DisciplinaryType { get; set; }
        public DbSet<Disciplinary> Disciplinary { get; set; }
        public DbSet<FaultyAsset> FaultyAsset { get; set; }
        public DbSet<FaultyAssetType> FaultyAssetType { get; set; }
        public DbSet<Experience> ExperienceReport { get; set; }
        public DbSet<AssetLoan> AssetLoan { get; set; }
        public DbSet<Donaition> Donaition { get; set; }
        public DbSet<AssetDesposal> AssetDesposal { get; set; }
        public DbSet<vechicles> vechicles { get; set; }
        public DbSet<Driver> Driver { get; set; }
        public DbSet<Maintenance> Maintenance { get; set; }
        public DbSet<RequestVehicleChange> RequestVehicleChange { get; set; }

        public DbSet<RequestVehicle> RequestVehicle { get; set; }
        public DbSet<ServiceAllocation> ServiceAllocation { get; set; }
        public DbSet<vechiclesType> vechiclesType { get; set; }
        public DbSet<WeeelyReport> WeeelyReport { get; set; }
        public DbSet<RiskType> RiskType { get; set; }
        public DbSet<Risk> Risk { get; set; }
        public DbSet<AnnualLossExpectancy> AnnualLossExpectancy { get; set; }
     
        public DbSet<ProjectType> ProjectType { get; internal set; }
        public DbSet<Project> Project { get; internal set; }
        public DbSet<Plan> Plan { get;  set; }
        public DbSet<PlanType> PlanType { get;  set; }
        public DbSet<Budget> Budget { get; internal set; }
        public DbSet<DirectorPlan> DirectorPlan { get; internal set; }
        public DbSet<Region> Region { get; internal set; }
        public DbSet<Zone> Zone { get; internal set; }

        public DbSet<RegionPlan> RegionPlan { get; internal set; }
        public DbSet<RegionBudget> RegionBudget { get; internal set; }
        public DbSet<ZonePlan> ZonePlan { get; internal set; }

        public DbSet<ZoneBudget> ZoneBudget { get; internal set; }





        public DbSet<AttendanceSupExcuses> AttendanceSupExcuses { get; internal set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            builder.HasDefaultSchema("Tbo");
            builder.Entity<IdentityUser>(entity =>
            {
                entity.ToTable(name: "User");
            });
            builder.Entity<IdentityRole>(entity =>
            {
                entity.ToTable(name: "Role");
            });
            builder.Entity<IdentityUserRole<string>>(entity =>
            {
                entity.ToTable("UserRoles");
            });
            builder.Entity<IdentityUserClaim<string>>(entity =>
            {
                entity.ToTable("UserClaims");
            });
            builder.Entity<IdentityUserLogin<string>>(entity =>
            {
                entity.ToTable("UserLogins");
            });
            builder.Entity<IdentityRoleClaim<string>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });
            builder.Entity<IdentityUserToken<string>>(entity =>
            {
                entity.ToTable("UserTokens");
            });

           /* builder.Entity<IdentityRole>().HasData(new IdentityRole { Name = "Admin", NormalizedName = "Admin".ToUpper() });*/
        }



        public DbSet<Erp.Models.AssetRisk> AssetRisk { get; set; }

        public DbSet<Erp.Models.AssetTypeRisk> AssetTypeRisk { get; set; }

        public DbSet<Erp.Models.Salary> Salary { get; set; }

        public DbSet<Erp.Models.Assign> Assigns { get; set; }
        public DbSet<Erp.Models.EmployeeAllowance> EmployeeAllowance { get; set; }

        public DbSet<Erp.Models.DeductionPolicy> DeductionPolicy { get; set; }

        public DbSet<EmployeeDeduction> EmployeeDeduction { get; set; }

        public DbSet<AdvancedScheme> AdvancedScheme { get; set; }

        public DbSet<LoanPolicy> LoanPolicy { get; set; }

        public DbSet<IssueSalery> IssueSalery { get; set; }

        public DbSet<LoanRequest> LoanRequest { get; set; }

        public DbSet<EmployeeBonus> EmployeeBonus { get; set; }

        public DbSet<BonusPolicy> BonusPolicy { get; set; } 

        public DbSet<Erp.Models.Tax> Tax { get; set; }

        public DbSet<Erp.Models.Tasks> Taskss { get; set; }
        public IEnumerable<object> Tasks { get; internal set; }
        public DbSet<Erp.Models.OrderAsset> OrderAsset { get; set; }
        public DbSet<Erp.Models.AuctionOwner> AuctionOwner { get; set; }
        public DbSet<Erp.Models.AuctionMember> AuctionMember { get; set; }

        public DbSet<Erp.Models.Route> Route { get; set; }
        public DbSet<Erp.Models.DriverAllocation> DriverAllocation { get; set; }

        public DbSet<FieldWork> FieldWork { get; set; }
        public DbSet<FieldWorkDriverAllocation> FieldWorkDriverAllocation { get; set; }
        public DbSet<Erp.Models.ServiceAllocation> EmployeeAllocation { get; set; }
        public DbSet<Erp.Models.EmployeeAllocation> EmployeeAllocation_1 { get; set; }
        public DbSet<Erp.Models.DMaintainance> DMaintainance { get; set; }

    }
}
