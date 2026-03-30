using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Led.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Update_LedStripType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                schema: "led",
                table: "led_strip_type",
                columns: new[] { "id", "name" },
                values: new object[] { (short)1, "SK6812 RGBW" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                schema: "led",
                table: "led_strip_type",
                keyColumn: "id",
                keyValue: (short)1);
        }
    }
}
