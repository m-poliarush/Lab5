using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuManager.Migrations
{
    /// <inheritdoc />
    public partial class newMigration5 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "category",
                table: "BaseMenuItem",
                type: "INTEGER",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "category",
                table: "BaseMenuItem");
        }
    }
}
