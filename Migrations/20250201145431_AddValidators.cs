using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CollectiveComments.Migrations
{
    /// <inheritdoc />
    public partial class AddValidators : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_feedbacks_companies_CompanyId",
                table: "feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_feedbacks_CompanyId",
                table: "feedbacks");

            migrationBuilder.DropColumn(
                name: "CompanyId",
                table: "feedbacks");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "feedbacks",
                type: "character varying(600)",
                maxLength: 600,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(500)",
                oldMaxLength: 500);

            migrationBuilder.AddColumn<string>(
                name: "CompanyCode",
                table: "feedbacks",
                type: "character varying(40)",
                maxLength: 40,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_companies_Code",
                table: "companies",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_feedbacks_CompanyCode",
                table: "feedbacks",
                column: "CompanyCode");

            migrationBuilder.AddForeignKey(
                name: "FK_feedbacks_companies_CompanyCode",
                table: "feedbacks",
                column: "CompanyCode",
                principalTable: "companies",
                principalColumn: "Code",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_feedbacks_companies_CompanyCode",
                table: "feedbacks");

            migrationBuilder.DropIndex(
                name: "IX_feedbacks_CompanyCode",
                table: "feedbacks");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_companies_Code",
                table: "companies");

            migrationBuilder.DropColumn(
                name: "CompanyCode",
                table: "feedbacks");

            migrationBuilder.AlterColumn<string>(
                name: "Message",
                table: "feedbacks",
                type: "character varying(500)",
                maxLength: 500,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(600)",
                oldMaxLength: 600);

            migrationBuilder.AddColumn<Guid>(
                name: "CompanyId",
                table: "feedbacks",
                type: "uuid",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_feedbacks_CompanyId",
                table: "feedbacks",
                column: "CompanyId");

            migrationBuilder.AddForeignKey(
                name: "FK_feedbacks_companies_CompanyId",
                table: "feedbacks",
                column: "CompanyId",
                principalTable: "companies",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
