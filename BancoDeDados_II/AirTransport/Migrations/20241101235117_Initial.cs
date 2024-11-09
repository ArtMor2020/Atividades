using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AirTransport.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "airport",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    location = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__airport__3213E83FBF10CE9E", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "company",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__company__3213E83FB14E0102", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "passenger",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__passenge__3213E83F1BA0284D", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "aircraft",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    type = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    model = table.Column<string>(type: "varchar(255)", unicode: false, maxLength: 255, nullable: false),
                    number_of_seats = table.Column<int>(type: "int", nullable: false),
                    id_company = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__aircraft__3213E83F10A77CAD", x => x.id);
                    table.ForeignKey(
                        name: "fk_aircraft_id_company",
                        column: x => x.id_company,
                        principalTable: "company",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "flight",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    id_aircraft = table.Column<int>(type: "int", nullable: false),
                    id_origin_airport = table.Column<int>(type: "int", nullable: false),
                    id_destination_airport = table.Column<int>(type: "int", nullable: false),
                    exit_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    estimated_arrival_time = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__flight__3213E83FBBFE6AA6", x => x.id);
                    table.ForeignKey(
                        name: "fk_flight_id_aircraft",
                        column: x => x.id_aircraft,
                        principalTable: "aircraft",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_flight_id_destination_airport",
                        column: x => x.id_destination_airport,
                        principalTable: "airport",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_flight_id_origin_airport",
                        column: x => x.id_origin_airport,
                        principalTable: "airport",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "layover",
                columns: table => new
                {
                    id_flight = table.Column<int>(type: "int", nullable: false),
                    id_origin_airport = table.Column<int>(type: "int", nullable: false),
                    id_destination_airport = table.Column<int>(type: "int", nullable: false),
                    exit_time = table.Column<DateTime>(type: "datetime", nullable: false),
                    estimated_arrival_time = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__layover__F7182B171E7C9119", x => new { x.id_flight, x.id_origin_airport });
                    table.ForeignKey(
                        name: "fk_layover_id_destination_airport",
                        column: x => x.id_destination_airport,
                        principalTable: "airport",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_layover_id_flight",
                        column: x => x.id_flight,
                        principalTable: "flight",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_layover_id_origin_airport",
                        column: x => x.id_origin_airport,
                        principalTable: "airport",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "list_passengers_flight",
                columns: table => new
                {
                    id_flight = table.Column<int>(type: "int", nullable: false),
                    id_passenger = table.Column<int>(type: "int", nullable: true),
                    is_window_seat = table.Column<bool>(type: "bit", nullable: false),
                    is_right = table.Column<bool>(type: "bit", nullable: false),
                    seat_number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.ForeignKey(
                        name: "fk_list_passengers_flight_id_flight",
                        column: x => x.id_flight,
                        principalTable: "flight",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "fk_list_passengers_flight_id_passenger",
                        column: x => x.id_passenger,
                        principalTable: "passenger",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_aircraft_id_company",
                table: "aircraft",
                column: "id_company");

            migrationBuilder.CreateIndex(
                name: "IX_flight_id_aircraft",
                table: "flight",
                column: "id_aircraft");

            migrationBuilder.CreateIndex(
                name: "IX_flight_id_destination_airport",
                table: "flight",
                column: "id_destination_airport");

            migrationBuilder.CreateIndex(
                name: "IX_flight_id_origin_airport",
                table: "flight",
                column: "id_origin_airport");

            migrationBuilder.CreateIndex(
                name: "IX_layover_id_destination_airport",
                table: "layover",
                column: "id_destination_airport");

            migrationBuilder.CreateIndex(
                name: "IX_layover_id_origin_airport",
                table: "layover",
                column: "id_origin_airport");

            migrationBuilder.CreateIndex(
                name: "IX_list_passengers_flight_id_flight",
                table: "list_passengers_flight",
                column: "id_flight");

            migrationBuilder.CreateIndex(
                name: "IX_list_passengers_flight_id_passenger",
                table: "list_passengers_flight",
                column: "id_passenger");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "layover");

            migrationBuilder.DropTable(
                name: "list_passengers_flight");

            migrationBuilder.DropTable(
                name: "flight");

            migrationBuilder.DropTable(
                name: "passenger");

            migrationBuilder.DropTable(
                name: "aircraft");

            migrationBuilder.DropTable(
                name: "airport");

            migrationBuilder.DropTable(
                name: "company");
        }
    }
}
