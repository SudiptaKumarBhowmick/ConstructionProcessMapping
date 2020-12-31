using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Portal.Data.Migrations
{
    public partial class _3tablesadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "StepName",
                table: "Jobdata");

            migrationBuilder.DropColumn(
                name: "StepNumber",
                table: "Jobdata");

            migrationBuilder.AddColumn<string>(
                name: "ContractingOrganisationType",
                table: "Jobdata",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "OrganisationType",
                table: "Jobdata",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CustomInputs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<string>(nullable: true),
                    ModifyBy = table.Column<string>(nullable: true),
                    InputName = table.Column<string>(nullable: true),
                    StructuringInformationModelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomInputs_Jobdata_StructuringInformationModelId",
                        column: x => x.StructuringInformationModelId,
                        principalTable: "Jobdata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CustomOutputs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<string>(nullable: true),
                    ModifyBy = table.Column<string>(nullable: true),
                    OutputName = table.Column<string>(nullable: true),
                    StructuringInformationModelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomOutputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomOutputs_Jobdata_StructuringInformationModelId",
                        column: x => x.StructuringInformationModelId,
                        principalTable: "Jobdata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GenericInputs",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<string>(nullable: true),
                    ModifyBy = table.Column<string>(nullable: true),
                    key = table.Column<string>(nullable: true),
                    value = table.Column<int>(nullable: false),
                    StructuringInformationModelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GenericInputs", x => x.Id);
                    table.ForeignKey(
                        name: "FK_GenericInputs_Jobdata_StructuringInformationModelId",
                        column: x => x.StructuringInformationModelId,
                        principalTable: "Jobdata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "JobSteps",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CreationDate = table.Column<DateTime>(nullable: false),
                    ModificationDate = table.Column<DateTime>(nullable: false),
                    CreateBy = table.Column<string>(nullable: true),
                    ModifyBy = table.Column<string>(nullable: true),
                    key = table.Column<string>(nullable: true),
                    value = table.Column<int>(nullable: false),
                    StructuringInformationModelId = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JobSteps", x => x.Id);
                    table.ForeignKey(
                        name: "FK_JobSteps_Jobdata_StructuringInformationModelId",
                        column: x => x.StructuringInformationModelId,
                        principalTable: "Jobdata",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CustomInputs_StructuringInformationModelId",
                table: "CustomInputs",
                column: "StructuringInformationModelId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomOutputs_StructuringInformationModelId",
                table: "CustomOutputs",
                column: "StructuringInformationModelId");

            migrationBuilder.CreateIndex(
                name: "IX_GenericInputs_StructuringInformationModelId",
                table: "GenericInputs",
                column: "StructuringInformationModelId");

            migrationBuilder.CreateIndex(
                name: "IX_JobSteps_StructuringInformationModelId",
                table: "JobSteps",
                column: "StructuringInformationModelId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CustomInputs");

            migrationBuilder.DropTable(
                name: "CustomOutputs");

            migrationBuilder.DropTable(
                name: "GenericInputs");

            migrationBuilder.DropTable(
                name: "JobSteps");

            migrationBuilder.DropColumn(
                name: "ContractingOrganisationType",
                table: "Jobdata");

            migrationBuilder.DropColumn(
                name: "OrganisationType",
                table: "Jobdata");

            migrationBuilder.AddColumn<string>(
                name: "CompanyType",
                table: "Jobdata",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ContractingCompanyType",
                table: "Jobdata",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomInput",
                table: "Jobdata",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CustomOutput",
                table: "Jobdata",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GenericInputDescription",
                table: "Jobdata",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GenericInputType",
                table: "Jobdata",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "StepName",
                table: "Jobdata",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "StepNumber",
                table: "Jobdata",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
