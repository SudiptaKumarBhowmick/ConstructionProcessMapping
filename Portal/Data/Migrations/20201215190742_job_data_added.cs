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
                    JobExecutor = table.Column<string>(nullable: true),
                    CompanyType = table.Column<string>(nullable: true),
                    ContractingCompanyType = table.Column<string>(nullable: true),
                    CustomInput = table.Column<string>(nullable: true),
                    StepNumber = table.Column<int>(nullable: false),
                    StepName = table.Column<string>(nullable: true),
                    GenericInputType = table.Column<string>(nullable: true),
                    GenericInputDescription = table.Column<string>(nullable: true),
                    CustomOutput = table.Column<string>(nullable: true),
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
