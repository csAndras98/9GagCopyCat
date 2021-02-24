using Microsoft.EntityFrameworkCore.Migrations;

namespace PetProject.Data.Migrations
{
    public partial class fighterandopponentchanges : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Morale",
                table: "Fighters");

            migrationBuilder.RenameColumn(
                name: "inParty",
                table: "Fighters",
                newName: "InParty");

            migrationBuilder.AddColumn<int>(
                name: "Loot",
                table: "Opponents",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "MaxHealth",
                table: "Fighters",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Loot",
                table: "Opponents");

            migrationBuilder.DropColumn(
                name: "MaxHealth",
                table: "Fighters");

            migrationBuilder.RenameColumn(
                name: "InParty",
                table: "Fighters",
                newName: "inParty");

            migrationBuilder.AddColumn<int>(
                name: "Morale",
                table: "Fighters",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
