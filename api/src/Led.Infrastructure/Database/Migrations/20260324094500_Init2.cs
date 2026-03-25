using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Led.Infrastructure.Database.Migrations;

/// <inheritdoc />
public partial class Init2 : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "FirstName",
            schema: "led",
            table: "users",
            newName: "FirstName_Value");

        migrationBuilder.RenameColumn(
            name: "Email",
            schema: "led",
            table: "users",
            newName: "Email_Value");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.RenameColumn(
            name: "FirstName_Value",
            schema: "led",
            table: "users",
            newName: "FirstName");

        migrationBuilder.RenameColumn(
            name: "Email_Value",
            schema: "led",
            table: "users",
            newName: "Email");
    }
}
