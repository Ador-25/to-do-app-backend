using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace toDoListApi.Migrations
{
    public partial class dateTimeNameFixed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Name",
                table: "Work");

            migrationBuilder.AddColumn<DateTime>(
                name: "Date",
                table: "Work",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "EndTime",
                table: "SubTask",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "SubTask",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Date",
                table: "Work");

            migrationBuilder.DropColumn(
                name: "EndTime",
                table: "SubTask");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "SubTask");

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Work",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
