using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace AuthenticationWithRazor.Migrations
{
    /// <inheritdoc />
    public partial class added_roles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "AspNetRoles",
                columns: new[] { "Id", "ConcurrencyStamp", "Name", "NormalizedName" },
                values: new object[,]
                {
                    { "22a7e9bf-ef27-454a-8187-b4dfd3012de0", "a33251b3-6e29-4770-ac2d-d5ee75ceae20", "admin", "admin" },
                    { "44bbfe29-cbd4-4aae-8e39-a10ea78038e2", "d03cf4d4-2318-4e96-b5cb-9c2ba36ee868", "employee", "employee" },
                    { "9bddd6a1-c01c-46e7-8d40-a9c28645a946", "23051b77-76e8-4f9a-b459-77028e20e496", "staff", "staff" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "22a7e9bf-ef27-454a-8187-b4dfd3012de0");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "44bbfe29-cbd4-4aae-8e39-a10ea78038e2");

            migrationBuilder.DeleteData(
                table: "AspNetRoles",
                keyColumn: "Id",
                keyValue: "9bddd6a1-c01c-46e7-8d40-a9c28645a946");
        }
    }
}
