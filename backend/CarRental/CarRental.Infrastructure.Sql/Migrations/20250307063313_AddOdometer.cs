using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CarRental.Infrastructure.Sql.Migrations
{
    /// <inheritdoc />
    public partial class AddOdometer : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Odemeter",
                table: "Cars",
                newName: "Odometer");

            migrationBuilder.AddColumn<string>(
                name: "DealerShipName",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "DealerShortName",
                table: "Settings",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "StartOdometer",
                table: "Bookings",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DealerShipName",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "DealerShortName",
                table: "Settings");

            migrationBuilder.DropColumn(
                name: "StartOdometer",
                table: "Bookings");

            migrationBuilder.RenameColumn(
                name: "Odometer",
                table: "Cars",
                newName: "Odemeter");
        }
    }
}
