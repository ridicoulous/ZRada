using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ZRada.Data.Migrations
{
    public partial class VoteDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Votes_Value",
                table: "Votes");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Votes",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.CreateIndex(
                name: "IX_Votes_Value_Date",
                table: "Votes",
                columns: new[] { "Value", "Date" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Votes_Value_Date",
                table: "Votes");

            migrationBuilder.DropColumn(
                name: "Date",
                table: "Votes");

            migrationBuilder.CreateIndex(
                name: "IX_Votes_Value",
                table: "Votes",
                column: "Value");
        }
    }
}
