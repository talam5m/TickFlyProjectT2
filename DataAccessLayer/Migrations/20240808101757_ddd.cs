using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class ddd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsConfirmedFromClient",
                table: "Tickets",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6feb7ae5-bd2f-48ec-b004-fce99cc30f8e"),
                column: "Password",
                value: "AQAAAAEAACcQAAAAEFD7xA1l1kHSOk32m7BtNVbJlX3VXtt6zx4DGWJB4L/Gs/d4iACgDnWMdCWDlW/dng==");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsConfirmedFromClient",
                table: "Tickets");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6feb7ae5-bd2f-48ec-b004-fce99cc30f8e"),
                column: "Password",
                value: "AQAAAAEAACcQAAAAEJdfOLEkB6O/Zg5y5C6z+HyLQkkyp+9vT7tj0b7Gh+QLkT/1kZcjnAGD+BHroGeLhA==");
        }
    }
}
