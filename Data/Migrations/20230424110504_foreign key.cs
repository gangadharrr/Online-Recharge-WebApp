using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Recharge_WebApp.Data.Migrations
{
    public partial class foreignkey : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "CustomerSupport",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_CustomerSupport_UserId",
                table: "CustomerSupport",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_CustomerSupport_AspNetUsers_UserId",
                table: "CustomerSupport",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CustomerSupport_AspNetUsers_UserId",
                table: "CustomerSupport");

            migrationBuilder.DropIndex(
                name: "IX_CustomerSupport_UserId",
                table: "CustomerSupport");

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "CustomerSupport");
        }
    }
}
