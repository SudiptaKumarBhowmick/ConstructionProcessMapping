using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Portal.Data.Migrations
{
    public partial class job_data_added : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Jobdata",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<string>(nullable: true),
                    ModifyBy = table.Column<string>(nullable: true),
                    Record = table.Column<Guid>(nullable: false),
                    JobName = table.Column<string>(nullable: true),
                    StepNumber = table.Column<int>(nullable: false),
                    JobExecutor = table.Column<string>(nullable: true),
                    StepName = table.Column<string>(nullable: true),
                    Input = table.Column<string>(nullable: true),
                    PrimaryPrecedingJob = table.Column<string>(nullable: true),
                    PrimarySubsequentJob = table.Column<string>(nullable: true),
                    SecondaryPrecedingJob1 = table.Column<string>(nullable: true),
                    SecondaryPrecedingJob2 = table.Column<string>(nullable: true),
                    SecondarySubsequentJob1 = table.Column<string>(nullable: true),
                    SecondarySubsequentJob2 = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Jobdata", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Jobdata");
        }
    }
}
