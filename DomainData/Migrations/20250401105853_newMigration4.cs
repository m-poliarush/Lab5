using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuManager.Migrations
{
    /// <inheritdoc />
    public partial class newMigration4 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseMenuItem_BaseMenuItem_BaseMenuItemID",
                table: "BaseMenuItem");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseMenuItem_BaseMenuItem_ComplexDishID",
                table: "BaseMenuItem");

            migrationBuilder.DropIndex(
                name: "IX_BaseMenuItem_BaseMenuItemID",
                table: "BaseMenuItem");

            migrationBuilder.DropIndex(
                name: "IX_BaseMenuItem_ComplexDishID",
                table: "BaseMenuItem");

            migrationBuilder.DropColumn(
                name: "BaseMenuItemID",
                table: "BaseMenuItem");

            migrationBuilder.DropColumn(
                name: "ComplexDishID",
                table: "BaseMenuItem");

            migrationBuilder.CreateTable(
                name: "ComplexDishDish",
                columns: table => new
                {
                    DishListID = table.Column<int>(type: "INTEGER", nullable: false),
                    complexDishesID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ComplexDishDish", x => new { x.DishListID, x.complexDishesID });
                    table.ForeignKey(
                        name: "FK_ComplexDishDish_BaseMenuItem_DishListID",
                        column: x => x.DishListID,
                        principalTable: "BaseMenuItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ComplexDishDish_BaseMenuItem_complexDishesID",
                        column: x => x.complexDishesID,
                        principalTable: "BaseMenuItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ComplexDishDish_complexDishesID",
                table: "ComplexDishDish",
                column: "complexDishesID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ComplexDishDish");

            migrationBuilder.AddColumn<int>(
                name: "BaseMenuItemID",
                table: "BaseMenuItem",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "ComplexDishID",
                table: "BaseMenuItem",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseMenuItem_BaseMenuItemID",
                table: "BaseMenuItem",
                column: "BaseMenuItemID");

            migrationBuilder.CreateIndex(
                name: "IX_BaseMenuItem_ComplexDishID",
                table: "BaseMenuItem",
                column: "ComplexDishID");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseMenuItem_BaseMenuItem_BaseMenuItemID",
                table: "BaseMenuItem",
                column: "BaseMenuItemID",
                principalTable: "BaseMenuItem",
                principalColumn: "ID");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseMenuItem_BaseMenuItem_ComplexDishID",
                table: "BaseMenuItem",
                column: "ComplexDishID",
                principalTable: "BaseMenuItem",
                principalColumn: "ID");
        }
    }
}
