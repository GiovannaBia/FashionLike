using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionLike_AccesoDatos.Migrations
{
    public partial class votosPosiNega : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Votos",
                table: "Posteos",
                newName: "VotosPositivos");

            migrationBuilder.AddColumn<int>(
                name: "VotosNegativos",
                table: "Posteos",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "VotosNegativos",
                table: "Posteos");

            migrationBuilder.RenameColumn(
                name: "VotosPositivos",
                table: "Posteos",
                newName: "Votos");
        }
    }
}
