using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuManager.Migrations
{
    /// <inheritdoc />
    public partial class newMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseMenuItem_DailyMenus_DailyMenuDayID",
                table: "BaseMenuItem");

            migrationBuilder.DropForeignKey(
                name: "FK_BaseMenuItem_Orders_OrderID",
                table: "BaseMenuItem");

            migrationBuilder.DropIndex(
                name: "IX_BaseMenuItem_DailyMenuDayID",
                table: "BaseMenuItem");

            migrationBuilder.DropColumn(
                name: "DailyMenuDayID",
                table: "BaseMenuItem");

            migrationBuilder.RenameColumn(
                name: "OrderID",
                table: "BaseMenuItem",
                newName: "BaseMenuItemID");

            migrationBuilder.RenameIndex(
                name: "IX_BaseMenuItem_OrderID",
                table: "BaseMenuItem",
                newName: "IX_BaseMenuItem_BaseMenuItemID");

            migrationBuilder.CreateTable(
                name: "BaseMenuItemDailyMenu",
                columns: table => new
                {
                    DishesID = table.Column<int>(type: "INTEGER", nullable: false),
                    menusDayID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseMenuItemDailyMenu", x => new { x.DishesID, x.menusDayID });
                    table.ForeignKey(
                        name: "FK_BaseMenuItemDailyMenu_BaseMenuItem_DishesID",
                        column: x => x.DishesID,
                        principalTable: "BaseMenuItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseMenuItemDailyMenu_DailyMenus_menusDayID",
                        column: x => x.menusDayID,
                        principalTable: "DailyMenus",
                        principalColumn: "DayID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "BaseMenuItemOrder",
                columns: table => new
                {
                    dishesID = table.Column<int>(type: "INTEGER", nullable: false),
                    ordersOrderID = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseMenuItemOrder", x => new { x.dishesID, x.ordersOrderID });
                    table.ForeignKey(
                        name: "FK_BaseMenuItemOrder_BaseMenuItem_dishesID",
                        column: x => x.dishesID,
                        principalTable: "BaseMenuItem",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_BaseMenuItemOrder_Orders_ordersOrderID",
                        column: x => x.ordersOrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseMenuItemDailyMenu_menusDayID",
                table: "BaseMenuItemDailyMenu",
                column: "menusDayID");

            migrationBuilder.CreateIndex(
                name: "IX_BaseMenuItemOrder_ordersOrderID",
                table: "BaseMenuItemOrder",
                column: "ordersOrderID");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseMenuItem_BaseMenuItem_BaseMenuItemID",
                table: "BaseMenuItem",
                column: "BaseMenuItemID",
                principalTable: "BaseMenuItem",
                principalColumn: "ID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BaseMenuItem_BaseMenuItem_BaseMenuItemID",
                table: "BaseMenuItem");

            migrationBuilder.DropTable(
                name: "BaseMenuItemDailyMenu");

            migrationBuilder.DropTable(
                name: "BaseMenuItemOrder");

            migrationBuilder.RenameColumn(
                name: "BaseMenuItemID",
                table: "BaseMenuItem",
                newName: "OrderID");

            migrationBuilder.RenameIndex(
                name: "IX_BaseMenuItem_BaseMenuItemID",
                table: "BaseMenuItem",
                newName: "IX_BaseMenuItem_OrderID");

            migrationBuilder.AddColumn<int>(
                name: "DailyMenuDayID",
                table: "BaseMenuItem",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_BaseMenuItem_DailyMenuDayID",
                table: "BaseMenuItem",
                column: "DailyMenuDayID");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseMenuItem_DailyMenus_DailyMenuDayID",
                table: "BaseMenuItem",
                column: "DailyMenuDayID",
                principalTable: "DailyMenus",
                principalColumn: "DayID");

            migrationBuilder.AddForeignKey(
                name: "FK_BaseMenuItem_Orders_OrderID",
                table: "BaseMenuItem",
                column: "OrderID",
                principalTable: "Orders",
                principalColumn: "OrderID");
        }
    }
}
