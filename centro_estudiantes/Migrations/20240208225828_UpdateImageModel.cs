using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace centroestudiantes.Migrations
{
    /// <inheritdoc />
    public partial class UpdateImageModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoChoris",
                table: "TipoChoris");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Roles",
                table: "Roles");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Choripanes",
                table: "Choripanes");

            migrationBuilder.RenameTable(
                name: "Usuarios",
                newName: "Usuario");

            migrationBuilder.RenameTable(
                name: "TipoChoris",
                newName: "TipoChori");

            migrationBuilder.RenameTable(
                name: "Roles",
                newName: "Rol");

            migrationBuilder.RenameTable(
                name: "Choripanes",
                newName: "Choripan");

            migrationBuilder.RenameColumn(
                name: "Usuario",
                table: "Choripan",
                newName: "IdUsuario");

            migrationBuilder.RenameColumn(
                name: "TipoChori",
                table: "Choripan",
                newName: "IdTipoChori");

            migrationBuilder.RenameColumn(
                name: "Adereso",
                table: "Choripan",
                newName: "Aderezo");

            migrationBuilder.AddColumn<int>(
                name: "IdRol",
                table: "Usuario",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario",
                column: "IdUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoChori",
                table: "TipoChori",
                column: "IdTipoChori");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Rol",
                table: "Rol",
                column: "IdRol");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Choripan",
                table: "Choripan",
                column: "IdChoripan");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Usuario",
                table: "Usuario");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TipoChori",
                table: "TipoChori");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Rol",
                table: "Rol");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Choripan",
                table: "Choripan");

            migrationBuilder.DropColumn(
                name: "IdRol",
                table: "Usuario");

            migrationBuilder.RenameTable(
                name: "Usuario",
                newName: "Usuarios");

            migrationBuilder.RenameTable(
                name: "TipoChori",
                newName: "TipoChoris");

            migrationBuilder.RenameTable(
                name: "Rol",
                newName: "Roles");

            migrationBuilder.RenameTable(
                name: "Choripan",
                newName: "Choripanes");

            migrationBuilder.RenameColumn(
                name: "IdUsuario",
                table: "Choripanes",
                newName: "Usuario");

            migrationBuilder.RenameColumn(
                name: "IdTipoChori",
                table: "Choripanes",
                newName: "TipoChori");

            migrationBuilder.RenameColumn(
                name: "Aderezo",
                table: "Choripanes",
                newName: "Adereso");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Usuarios",
                table: "Usuarios",
                column: "IdUsuario");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TipoChoris",
                table: "TipoChoris",
                column: "IdTipoChori");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Roles",
                table: "Roles",
                column: "IdRol");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Choripanes",
                table: "Choripanes",
                column: "IdChoripan");
        }
    }
}
