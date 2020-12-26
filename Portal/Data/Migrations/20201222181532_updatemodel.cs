using Microsoft.EntityFrameworkCore.Migrations;

namespace Portal.Data.Migrations
{
    public partial class updatemodel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Input",
                table: "Jobdata");

            migrationBuilder.DropColumn(
                name: "PrimaryPrecedingJob",
                table: "Jobdata");

            migrationBuilder.DropColumn(
                name: "PrimarySubsequentJob",
                table: "Jobdata");

            migrationBuilder.DropColumn(
                name: "SecondaryPrecedingJob1",
                table: "Jobdata");

            migrationBuilder.DropColumn(
                name: "SecondaryPrecedingJob2",
                table: "Jobdata");

            migrationBuilder.DropColumn(
                name: "SecondarySubsequentJob1",
                table: "Jobdata");

            migrationBuilder.DropColumn(
                name: "SecondarySubsequentJob2",
                table: "Jobdata");

            migrationBuilder.AddColumn<string>(
                name: "CompanyType",
                table: "Jobdata",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractingCompanyType",
                table: "Jobdata",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomInput",
                table: "Jobdata",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomOutput",
                table: "Jobdata",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GenericInputDescription",
                table: "Jobdata",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GenericInputType",
                table: "Jobdata",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CompanyType",
                table: "Jobdata");

            migrationBuilder.DropColumn(
                name: "ContractingCompanyType",
                table: "Jobdata");

            migrationBuilder.DropColumn(
                name: "CustomInput",
                table: "Jobdata");

            migrationBuilder.DropColumn(
                name: "CustomOutput",
                table: "Jobdata");

            migrationBuilder.DropColumn(
                name: "GenericInputDescription",
                table: "Jobdata");

            migrationBuilder.DropColumn(
                name: "GenericInputType",
                table: "Jobdata");

            migrationBuilder.AddColumn<string>(
                name: "Input",
                table: "Jobdata",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimaryPrecedingJob",
                table: "Jobdata",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PrimarySubsequentJob",
                table: "Jobdata",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondaryPrecedingJob1",
                table: "Jobdata",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondaryPrecedingJob2",
                table: "Jobdata",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondarySubsequentJob1",
                table: "Jobdata",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SecondarySubsequentJob2",
                table: "Jobdata",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
