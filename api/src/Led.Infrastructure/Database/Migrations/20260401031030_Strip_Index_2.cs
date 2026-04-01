using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Led.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Strip_Index_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_led_strip_device_id_dma_channel",
                schema: "led",
                table: "led_strip",
                columns: new[] { "device_id", "dma_channel" },
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_led_strip_tenant_id_name",
                schema: "led",
                table: "led_strip",
                columns: new[] { "tenant_id", "name" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_led_strip_device_id_dma_channel",
                schema: "led",
                table: "led_strip");

            migrationBuilder.DropIndex(
                name: "IX_led_strip_tenant_id_name",
                schema: "led",
                table: "led_strip");
        }
    }
}
