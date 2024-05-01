using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccess.Migrations
{
    /// <inheritdoc />
    public partial class v3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Buildings_ConstructionCompany_ConstructionCompanyCompanyId",
                table: "Buildings");

            migrationBuilder.DropTable(
                name: "ConstructionCompany");

            migrationBuilder.DropIndex(
                name: "IX_Buildings_ConstructionCompanyCompanyId",
                table: "Buildings");

            migrationBuilder.DropColumn(
                name: "ConstructionCompanyCompanyId",
                table: "Buildings");

            migrationBuilder.AddColumn<string>(
                name: "ConstructionCompany",
                table: "Buildings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ConstructionCompany",
                table: "Buildings");

            migrationBuilder.AddColumn<Guid>(
                name: "ConstructionCompanyCompanyId",
                table: "Buildings",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "ConstructionCompany",
                columns: table => new
                {
                    CompanyId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ConstructionCompany", x => x.CompanyId);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Buildings_ConstructionCompanyCompanyId",
                table: "Buildings",
                column: "ConstructionCompanyCompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_Buildings_ConstructionCompany_ConstructionCompanyCompanyId",
                table: "Buildings",
                column: "ConstructionCompanyCompanyId",
                principalTable: "ConstructionCompany",
                principalColumn: "CompanyId",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
