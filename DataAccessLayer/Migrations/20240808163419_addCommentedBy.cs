using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class addCommentedBy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CommentedBy",
                table: "TicketComments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "Attachments",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6feb7ae5-bd2f-48ec-b004-fce99cc30f8e"),
                column: "Password",
                value: "AQAAAAEAACcQAAAAEB0xIWmmMXXJHj2Lzn8WmAKRJQHxZJ4L4F/pk1j6ngSpRYucVUShsKSwh4C9fNNhaA==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CommentedBy",
                table: "TicketComments");

            migrationBuilder.DropColumn(
                name: "FileName",
                table: "Attachments");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6feb7ae5-bd2f-48ec-b004-fce99cc30f8e"),
                column: "Password",
                value: "AQAAAAEAACcQAAAAEFD7xA1l1kHSOk32m7BtNVbJlX3VXtt6zx4DGWJB4L/Gs/d4iACgDnWMdCWDlW/dng==");
        }
    }
}
