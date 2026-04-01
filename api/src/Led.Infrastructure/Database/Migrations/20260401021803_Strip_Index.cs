using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Led.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Strip_Index : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_led_strip_device_id_gpio_pin",
                schema: "led",
                table: "led_strip",
                columns: new[] { "device_id", "gpio_pin" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_led_strip_device_id_gpio_pin",
                schema: "led",
                table: "led_strip");
        }
    }
}
