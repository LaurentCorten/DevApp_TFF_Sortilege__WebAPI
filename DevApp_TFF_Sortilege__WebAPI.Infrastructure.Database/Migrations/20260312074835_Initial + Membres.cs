using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DevApp_TFF_Sortilege__WebAPI.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class InitialMembres : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Membres",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Pseudo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    Email = table.Column<string>(type: "character varying(320)", maxLength: 320, nullable: false),
                    Hash_Mot = table.Column<string>(type: "character varying(200)", maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Membres", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "UK_Membres__Email",
                table: "Membres",
                column: "Email",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "UK_Membres__Pseudo",
                table: "Membres",
                column: "Pseudo",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Membres");
        }
    }
}
