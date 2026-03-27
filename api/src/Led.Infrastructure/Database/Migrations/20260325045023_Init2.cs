using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Led.Infrastructure.Database.Migrations
{
    /// <inheritdoc />
    public partial class Init2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TenantUser_tenant_tenant_id",
                schema: "led",
                table: "TenantUser");

            migrationBuilder.DropForeignKey(
                name: "FK_TenantUser_users_user_id",
                schema: "led",
                table: "TenantUser");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TenantUser",
                schema: "led",
                table: "TenantUser");

            migrationBuilder.RenameTable(
                name: "TenantUser",
                schema: "led",
                newName: "tenant_user",
                newSchema: "led");

            migrationBuilder.RenameColumn(
                name: "PasswordHash",
                schema: "led",
                table: "users",
                newName: "password_hash");

            migrationBuilder.RenameColumn(
                name: "ModifiedAtUtc",
                schema: "led",
                table: "tenant",
                newName: "modified_at_utc");

            migrationBuilder.RenameIndex(
                name: "IX_TenantUser_user_id",
                schema: "led",
                table: "tenant_user",
                newName: "IX_tenant_user_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tenant_user",
                schema: "led",
                table: "tenant_user",
                columns: new[] { "tenant_id", "user_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_tenant_user_tenant_tenant_id",
                schema: "led",
                table: "tenant_user",
                column: "tenant_id",
                principalSchema: "led",
                principalTable: "tenant",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tenant_user_users_user_id",
                schema: "led",
                table: "tenant_user",
                column: "user_id",
                principalSchema: "led",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tenant_user_tenant_tenant_id",
                schema: "led",
                table: "tenant_user");

            migrationBuilder.DropForeignKey(
                name: "FK_tenant_user_users_user_id",
                schema: "led",
                table: "tenant_user");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tenant_user",
                schema: "led",
                table: "tenant_user");

            migrationBuilder.RenameTable(
                name: "tenant_user",
                schema: "led",
                newName: "TenantUser",
                newSchema: "led");

            migrationBuilder.RenameColumn(
                name: "password_hash",
                schema: "led",
                table: "users",
                newName: "PasswordHash");

            migrationBuilder.RenameColumn(
                name: "modified_at_utc",
                schema: "led",
                table: "tenant",
                newName: "ModifiedAtUtc");

            migrationBuilder.RenameIndex(
                name: "IX_tenant_user_user_id",
                schema: "led",
                table: "TenantUser",
                newName: "IX_TenantUser_user_id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TenantUser",
                schema: "led",
                table: "TenantUser",
                columns: new[] { "tenant_id", "user_id" });

            migrationBuilder.AddForeignKey(
                name: "FK_TenantUser_tenant_tenant_id",
                schema: "led",
                table: "TenantUser",
                column: "tenant_id",
                principalSchema: "led",
                principalTable: "tenant",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TenantUser_users_user_id",
                schema: "led",
                table: "TenantUser",
                column: "user_id",
                principalSchema: "led",
                principalTable: "users",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
