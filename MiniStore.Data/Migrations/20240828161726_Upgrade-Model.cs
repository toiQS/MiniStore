using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpgradeModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Employee_Shift_Shift Id",
                table: "Employee");

            migrationBuilder.DropForeignKey(
                name: "FK_Invoice_Coupon_Coupon Id",
                table: "Invoice");

            migrationBuilder.DropTable(
                name: "Calender");

            migrationBuilder.DropTable(
                name: "Coupon");

            migrationBuilder.DropTable(
                name: "Receipt Detail");

            migrationBuilder.DropTable(
                name: "Shift");

            migrationBuilder.DropTable(
                name: "Receipt");

            migrationBuilder.DropIndex(
                name: "IX_Invoice_Coupon Id",
                table: "Invoice");

            migrationBuilder.DropIndex(
                name: "IX_Employee_Shift Id",
                table: "Employee");

            migrationBuilder.DropColumn(
                name: "Coupon Id",
                table: "Invoice");

            migrationBuilder.DropColumn(
                name: "Shift Id",
                table: "Employee");

            migrationBuilder.AddColumn<string>(
                name: "Supplier Id",
                table: "Item",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Item_Supplier Id",
                table: "Item",
                column: "Supplier Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Supplier_Supplier Id",
                table: "Item",
                column: "Supplier Id",
                principalTable: "Supplier",
                principalColumn: "Supplier Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Item_Supplier_Supplier Id",
                table: "Item");

            migrationBuilder.DropIndex(
                name: "IX_Item_Supplier Id",
                table: "Item");

            migrationBuilder.DropColumn(
                name: "Supplier Id",
                table: "Item");

            migrationBuilder.AddColumn<string>(
                name: "Coupon Id",
                table: "Invoice",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Shift Id",
                table: "Employee",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateTable(
                name: "Coupon",
                columns: table => new
                {
                    CouponId = table.Column<string>(name: "Coupon Id", type: "nvarchar(450)", nullable: false),
                    StyleItemId = table.Column<string>(name: "Style Item Id", type: "nvarchar(450)", nullable: false),
                    CouponDescription = table.Column<string>(name: "Coupon Description", type: "nvarchar(max)", nullable: false),
                    CouponName = table.Column<string>(name: "Coupon Name", type: "nvarchar(max)", nullable: false),
                    EndAt = table.Column<DateTime>(name: "End At", type: "datetime2", nullable: false),
                    StartAt = table.Column<DateTime>(name: "Start At", type: "datetime2", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupon", x => x.CouponId);
                    table.ForeignKey(
                        name: "FK_Coupon_Style Item_Style Item Id",
                        column: x => x.StyleItemId,
                        principalTable: "Style Item",
                        principalColumn: "Style Item Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receipt",
                columns: table => new
                {
                    ReceiptId = table.Column<string>(name: "Receipt Id", type: "nvarchar(450)", nullable: false),
                    SupplierId = table.Column<string>(name: "Supplier Id", type: "nvarchar(450)", nullable: false),
                    CreateAt = table.Column<DateTime>(name: "Create At", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipt", x => x.ReceiptId);
                    table.ForeignKey(
                        name: "FK_Receipt_Supplier_Supplier Id",
                        column: x => x.SupplierId,
                        principalTable: "Supplier",
                        principalColumn: "Supplier Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Shift",
                columns: table => new
                {
                    ShiftId = table.Column<string>(name: "Shift Id", type: "nvarchar(450)", nullable: false),
                    ShiftName = table.Column<string>(name: "Shift Name ", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shift", x => x.ShiftId);
                });

            migrationBuilder.CreateTable(
                name: "Receipt Detail",
                columns: table => new
                {
                    ReceiptDetailId = table.Column<string>(name: "Receipt Detail Id", type: "nvarchar(450)", nullable: false),
                    ItemId = table.Column<string>(name: "Item Id", type: "nvarchar(450)", nullable: false),
                    ReceiptId = table.Column<string>(name: "Receipt Id", type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipt Detail", x => x.ReceiptDetailId);
                    table.ForeignKey(
                        name: "FK_Receipt Detail_Item_Item Id",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Item Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receipt Detail_Receipt_Receipt Id",
                        column: x => x.ReceiptId,
                        principalTable: "Receipt",
                        principalColumn: "Receipt Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Calender",
                columns: table => new
                {
                    CalenderId = table.Column<string>(name: "Calender Id", type: "nvarchar(450)", nullable: false),
                    Dayofweek = table.Column<int>(name: "Day of week", type: "int", nullable: false),
                    EndAt = table.Column<TimeOnly>(name: "End At", type: "time", nullable: false),
                    ShiftId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    StatAt = table.Column<TimeOnly>(name: "Stat At", type: "time", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calender", x => x.CalenderId);
                    table.ForeignKey(
                        name: "FK_Calender_Shift_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shift",
                        principalColumn: "Shift Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Coupon Id",
                table: "Invoice",
                column: "Coupon Id");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Shift Id",
                table: "Employee",
                column: "Shift Id");

            migrationBuilder.CreateIndex(
                name: "IX_Calender_ShiftId",
                table: "Calender",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Coupon_Style Item Id",
                table: "Coupon",
                column: "Style Item Id");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt_Supplier Id",
                table: "Receipt",
                column: "Supplier Id");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt Detail_Item Id",
                table: "Receipt Detail",
                column: "Item Id");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt Detail_Receipt Id",
                table: "Receipt Detail",
                column: "Receipt Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Employee_Shift_Shift Id",
                table: "Employee",
                column: "Shift Id",
                principalTable: "Shift",
                principalColumn: "Shift Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Coupon_Coupon Id",
                table: "Invoice",
                column: "Coupon Id",
                principalTable: "Coupon",
                principalColumn: "Coupon Id");
        }
    }
}
