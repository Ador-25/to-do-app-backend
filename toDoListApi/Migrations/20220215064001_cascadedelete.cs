using Microsoft.EntityFrameworkCore.Migrations;

namespace toDoListApi.Migrations
{
    public partial class cascadedelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubTask_Work_WorkId",
                table: "SubTask");

            migrationBuilder.AddForeignKey(
                name: "FK_SubTask_Work_WorkId",
                table: "SubTask",
                column: "WorkId",
                principalTable: "Work",
                principalColumn: "WorkId",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_SubTask_Work_WorkId",
                table: "SubTask");

            migrationBuilder.AddForeignKey(
                name: "FK_SubTask_Work_WorkId",
                table: "SubTask",
                column: "WorkId",
                principalTable: "Work",
                principalColumn: "WorkId",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
