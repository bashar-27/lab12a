using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace lab12a.Migrations
{
    /// <inheritdoc />
    public partial class modify : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AmenitiesId",
                table: "amenities",
                type: "int",
                nullable: true);

            migrationBuilder.UpdateData(
                table: "amenities",
                keyColumn: "Id",
                keyValue: 1,
                column: "AmenitiesId",
                value: null);

            migrationBuilder.UpdateData(
                table: "amenities",
                keyColumn: "Id",
                keyValue: 2,
                column: "AmenitiesId",
                value: null);

            migrationBuilder.UpdateData(
                table: "amenities",
                keyColumn: "Id",
                keyValue: 3,
                column: "AmenitiesId",
                value: null);

            migrationBuilder.CreateIndex(
                name: "IX_amenities_AmenitiesId",
                table: "amenities",
                column: "AmenitiesId");

            migrationBuilder.AddForeignKey(
                name: "FK_amenities_amenities_AmenitiesId",
                table: "amenities",
                column: "AmenitiesId",
                principalTable: "amenities",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_amenities_amenities_AmenitiesId",
                table: "amenities");

            migrationBuilder.DropIndex(
                name: "IX_amenities_AmenitiesId",
                table: "amenities");

            migrationBuilder.DropColumn(
                name: "AmenitiesId",
                table: "amenities");
        }
    }
}
