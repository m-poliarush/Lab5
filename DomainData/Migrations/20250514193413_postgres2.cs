using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuManager.Migrations
{
    /// <inheritdoc />
    public partial class postgres2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseMenuItemDailyMenu_BaseMenuItem_DishesID",
                table: "BaseMenuItemDailyMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseMenuItemDailyMenu_DailyMenus_menusDayID",
                table: "BaseMenuItemDailyMenu");

            migrationBuilder.RenameColumn(
                name: "ItemType",
                table: "BaseMenuItem",
                newName: "Discriminator");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseMenuItemDailyMenu_BaseMenuItem",
                table: "BaseMenuItemDailyMenu",
                column: "DishesID",
                principalTable: "BaseMenuItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseMenuItemDailyMenu_DailyMenu",
                table: "BaseMenuItemDailyMenu",
                column: "menusDayID",
                principalTable: "DailyMenus",
                principalColumn: "DayID",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseMenuItemDailyMenu_BaseMenuItem",
                table: "BaseMenuItemDailyMenu");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseMenuItemDailyMenu_DailyMenu",
                table: "BaseMenuItemDailyMenu");

            migrationBuilder.RenameColumn(
                name: "Discriminator",
                table: "BaseMenuItem",
                newName: "ItemType");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseMenuItemDailyMenu_BaseMenuItem_DishesID",
                table: "BaseMenuItemDailyMenu",
                column: "DishesID",
                principalTable: "BaseMenuItem",
                principalColumn: "ID",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_BaseMenuItemDailyMenu_DailyMenus_menusDayID",
                table: "BaseMenuItemDailyMenu",
                column: "menusDayID",
                principalTable: "DailyMenus",
                principalColumn: "DayID",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
