using Microsoft.EntityFrameworkCore.Migrations;

namespace PetProject.Data.Migrations
{
    public partial class partystatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "inParty",
                table: "Fighters",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "inParty",
                table: "Fighters");
        }
    }
}
