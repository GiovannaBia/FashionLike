using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace FashionLike_AccesoDatos.Migrations
{
    /// <inheritdoc />
    public partial class tablas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Discriminator",
                table: "AspNetUsers",
                type: "nvarchar(21)",
                maxLength: 21,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "NombreCompleto",
                table: "AspNetUsers",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "MeGustaPosteo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PosteoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MeGustaPosteo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MeGustaPosteo_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MeGustaPosteo_Posteos_PosteoId",
                        column: x => x.PosteoId,
                        principalTable: "Posteos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "NoMeGustaPosteo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UsuarioId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PosteoId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NoMeGustaPosteo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NoMeGustaPosteo_AspNetUsers_UsuarioId",
                        column: x => x.UsuarioId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_NoMeGustaPosteo_Posteos_PosteoId",
                        column: x => x.PosteoId,
                        principalTable: "Posteos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PosteoUsuarioAplicacion",
                columns: table => new
                {
                    MeGustaPosteoId = table.Column<int>(type: "int", nullable: false),
                    UsuariosQueLeGustanId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PosteoUsuarioAplicacion", x => new { x.MeGustaPosteoId, x.UsuariosQueLeGustanId });
                    table.ForeignKey(
                        name: "FK_PosteoUsuarioAplicacion_AspNetUsers_UsuariosQueLeGustanId",
                        column: x => x.UsuariosQueLeGustanId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PosteoUsuarioAplicacion_Posteos_MeGustaPosteoId",
                        column: x => x.MeGustaPosteoId,
                        principalTable: "Posteos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "PosteoUsuarioAplicacion1",
                columns: table => new
                {
                    NoMegustaPosteoId = table.Column<int>(type: "int", nullable: false),
                    UsuariosQueNoLeGustanId = table.Column<string>(type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PosteoUsuarioAplicacion1", x => new { x.NoMegustaPosteoId, x.UsuariosQueNoLeGustanId });
                    table.ForeignKey(
                        name: "FK_PosteoUsuarioAplicacion1_AspNetUsers_UsuariosQueNoLeGustanId",
                        column: x => x.UsuariosQueNoLeGustanId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_PosteoUsuarioAplicacion1_Posteos_NoMegustaPosteoId",
                        column: x => x.NoMegustaPosteoId,
                        principalTable: "Posteos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_MeGustaPosteo_PosteoId",
                table: "MeGustaPosteo",
                column: "PosteoId");

            migrationBuilder.CreateIndex(
                name: "IX_MeGustaPosteo_UsuarioId",
                table: "MeGustaPosteo",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_NoMeGustaPosteo_PosteoId",
                table: "NoMeGustaPosteo",
                column: "PosteoId");

            migrationBuilder.CreateIndex(
                name: "IX_NoMeGustaPosteo_UsuarioId",
                table: "NoMeGustaPosteo",
                column: "UsuarioId");

            migrationBuilder.CreateIndex(
                name: "IX_PosteoUsuarioAplicacion_UsuariosQueLeGustanId",
                table: "PosteoUsuarioAplicacion",
                column: "UsuariosQueLeGustanId");

            migrationBuilder.CreateIndex(
                name: "IX_PosteoUsuarioAplicacion1_UsuariosQueNoLeGustanId",
                table: "PosteoUsuarioAplicacion1",
                column: "UsuariosQueNoLeGustanId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MeGustaPosteo");

            migrationBuilder.DropTable(
                name: "NoMeGustaPosteo");

            migrationBuilder.DropTable(
                name: "PosteoUsuarioAplicacion");

            migrationBuilder.DropTable(
                name: "PosteoUsuarioAplicacion1");

            migrationBuilder.DropColumn(
                name: "Discriminator",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NombreCompleto",
                table: "AspNetUsers");
        }
    }
}
