using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    public partial class _12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketComments_Tickets_TicketId",
                table: "TicketComments");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketComments_Users_UserId",
                table: "TicketComments");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "TicketComments",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"),
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier",
                oldNullable: true);

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6feb7ae5-bd2f-48ec-b004-fce99cc30f8e"),
                column: "Password",
                value: "AQAAAAEAACcQAAAAEJdfOLEkB6O/Zg5y5C6z+HyLQkkyp+9vT7tj0b7Gh+QLkT/1kZcjnAGD+BHroGeLhA==");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketComments_Tickets_TicketId",
                table: "TicketComments",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketComments_Users_UserId",
                table: "TicketComments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TicketComments_Tickets_TicketId",
                table: "TicketComments");

            migrationBuilder.DropForeignKey(
                name: "FK_TicketComments_Users_UserId",
                table: "TicketComments");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                table: "TicketComments",
                type: "uniqueidentifier",
                nullable: true,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.UpdateData(
                table: "Users",
                keyColumn: "Id",
                keyValue: new Guid("6feb7ae5-bd2f-48ec-b004-fce99cc30f8e"),
                column: "Password",
                value: "AQAAAAEAACcQAAAAEP+I0pEFEEMpI6QkNUiOtQPCKv6ZQPPzogghfG8l04e0rr1J6Hb8R2+p0Q/8ebFYkw==");

            migrationBuilder.AddForeignKey(
                name: "FK_TicketComments_Tickets_TicketId",
                table: "TicketComments",
                column: "TicketId",
                principalTable: "Tickets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TicketComments_Users_UserId",
                table: "TicketComments",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
