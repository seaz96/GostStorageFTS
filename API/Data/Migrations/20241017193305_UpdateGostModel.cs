using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateGostModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DocumentText",
                table: "Gosts");

            migrationBuilder.RenameColumn(
                name: "CodeOKS",
                table: "Gosts",
                newName: "CodeOks");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Gosts",
                type: "text",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "text");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "CodeOks",
                table: "Gosts",
                newName: "CodeOKS");

            migrationBuilder.AlterColumn<string>(
                name: "FullName",
                table: "Gosts",
                type: "text",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "text",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "DocumentText",
                table: "Gosts",
                type: "text",
                nullable: true);
        }
    }
}
