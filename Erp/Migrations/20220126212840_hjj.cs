using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Erp.Migrations
{
    public partial class hjj : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "filesatuts",
                schema: "Tbo",
                table: "SendFile",
                nullable: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.CreateTable(
                name: "Region",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RegionCode = table.Column<string>(nullable: false),
                    RegionName = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Sex = table.Column<string>(nullable: false),
                    Mobile = table.Column<string>(maxLength: 13, nullable: false),
                    WorkTelephone = table.Column<string>(maxLength: 13, nullable: true),
                    AboutMe = table.Column<string>(nullable: true),
                    AreaOfExpertise = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Region", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Region_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Tbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegionPlan",
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
                    RegionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionPlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegionPlan_Region_RegionId",
                        column: x => x.RegionId,
                        principalSchema: "Tbo",
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Zone",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ZoneCode = table.Column<string>(nullable: false),
                    ZoneName = table.Column<string>(nullable: false),
                    FirstName = table.Column<string>(nullable: false),
                    LastName = table.Column<string>(nullable: false),
                    Sex = table.Column<string>(nullable: false),
                    Mobile = table.Column<string>(maxLength: 13, nullable: false),
                    WorkTelephone = table.Column<string>(maxLength: 13, nullable: true),
                    AboutMe = table.Column<string>(nullable: true),
                    AreaOfExpertise = table.Column<string>(nullable: true),
                    UserId = table.Column<string>(nullable: true),
                    RegionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Zone", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Zone_Region_RegionId",
                        column: x => x.RegionId,
                        principalSchema: "Tbo",
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Zone_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalSchema: "Tbo",
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RegionBudget",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    TotalRegionOverheadCosts = table.Column<double>(nullable: false),
                    TotalRegionTimeCosts = table.Column<double>(nullable: false),
                    TotalRegionExternalCost = table.Column<double>(nullable: false),
                    TotalRegionZoneTotalCost = table.Column<double>(nullable: false),
                    Status = table.Column<bool>(nullable: true),
                    RegionPlanId = table.Column<int>(nullable: false),
                    RegionId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RegionBudget", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RegionBudget_Region_RegionId",
                        column: x => x.RegionId,
                        principalSchema: "Tbo",
                        principalTable: "Region",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_RegionBudget_RegionPlan_RegionPlanId",
                        column: x => x.RegionPlanId,
                        principalSchema: "Tbo",
                        principalTable: "RegionPlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateTable(
                name: "ZonePlan",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PlanName = table.Column<string>(nullable: true),
                    FiscalYear = table.Column<int>(nullable: false),
                    PlanTypeId = table.Column<int>(nullable: false),
                    Period = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    ProjectNumber = table.Column<int>(nullable: false),
                    Status = table.Column<bool>(nullable: true),
                    OperationExpense = table.Column<decimal>(nullable: false),
                    ZoneId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZonePlan", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZonePlan_PlanType_PlanTypeId",
                        column: x => x.PlanTypeId,
                        principalSchema: "Tbo",
                        principalTable: "PlanType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ZonePlan_Zone_ZoneId",
                        column: x => x.ZoneId,
                        principalSchema: "Tbo",
                        principalTable: "Zone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ZoneBudget",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Description = table.Column<string>(nullable: true),
                    OverheadCosts = table.Column<double>(nullable: false),
                    TimeCosts = table.Column<double>(nullable: false),
                    ExternalCost = table.Column<double>(nullable: false),
                    ZoneTotalCost = table.Column<double>(nullable: false),
                    Status = table.Column<bool>(nullable: true),
                    ZonePlanId = table.Column<int>(nullable: false),
                    ZoneId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ZoneBudget", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ZoneBudget_Zone_ZoneId",
                        column: x => x.ZoneId,
                        principalSchema: "Tbo",
                        principalTable: "Zone",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ZoneBudget_ZonePlan_ZonePlanId",
                        column: x => x.ZonePlanId,
                        principalSchema: "Tbo",
                        principalTable: "ZonePlan",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.NoAction);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Region_UserId",
                schema: "Tbo",
                table: "Region",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_RegionBudget_RegionId",
                schema: "Tbo",
                table: "RegionBudget",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_RegionBudget_RegionPlanId",
                schema: "Tbo",
                table: "RegionBudget",
                column: "RegionPlanId");

            migrationBuilder.CreateIndex(
                name: "IX_RegionPlan_RegionId",
                schema: "Tbo",
                table: "RegionPlan",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Zone_RegionId",
                schema: "Tbo",
                table: "Zone",
                column: "RegionId");

            migrationBuilder.CreateIndex(
                name: "IX_Zone_UserId",
                schema: "Tbo",
                table: "Zone",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneBudget_ZoneId",
                schema: "Tbo",
                table: "ZoneBudget",
                column: "ZoneId");

            migrationBuilder.CreateIndex(
                name: "IX_ZoneBudget_ZonePlanId",
                schema: "Tbo",
                table: "ZoneBudget",
                column: "ZonePlanId");

            migrationBuilder.CreateIndex(
                name: "IX_ZonePlan_PlanTypeId",
                schema: "Tbo",
                table: "ZonePlan",
                column: "PlanTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_ZonePlan_ZoneId",
                schema: "Tbo",
                table: "ZonePlan",
                column: "ZoneId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "RegionBudget",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "ZoneBudget",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "RegionPlan",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "ZonePlan",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Zone",
                schema: "Tbo");

            migrationBuilder.DropTable(
                name: "Region",
                schema: "Tbo");

            migrationBuilder.AlterColumn<bool>(
                name: "filesatuts",
                schema: "Tbo",
                table: "SendFile",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldNullable: true);
        }
    }
}
