using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TimRailcarTrips.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cities",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CityName = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    TimeZone = table.Column<string>(type: "TEXT", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Cities", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "EventCodeDefinitions",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Code = table.Column<string>(type: "TEXT", maxLength: 1, nullable: false),
                    DescriptionShort = table.Column<string>(type: "TEXT", maxLength: 200, nullable: false),
                    DescriptionLong = table.Column<string>(type: "TEXT", maxLength: 2000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EventCodeDefinitions", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Trips",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    EquipmentCode = table.Column<string>(type: "TEXT", nullable: false),
                    OriginCityId = table.Column<int>(type: "INTEGER", nullable: false),
                    DestinationCityId = table.Column<int>(type: "INTEGER", nullable: false),
                    StartDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    EndDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    TotalTripHours = table.Column<double>(type: "REAL", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Trips", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Trips_Cities_DestinationCityId",
                        column: x => x.DestinationCityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Trips_Cities_OriginCityId",
                        column: x => x.OriginCityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TripEvent",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    TripId = table.Column<int>(type: "INTEGER", nullable: false),
                    EventCodeId = table.Column<int>(type: "INTEGER", nullable: false),
                    EventDateTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    CityId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TripEvent", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TripEvent_Cities_CityId",
                        column: x => x.CityId,
                        principalTable: "Cities",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripEvent_EventCodeDefinitions_EventCodeId",
                        column: x => x.EventCodeId,
                        principalTable: "EventCodeDefinitions",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TripEvent_Trips_TripId",
                        column: x => x.TripId,
                        principalTable: "Trips",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_EventCodeDefinitions_Code",
                table: "EventCodeDefinitions",
                column: "Code");

            migrationBuilder.CreateIndex(
                name: "IX_TripEvent_CityId",
                table: "TripEvent",
                column: "CityId");

            migrationBuilder.CreateIndex(
                name: "IX_TripEvent_EventCodeId",
                table: "TripEvent",
                column: "EventCodeId");

            migrationBuilder.CreateIndex(
                name: "IX_TripEvent_TripId",
                table: "TripEvent",
                column: "TripId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_DestinationCityId",
                table: "Trips",
                column: "DestinationCityId");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_OriginCityId",
                table: "Trips",
                column: "OriginCityId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TripEvent");

            migrationBuilder.DropTable(
                name: "EventCodeDefinitions");

            migrationBuilder.DropTable(
                name: "Trips");

            migrationBuilder.DropTable(
                name: "Cities");
        }
    }
}
