using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Online_Recharge_WebApp.Data.Migrations
{
    public partial class compltedadded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Completed",
                table: "CustomerSupport",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Completed",
                table: "CustomerSupport");
        }
    }
}
