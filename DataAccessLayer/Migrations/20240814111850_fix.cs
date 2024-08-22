using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CommentedBy",
                table: "TicketComments",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6feb7ae5-bd2f-48ec-b004-fce99cc30f8e"),
                column: "Password",
                value: "AQAAAAEAACcQAAAAEA9VMbYTs1OaCaEiGNmEH61zGBepp4HWoScRyd0FVZTqUW99YEWdlA9aUNp1eLaFDg==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "CommentedBy",
                table: "TicketComments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6feb7ae5-bd2f-48ec-b004-fce99cc30f8e"),
                column: "Password",
                value: "AQAAAAEAACcQAAAAEB0xIWmmMXXJHj2Lzn8WmAKRJQHxZJ4L4F/pk1j6ngSpRYucVUShsKSwh4C9fNNhaA==");
        }
    }
}
