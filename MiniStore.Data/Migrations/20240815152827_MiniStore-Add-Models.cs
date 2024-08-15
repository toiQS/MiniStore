using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class MiniStoreAddModels : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Coupons",
                columns: table => new
                {
                    CouponId = table.Column<string>(name: "Coupon Id", type: "nvarchar(450)", nullable: false),
                    CouponName = table.Column<string>(name: "Coupon Name", type: "nvarchar(max)", nullable: false),
                    CouponDescription = table.Column<string>(name: "Coupon Description", type: "nvarchar(max)", nullable: false),
                    Value = table.Column<float>(type: "real", nullable: false),
                    Unit = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StartAt = table.Column<DateTime>(name: "Start At", type: "datetime2", nullable: false),
                    EndAt = table.Column<DateTime>(name: "End At", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Coupons", x => x.CouponId);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    CustomerId = table.Column<string>(name: "Customer Id", type: "nvarchar(450)", nullable: false),
                    CustomerName = table.Column<string>(name: "Customer Name", type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.CustomerId);
                });

            migrationBuilder.CreateTable(
                name: "Shifts",
                columns: table => new
                {
                    ShiftId = table.Column<string>(name: "Shift Id", type: "nvarchar(450)", nullable: false),
                    ShiftName = table.Column<string>(name: "Shift Name ", type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shifts", x => x.ShiftId);
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
                name: "Suppliers",
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
                    table.PrimaryKey("PK_Suppliers", x => x.SupplierId);
                });

            migrationBuilder.CreateTable(
                name: "Calenders",
                columns: table => new
                {
                    CalenderId = table.Column<string>(name: "Calender Id", type: "nvarchar(450)", nullable: false),
                    Dayofweek = table.Column<int>(name: "Day of week", type: "int", nullable: false),
                    StatAt = table.Column<TimeOnly>(name: "Stat At", type: "time", nullable: false),
                    EndAt = table.Column<DateTime>(name: "End At", type: "datetime2", nullable: false),
                    ShiftId = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Calenders", x => x.CalenderId);
                    table.ForeignKey(
                        name: "FK_Calenders_Shifts_ShiftId",
                        column: x => x.ShiftId,
                        principalTable: "Shifts",
                        principalColumn: "Shift Id");
                });

            migrationBuilder.CreateTable(
                name: "Items",
                columns: table => new
                {
                    ItemId = table.Column<string>(name: "Item Id", type: "nvarchar(450)", nullable: false),
                    ItemName = table.Column<string>(name: "Item Name", type: "nvarchar(max)", nullable: false),
                    Quantity = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    StyleItemId = table.Column<string>(name: "Style Item Id", type: "nvarchar(450)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Items", x => x.ItemId);
                    table.ForeignKey(
                        name: "FK_Items_Style Item_Style Item Id",
                        column: x => x.StyleItemId,
                        principalTable: "Style Item",
                        principalColumn: "Style Item Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Receipts",
                columns: table => new
                {
                    ReceiptId = table.Column<string>(name: "Receipt Id", type: "nvarchar(450)", nullable: false),
                    SupplierId = table.Column<string>(name: "Supplier Id", type: "nvarchar(450)", nullable: false),
                    CreateAt = table.Column<DateTime>(name: "Create At", type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Receipts", x => x.ReceiptId);
                    table.ForeignKey(
                        name: "FK_Receipts_Suppliers_Supplier Id",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Supplier Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Employees",
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
                    CalenderId = table.Column<string>(name: "Calender Id", type: "nvarchar(450)", nullable: false),
                    Status = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Employees", x => x.EmployeeId);
                    table.ForeignKey(
                        name: "FK_Employees_Calenders_Calender Id",
                        column: x => x.CalenderId,
                        principalTable: "Calenders",
                        principalColumn: "Calender Id",
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
                        name: "FK_Receipt Detail_Items_Item Id",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Item Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Receipt Detail_Receipts_Receipt Id",
                        column: x => x.ReceiptId,
                        principalTable: "Receipts",
                        principalColumn: "Receipt Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Invoices",
                columns: table => new
                {
                    InvoiceId = table.Column<string>(name: "Invoice Id", type: "nvarchar(450)", nullable: false),
                    EmployeeId = table.Column<string>(name: "Employee Id", type: "nvarchar(450)", nullable: false),
                    CustomerId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    CouponId = table.Column<string>(name: "Coupon Id", type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Invoices", x => x.InvoiceId);
                    table.ForeignKey(
                        name: "FK_Invoices_Coupons_Coupon Id",
                        column: x => x.CouponId,
                        principalTable: "Coupons",
                        principalColumn: "Coupon Id");
                    table.ForeignKey(
                        name: "FK_Invoices_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Customer Id");
                    table.ForeignKey(
                        name: "FK_Invoices_Employees_Employee Id",
                        column: x => x.EmployeeId,
                        principalTable: "Employees",
                        principalColumn: "Employee Id",
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
                        name: "FK_Invoice Detail_Invoices_Invoice Id",
                        column: x => x.InvoiceId,
                        principalTable: "Invoices",
                        principalColumn: "Invoice Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Invoice Detail_Items_Item Id",
                        column: x => x.ItemId,
                        principalTable: "Items",
                        principalColumn: "Item Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Calenders_ShiftId",
                table: "Calenders",
                column: "ShiftId");

            migrationBuilder.CreateIndex(
                name: "IX_Employees_Calender Id",
                table: "Employees",
                column: "Calender Id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice Detail_Invoice Id",
                table: "Invoice Detail",
                column: "Invoice Id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoice Detail_Item Id",
                table: "Invoice Detail",
                column: "Item Id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_Coupon Id",
                table: "Invoices",
                column: "Coupon Id");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_CustomerId",
                table: "Invoices",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_Employee Id",
                table: "Invoices",
                column: "Employee Id");

            migrationBuilder.CreateIndex(
                name: "IX_Items_Style Item Id",
                table: "Items",
                column: "Style Item Id");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt Detail_Item Id",
                table: "Receipt Detail",
                column: "Item Id");

            migrationBuilder.CreateIndex(
                name: "IX_Receipt Detail_Receipt Id",
                table: "Receipt Detail",
                column: "Receipt Id");

            migrationBuilder.CreateIndex(
                name: "IX_Receipts_Supplier Id",
                table: "Receipts",
                column: "Supplier Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Invoice Detail");

            migrationBuilder.DropTable(
                name: "Receipt Detail");

            migrationBuilder.DropTable(
                name: "Invoices");

            migrationBuilder.DropTable(
                name: "Items");

            migrationBuilder.DropTable(
                name: "Receipts");

            migrationBuilder.DropTable(
                name: "Coupons");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Employees");

            migrationBuilder.DropTable(
                name: "Style Item");

            migrationBuilder.DropTable(
                name: "Suppliers");

            migrationBuilder.DropTable(
                name: "Calenders");

            migrationBuilder.DropTable(
                name: "Shifts");
        }
    }
}
