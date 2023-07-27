using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ContactManager.API.Migrations
{
    public partial class DataSeed : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Emergency", "Favorite", "FirstName", "LastName", "Note" },
                values: new object[] { 1, false, false, "Jeremy", "Flores", "This is a Note" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Emergency", "Favorite", "FirstName", "LastName", "Note" },
                values: new object[] { 2, false, false, "Lirae", "Data", "This is a Note 2" });

            migrationBuilder.InsertData(
                table: "Contacts",
                columns: new[] { "Id", "Emergency", "Favorite", "FirstName", "LastName", "Note" },
                values: new object[] { 3, false, false, "Charis Arlie", "Baclayon", null });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "AddressDetails", "ContactId", "Type" },
                values: new object[] { 1, "This is my address 1", 1, "Home" });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "AddressDetails", "ContactId", "Type" },
                values: new object[] { 2, "This is Fullscale Address", 1, "Work" });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "AddressDetails", "ContactId", "Type" },
                values: new object[] { 3, "This is my address", 2, "Home" });

            migrationBuilder.InsertData(
                table: "Address",
                columns: new[] { "Id", "AddressDetails", "ContactId", "Type" },
                values: new object[] { 4, "This is Fullscale Address", 3, "Work" });

            migrationBuilder.InsertData(
                table: "Email",
                columns: new[] { "Id", "ContactId", "EmailAddress", "Type" },
                values: new object[] { 1, 3, "jeremygwapo@gmail.com", "Home" });

            migrationBuilder.InsertData(
                table: "Number",
                columns: new[] { "Id", "ContactId", "ContactNumber", "Type" },
                values: new object[] { 1, 1, "099292929", "Work" });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Address",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Email",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Number",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Contacts",
                keyColumn: "Id",
                keyValue: 3);
        }
    }
}
