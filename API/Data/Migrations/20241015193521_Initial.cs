using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace API.Data.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Gosts",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    Designation = table.Column<string>(type: "text", nullable: false),
                    FullName = table.Column<string>(type: "text", nullable: false),
                    CodeOKS = table.Column<string>(type: "text", nullable: true),
                    ActivityField = table.Column<string>(type: "text", nullable: true),
                    AcceptanceYear = table.Column<int>(type: "integer", nullable: true),
                    CommissionYear = table.Column<int>(type: "integer", nullable: true),
                    Author = table.Column<string>(type: "text", nullable: true),
                    AcceptedFirstTimeOrReplaced = table.Column<string>(type: "text", nullable: true),
                    Content = table.Column<string>(type: "text", nullable: true),
                    KeyWords = table.Column<string>(type: "text", nullable: true),
                    ApplicationArea = table.Column<string>(type: "text", nullable: true),
                    AdoptionLevel = table.Column<int>(type: "integer", nullable: true),
                    DocumentText = table.Column<string>(type: "text", nullable: true),
                    Changes = table.Column<string>(type: "text", nullable: true),
                    Amendments = table.Column<string>(type: "text", nullable: true),
                    Status = table.Column<int>(type: "integer", nullable: false),
                    Harmonization = table.Column<int>(type: "integer", nullable: true),
                    IndexedWordsCount = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gosts", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Indexes",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    WordId = table.Column<Guid>(type: "uuid", nullable: false),
                    GostId = table.Column<int>(type: "integer", nullable: false),
                    Frequency = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Indexes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Words",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Content = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Words", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Gosts_Id",
                table: "Gosts",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Indexes_Id",
                table: "Indexes",
                column: "Id",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Words_Id_Content",
                table: "Words",
                columns: new[] { "Id", "Content" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Gosts");

            migrationBuilder.DropTable(
                name: "Indexes");

            migrationBuilder.DropTable(
                name: "Words");
        }
    }
}
