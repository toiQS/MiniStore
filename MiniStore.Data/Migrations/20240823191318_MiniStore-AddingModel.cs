using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class MiniStoreAddingModel : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Customer",
                columns: table => new
                {
                    CustomerId = table.Column<string>(name: "Customer Id", type: "nvarchar(450)", nullable: false),
                    CustomerName = table.Column<string>(name: "Customer Name", type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customer", x => x.CustomerId);
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
                name: "Style Item",
                columns: table => new
                {
                    StyleItemId = table.Column<string>(name: "Style Item Id", type: "nvarchar(450)", nullable: false),
                    StyleItemName = table.Column<string>(name: "Style Item Name", type: "nvarchar(max)", nullable: false),
                    StyleItemDescription = table.Column<string>(name: "Style Item Description", type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Style Item", x => x.StyleItemId);
                });

            migrationBuilder.CreateTable(
                name: "Supplier",
                columns: table => new
                {
                    SupplierId = table.Column<string>(name: "Supplier Id", type: "nvarchar(450)", nullable: false),
                    SupplierName = table.Column<string>(name: "Supplier Name", type: "nvarchar(max)", nullable: false),
                    SupplierPhone = table.Column<string>(name: "Supplier Phone", type: "nvarchar(max)", nullable: false),
                    SupplierEmail = table.Column<string>(name: "Supplier Email", type: "nvarchar(max)", nullable: false),
                    SupplerAddress = table.Column<string>(name: "Suppler Address", type: "nvarchar(max)", nullable: false),
                    SupplierStatus = table.Column<bool>(name: "Supplier Status", type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Supplier", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "Calender",
                columns: table => new
                {
                    CalenderId = table.Column<string>(name: "Calender Id", type: "nvarchar(450)", nullable: false),
                    Dayofweek = table.Column<int>(name: "Day of week", type: "int", nullable: false),
                    StatAt = table.Column<TimeOnly>(name: "Stat At", type: "time", nullable: false),
                    EndAt = table.Column<TimeOnly>(name: "End At", type: "time", nullable: false),
                    ShiftId = table.Column<string>(name: "Shift Id", type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calender", x => x.CalenderId);
                    table.ForeignKey(
                        name: "FK_Calender_Shift_Shift Id",
                        column: x => x.ShiftId,
                        principalTable: "Shift",
                        principalColumn: "Shift Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employee",
                columns: table => new
                {
                    EmployeeId = table.Column<string>(name: "Employee Id", type: "nvarchar(450)", nullable: false),
                    FirstName = table.Column<string>(name: "First Name", type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(name: "Last Name", type: "nvarchar(max)", nullable: false),
                    MiddleName = table.Column<string>(name: "Middle Name", type: "nvarchar(max)", nullable: true),
                    EmployeeName = table.Column<string>(name: "Employee Name", type: "nvarchar(max)", nullable: false),
                    CCCD = table.Column<int>(type: "int", nullable: false),
                    EmployeeEmail = table.Column<string>(name: "Employee Email", type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ShiftId = table.Column<string>(name: "Shift Id", type: "nvarchar(450)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employee", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employee_Shift_Shift Id",
                        column: x => x.ShiftId,
                        principalTable: "Shift",
                        principalColumn: "Shift Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Coupon",
                columns: table => new
                {
                    CouponId = table.Column<string>(name: "Coupon Id", type: "nvarchar(450)", nullable: false),
                    CouponName = table.Column<string>(name: "Coupon Name", type: "nvarchar(max)", nullable: false),
                    CouponDescription = table.Column<string>(name: "Coupon Description", type: "nvarchar(max)", nullable: false),
                    StyleItemId = table.Column<string>(name: "Style Item Id", type: "nvarchar(450)", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartAt = table.Column<DateTime>(name: "Start At", type: "datetime2", nullable: false),
                    EndAt = table.Column<DateTime>(name: "End At", type: "datetime2", nullable: false)
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
                name: "Item",
                columns: table => new
                {
                    ItemId = table.Column<string>(name: "Item Id", type: "nvarchar(450)", nullable: false),
                    ItemName = table.Column<string>(name: "Item Name", type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false),
                    StyleItemId = table.Column<string>(name: "Style Item Id", type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Item_Style Item_Style Item Id",
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
                name: "Invoice",
                columns: table => new
                {
                    InvoiceId = table.Column<string>(name: "Invoice Id", type: "nvarchar(450)", nullable: false),
                    EmployeeId = table.Column<string>(name: "Employee Id", type: "nvarchar(450)", nullable: false),
                    CustomerId = table.Column<string>(name: "Customer Id", type: "nvarchar(450)", nullable: false),
                    CouponId = table.Column<string>(name: "Coupon Id", type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoice_Coupon_Coupon Id",
                        column: x => x.CouponId,
                        principalTable: "Coupon",
                        principalColumn: "Coupon Id");
                    table.ForeignKey(
                        name: "FK_Invoice_Customer_Customer Id",
                        column: x => x.CustomerId,
                        principalTable: "Customer",
                        principalColumn: "Customer Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoice_Employee_Employee Id",
                        column: x => x.EmployeeId,
                        principalTable: "Employee",
                        principalColumn: "Employee Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receipt Detail",
                columns: table => new
                {
                    ReceiptDetailId = table.Column<string>(name: "Receipt Detail Id", type: "nvarchar(450)", nullable: false),
                    ReceiptId = table.Column<string>(name: "Receipt Id", type: "nvarchar(450)", nullable: false),
                    ItemId = table.Column<string>(name: "Item Id", type: "nvarchar(450)", nullable: false)
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
                name: "Invoice Detail",
                columns: table => new
                {
                    InvoiceDetailId = table.Column<string>(name: "Invoice Detail Id", type: "nvarchar(450)", nullable: false),
                    ItemId = table.Column<string>(name: "Item Id", type: "nvarchar(450)", nullable: false),
                    InvoiceId = table.Column<string>(name: "Invoice Id", type: "nvarchar(450)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoice Detail", x => x.InvoiceDetailId);
                    table.ForeignKey(
                        name: "FK_Invoice Detail_Invoice_Invoice Id",
                        column: x => x.InvoiceId,
                        principalTable: "Invoice",
                        principalColumn: "Invoice Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoice Detail_Item_Item Id",
                        column: x => x.ItemId,
                        principalTable: "Item",
                        principalColumn: "Item Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calender_Shift Id",
                table: "Calender",
                column: "Shift Id");

            migrationBuilder.CreateIndex(
                name: "IX_Coupon_Style Item Id",
                table: "Coupon",
                column: "Style Item Id");

            migrationBuilder.CreateIndex(
                name: "IX_Employee_Shift Id",
                table: "Employee",
                column: "Shift Id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Coupon Id",
                table: "Invoice",
                column: "Coupon Id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Customer Id",
                table: "Invoice",
                column: "Customer Id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice_Employee Id",
                table: "Invoice",
                column: "Employee Id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice Detail_Invoice Id",
                table: "Invoice Detail",
                column: "Invoice Id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice Detail_Item Id",
                table: "Invoice Detail",
                column: "Item Id");

            migrationBuilder.CreateIndex(
                name: "IX_Item_Style Item Id",
                table: "Item",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Calender");

            migrationBuilder.DropTable(
                name: "Invoice Detail");

            migrationBuilder.DropTable(
                name: "Receipt Detail");

            migrationBuilder.DropTable(
                name: "Invoice");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Receipt");

            migrationBuilder.DropTable(
                name: "Coupon");

            migrationBuilder.DropTable(
                name: "Customer");

            migrationBuilder.DropTable(
                name: "Employee");

            migrationBuilder.DropTable(
                name: "Supplier");

            migrationBuilder.DropTable(
                name: "Style Item");

            migrationBuilder.DropTable(
                name: "Shift");
        }
    }
}
