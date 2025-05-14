using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MenuManager.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "DailyMenus",
                columns: table => new
                {
                    DayID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DayOfWeek = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_DailyMenus", x => x.DayID);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    OrderID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TotalCost = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.OrderID);
                });

            migrationBuilder.CreateTable(
                name: "BaseMenuItem",
                columns: table => new
                {
                    ID = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: false),
                    Price = table.Column<int>(type: "INTEGER", nullable: false),
                    DailyMenuDayID = table.Column<int>(type: "INTEGER", nullable: true),
                    ItemType = table.Column<string>(type: "TEXT", maxLength: 13, nullable: false),
                    OrderID = table.Column<int>(type: "INTEGER", nullable: true),
                    ComplexDishID = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BaseMenuItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_BaseMenuItem_BaseMenuItem_ComplexDishID",
                        column: x => x.ComplexDishID,
                        principalTable: "BaseMenuItem",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_BaseMenuItem_DailyMenus_DailyMenuDayID",
                        column: x => x.DailyMenuDayID,
                        principalTable: "DailyMenus",
                        principalColumn: "DayID");
                    table.ForeignKey(
                        name: "FK_BaseMenuItem_Orders_OrderID",
                        column: x => x.OrderID,
                        principalTable: "Orders",
                        principalColumn: "OrderID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_BaseMenuItem_ComplexDishID",
                table: "BaseMenuItem",
                column: "ComplexDishID");

            migrationBuilder.CreateIndex(
                name: "IX_BaseMenuItem_DailyMenuDayID",
                table: "BaseMenuItem",
                column: "DailyMenuDayID");

            migrationBuilder.CreateIndex(
                name: "IX_BaseMenuItem_OrderID",
                table: "BaseMenuItem",
                column: "OrderID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BaseMenuItem");

            migrationBuilder.DropTable(
                name: "DailyMenus");

            migrationBuilder.DropTable(
                name: "Orders");
        }
    }
}
