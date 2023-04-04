using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NetCoreApp.Data.Migrations
{
    public partial class addNumTeamsToStart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "numTeamsToStart",
                table: "League",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "numTeamsToStart",
                table: "League");
        }
    }
}
