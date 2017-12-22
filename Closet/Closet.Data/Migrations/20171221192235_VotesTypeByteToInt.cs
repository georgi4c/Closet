using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace Closet.Data.Migrations
{
    public partial class VotesTypeByteToInt : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Value",
                table: "MemeVotes",
                nullable: false,
                oldClrType: typeof(byte));

            migrationBuilder.AlterColumn<int>(
                name: "Value",
                table: "CommentVotes",
                nullable: false,
                oldClrType: typeof(byte));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<byte>(
                name: "Value",
                table: "MemeVotes",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AlterColumn<byte>(
                name: "Value",
                table: "CommentVotes",
                nullable: false,
                oldClrType: typeof(int));
        }
    }
}
