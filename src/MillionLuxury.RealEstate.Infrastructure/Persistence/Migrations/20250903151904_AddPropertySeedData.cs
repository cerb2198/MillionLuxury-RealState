using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace MillionLuxury.RealEstate.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddPropertySeedData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "Property",
                columns: new[] { "Id", "CodeInternal", "CreatedAt", "CreatedBy", "DeletedAt", "DeletedBy", "IsDeleted", "LastModifiedAt", "LastModifiedBy", "Name", "OwnerId", "Price", "Year", "Address_City", "Address_Country", "Address_Street", "Address_ZipCode" },
                values: new object[,]
                {
                    { 1, 1001, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "DataSeed", null, null, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Luxury Downtown Penthouse", 1, 2500000.00m, 2020, "New York", "USA", "500 Park Avenue", 10022 },
                    { 2, 1002, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "DataSeed", null, null, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Modern Family Villa", 1, 1850000.00m, 2019, "Beverly Hills", "USA", "1234 Maple Drive", 90210 },
                    { 3, 1003, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "DataSeed", null, null, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Beachfront Condo", 2, 1200000.00m, 2021, "Miami", "USA", "789 Ocean Boulevard", 33139 },
                    { 4, 1004, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "DataSeed", null, null, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Historic Mansion", 2, 4200000.00m, 1895, "Boston", "USA", "42 Historic Lane", 2108 },
                    { 5, 1005, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "DataSeed", null, null, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Contemporary Loft", 1, 975000.00m, 2022, "Seattle", "USA", "888 Industrial Way", 98101 },
                    { 6, 1006, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "DataSeed", null, null, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Suburban Estate", 2, 3100000.00m, 2018, "Atlanta", "USA", "1500 Country Club Road", 30309 },
                    { 7, 1007, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "DataSeed", null, null, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Urban Studio Apartment", 1, 450000.00m, 2023, "Chicago", "USA", "101 Downtown Street", 60601 },
                    { 8, 1008, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "DataSeed", null, null, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Mountain Retreat Cabin", 2, 875000.00m, 2017, "Denver", "USA", "2000 Mountain View Drive", 80202 },
                    { 9, 1009, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "DataSeed", null, null, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Waterfront Townhouse", 1, 1650000.00m, 2020, "San Francisco", "USA", "75 Harbor Front", 94105 },
                    { 10, 1010, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), "DataSeed", null, null, false, new DateTime(2024, 1, 1, 0, 0, 0, 0, DateTimeKind.Utc), null, "Garden View Cottage", 2, 720000.00m, 2016, "Portland", "USA", "350 Garden Lane", 97201 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "Id",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "Id",
                keyValue: 2);

            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "Id",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "Id",
                keyValue: 4);

            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "Id",
                keyValue: 5);

            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "Id",
                keyValue: 6);

            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "Id",
                keyValue: 7);

            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "Id",
                keyValue: 8);

            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "Id",
                keyValue: 9);

            migrationBuilder.DeleteData(
                table: "Property",
                keyColumn: "Id",
                keyValue: 10);
        }
    }
}
