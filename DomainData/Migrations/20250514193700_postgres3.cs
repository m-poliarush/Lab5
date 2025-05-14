using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuManager.Migrations
{
    /// <inheritdoc />
    public partial class postgres3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "BaseMenuItem",
                newName: "ItemType");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ItemType",
                table: "BaseMenuItem",
                newName: "Discriminator");
        }
    }
}
