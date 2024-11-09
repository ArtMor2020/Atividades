﻿// <auto-generated />
using System;
using AirTransport.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

#nullable disable

namespace AirTransport.Migrations
{
    [DbContext(typeof(AirTransportContext))]
    partial class AirTransportContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "8.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 128);

            SqlServerModelBuilderExtensions.UseIdentityColumns(modelBuilder);

            modelBuilder.Entity("AirTransport.Aircraft", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<int>("IdCompany")
                        .HasColumnType("int")
                        .HasColumnName("id_company");

                    b.Property<string>("Model")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("model");

                    b.Property<int>("NumberOfSeats")
                        .HasColumnType("int")
                        .HasColumnName("number_of_seats");

                    b.Property<string>("Type")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("type");

                    b.HasKey("Id")
                        .HasName("PK__aircraft__3213E83F10A77CAD");

                    b.HasIndex("IdCompany");

                    b.ToTable("aircraft", (string)null);
                });

            modelBuilder.Entity("AirTransport.Airport", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Location")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("location");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("PK__airport__3213E83FBF10CE9E");

                    b.ToTable("airport", (string)null);
                });

            modelBuilder.Entity("AirTransport.Company", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("PK__company__3213E83FB14E0102");

                    b.ToTable("company", (string)null);
                });

            modelBuilder.Entity("AirTransport.Flight", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<DateTime>("EstimatedArrivalTime")
                        .HasColumnType("datetime")
                        .HasColumnName("estimated_arrival_time");

                    b.Property<DateTime>("ExitTime")
                        .HasColumnType("datetime")
                        .HasColumnName("exit_time");

                    b.Property<int>("IdAircraft")
                        .HasColumnType("int")
                        .HasColumnName("id_aircraft");

                    b.Property<int>("IdDestinationAirport")
                        .HasColumnType("int")
                        .HasColumnName("id_destination_airport");

                    b.Property<int>("IdOriginAirport")
                        .HasColumnType("int")
                        .HasColumnName("id_origin_airport");

                    b.HasKey("Id")
                        .HasName("PK__flight__3213E83FBBFE6AA6");

                    b.HasIndex("IdAircraft");

                    b.HasIndex("IdDestinationAirport");

                    b.HasIndex("IdOriginAirport");

                    b.ToTable("flight", (string)null);
                });

            modelBuilder.Entity("AirTransport.Layover", b =>
                {
                    b.Property<int>("IdFlight")
                        .HasColumnType("int")
                        .HasColumnName("id_flight");

                    b.Property<int>("IdOriginAirport")
                        .HasColumnType("int")
                        .HasColumnName("id_origin_airport");

                    b.Property<DateTime>("EstimatedArrivalTime")
                        .HasColumnType("datetime")
                        .HasColumnName("estimated_arrival_time");

                    b.Property<DateTime>("ExitTime")
                        .HasColumnType("datetime")
                        .HasColumnName("exit_time");

                    b.Property<int>("IdDestinationAirport")
                        .HasColumnType("int")
                        .HasColumnName("id_destination_airport");

                    b.HasKey("IdFlight", "IdOriginAirport")
                        .HasName("PK__layover__F7182B171E7C9119");

                    b.HasIndex("IdDestinationAirport");

                    b.HasIndex("IdOriginAirport");

                    b.ToTable("layover", (string)null);
                });

            modelBuilder.Entity("AirTransport.ListPassengersFlight", b =>
                {
                    b.Property<int>("IdFlight")
                        .HasColumnType("int")
                        .HasColumnName("id_flight");

                    b.Property<int?>("IdPassenger")
                        .HasColumnType("int")
                        .HasColumnName("id_passenger");

                    b.Property<bool>("IsRight")
                        .HasColumnType("bit")
                        .HasColumnName("is_right");

                    b.Property<bool>("IsWindowSeat")
                        .HasColumnType("bit")
                        .HasColumnName("is_window_seat");

                    b.Property<int>("SeatNumber")
                        .HasColumnType("int")
                        .HasColumnName("seat_number");

                    b.HasIndex("IdFlight");

                    b.HasIndex("IdPassenger");

                    b.ToTable("list_passengers_flight", (string)null);
                });

            modelBuilder.Entity("AirTransport.Passenger", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("int")
                        .HasColumnName("id");

                    SqlServerPropertyBuilderExtensions.UseIdentityColumn(b.Property<int>("Id"));

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasMaxLength(255)
                        .IsUnicode(false)
                        .HasColumnType("varchar(255)")
                        .HasColumnName("name");

                    b.HasKey("Id")
                        .HasName("PK__passenge__3213E83F1BA0284D");

                    b.ToTable("passenger", (string)null);
                });

            modelBuilder.Entity("AirTransport.Aircraft", b =>
                {
                    b.HasOne("AirTransport.Company", "IdCompanyNavigation")
                        .WithMany("Aircraft")
                        .HasForeignKey("IdCompany")
                        .IsRequired()
                        .HasConstraintName("fk_aircraft_id_company");

                    b.Navigation("IdCompanyNavigation");
                });

            modelBuilder.Entity("AirTransport.Flight", b =>
                {
                    b.HasOne("AirTransport.Aircraft", "IdAircraftNavigation")
                        .WithMany("Flights")
                        .HasForeignKey("IdAircraft")
                        .IsRequired()
                        .HasConstraintName("fk_flight_id_aircraft");

                    b.HasOne("AirTransport.Airport", "IdDestinationAirportNavigation")
                        .WithMany("FlightIdDestinationAirportNavigations")
                        .HasForeignKey("IdDestinationAirport")
                        .IsRequired()
                        .HasConstraintName("fk_flight_id_destination_airport");

                    b.HasOne("AirTransport.Airport", "IdOriginAirportNavigation")
                        .WithMany("FlightIdOriginAirportNavigations")
                        .HasForeignKey("IdOriginAirport")
                        .IsRequired()
                        .HasConstraintName("fk_flight_id_origin_airport");

                    b.Navigation("IdAircraftNavigation");

                    b.Navigation("IdDestinationAirportNavigation");

                    b.Navigation("IdOriginAirportNavigation");
                });

            modelBuilder.Entity("AirTransport.Layover", b =>
                {
                    b.HasOne("AirTransport.Airport", "IdDestinationAirportNavigation")
                        .WithMany("LayoverIdDestinationAirportNavigations")
                        .HasForeignKey("IdDestinationAirport")
                        .IsRequired()
                        .HasConstraintName("fk_layover_id_destination_airport");

                    b.HasOne("AirTransport.Flight", "IdFlightNavigation")
                        .WithMany("Layovers")
                        .HasForeignKey("IdFlight")
                        .IsRequired()
                        .HasConstraintName("fk_layover_id_flight");

                    b.HasOne("AirTransport.Airport", "IdOriginAirportNavigation")
                        .WithMany("LayoverIdOriginAirportNavigations")
                        .HasForeignKey("IdOriginAirport")
                        .IsRequired()
                        .HasConstraintName("fk_layover_id_origin_airport");

                    b.Navigation("IdDestinationAirportNavigation");

                    b.Navigation("IdFlightNavigation");

                    b.Navigation("IdOriginAirportNavigation");
                });

            modelBuilder.Entity("AirTransport.ListPassengersFlight", b =>
                {
                    b.HasOne("AirTransport.Flight", "IdFlightNavigation")
                        .WithMany()
                        .HasForeignKey("IdFlight")
                        .IsRequired()
                        .HasConstraintName("fk_list_passengers_flight_id_flight");

                    b.HasOne("AirTransport.Passenger", "IdPassengerNavigation")
                        .WithMany()
                        .HasForeignKey("IdPassenger")
                        .HasConstraintName("fk_list_passengers_flight_id_passenger");

                    b.Navigation("IdFlightNavigation");

                    b.Navigation("IdPassengerNavigation");
                });

            modelBuilder.Entity("AirTransport.Aircraft", b =>
                {
                    b.Navigation("Flights");
                });

            modelBuilder.Entity("AirTransport.Airport", b =>
                {
                    b.Navigation("FlightIdDestinationAirportNavigations");

                    b.Navigation("FlightIdOriginAirportNavigations");

                    b.Navigation("LayoverIdDestinationAirportNavigations");

                    b.Navigation("LayoverIdOriginAirportNavigations");
                });

            modelBuilder.Entity("AirTransport.Company", b =>
                {
                    b.Navigation("Aircraft");
                });

            modelBuilder.Entity("AirTransport.Flight", b =>
                {
                    b.Navigation("Layovers");
                });
#pragma warning restore 612, 618
        }
    }
}