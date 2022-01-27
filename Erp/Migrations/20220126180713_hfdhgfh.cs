using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Erp.Migrations
{
    public partial class hfdhgfh : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SendFile",
                schema: "Tbo",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    path = table.Column<string>(nullable: true),
                    senderId = table.Column<int>(nullable: false),
                    ReciverId = table.Column<int>(nullable: false),
                    DateTime = table.Column<DateTime>(nullable: false),
                    filesatuts = table.Column<bool>(nullable: false),
                    EmployeeId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SendFile", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SendFile_Employees_EmployeeId",
                        column: x => x.EmployeeId,
                        principalSchema: "Tbo",
                        principalTable: "Employees",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SendFile_EmployeeId",
                schema: "Tbo",
                table: "SendFile",
                column: "EmployeeId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SendFile",
                schema: "Tbo");
        }
    }
}
