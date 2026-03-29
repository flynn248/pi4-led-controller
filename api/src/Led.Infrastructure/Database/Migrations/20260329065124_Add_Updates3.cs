using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Led.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Add_Updates3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_device_tenant_id",
                schema: "led",
                table: "device");

            migrationBuilder.CreateIndex(
                name: "IX_device_tenant_id_ip_address",
                schema: "led",
                table: "device",
                columns: new[] { "tenant_id", "ip_address" },
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_device_tenant_id_ip_address",
                schema: "led",
                table: "device");

            migrationBuilder.CreateIndex(
                name: "IX_device_tenant_id",
                schema: "led",
                table: "device",
                column: "tenant_id");
        }
    }
}
