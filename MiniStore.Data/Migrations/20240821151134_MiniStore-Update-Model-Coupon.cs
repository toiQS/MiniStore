using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class MiniStoreUpdateModelCoupon : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Apply To Item",
                table: "Coupons",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Coupons_Apply To Item",
                table: "Coupons",
                column: "Apply To Item");

            migrationBuilder.AddForeignKey(
                name: "FK_Coupons_Style Item_Apply To Item",
                table: "Coupons",
                column: "Apply To Item",
                principalTable: "Style Item",
                principalColumn: "Style Item Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Coupons_Style Item_Apply To Item",
                table: "Coupons");

            migrationBuilder.DropIndex(
                name: "IX_Coupons_Apply To Item",
                table: "Coupons");

            migrationBuilder.DropColumn(
                name: "Apply To Item",
                table: "Coupons");
        }
    }
}
