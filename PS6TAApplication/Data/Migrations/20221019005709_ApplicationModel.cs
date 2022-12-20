using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PS6TAApplication.Data.Migrations
{
    public partial class ApplicationModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ApplicationId",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "Application",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TAUserId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    CurrentDegree = table.Column<int>(type: "int", nullable: false),
                    Department = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    UofUGPA = table.Column<float>(type: "real", nullable: false),
                    HoursRequested = table.Column<int>(type: "int", nullable: false),
                    IsAvailableWeekBefore = table.Column<bool>(type: "bit", nullable: false),
                    SemestersCompleted = table.Column<int>(type: "int", nullable: false),
                    Statement = table.Column<string>(type: "nvarchar(max)", maxLength: 50000, nullable: true),
                    TransferSchool = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: true),
                    LinkedInURL = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    ResumeFileName = table.Column<string>(type: "nvarchar(1000)", maxLength: 1000, nullable: true),
                    CreationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ModificationDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Application", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Application_AspNetUsers_TAUserId",
                        column: x => x.TAUserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Application_TAUserId",
                table: "Application",
                column: "TAUserId",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Application");

            migrationBuilder.DropColumn(
                name: "ApplicationId",
                table: "AspNetUsers");
        }
    }
}
