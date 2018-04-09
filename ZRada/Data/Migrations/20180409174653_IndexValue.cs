using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace ZRada.Data.Migrations
{
    public partial class IndexValue : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Votes",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Votes_Value",
                table: "Votes",
                column: "Value");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Votes_Value",
                table: "Votes");

            migrationBuilder.AlterColumn<string>(
                name: "Value",
                table: "Votes",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
