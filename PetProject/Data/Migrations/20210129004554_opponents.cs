using Microsoft.EntityFrameworkCore.Migrations;

namespace PetProject.Data.Migrations
{
    public partial class opponents : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Opponents",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Image = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true),
                    Level = table.Column<int>(nullable: false),
                    Health = table.Column<int>(nullable: false),
                    Accuracy = table.Column<int>(nullable: false),
                    Power = table.Column<int>(nullable: false),
                    Initiative = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Opponents", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Opponents");
        }
    }
}
