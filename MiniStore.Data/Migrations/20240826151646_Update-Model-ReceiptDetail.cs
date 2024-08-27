using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MiniStore.Data.Migrations
{
    /// <inheritdoc />
    public partial class UpdateModelReceiptDetail : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Quantity",
                table: "Receipt Detail",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Quantity",
                table: "Receipt Detail");
        }
    }
}
