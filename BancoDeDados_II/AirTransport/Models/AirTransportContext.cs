using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace AirTransport.Models;

public partial class AirTransportContext : DbContext
{
    public AirTransportContext()
    {
    }

    public AirTransportContext(DbContextOptions<AirTransportContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Aircraft> Aircraft { get; set; }

    public virtual DbSet<Airport> Airports { get; set; }

    public virtual DbSet<Company> Companies { get; set; }

    public virtual DbSet<Flight> Flights { get; set; }

    public virtual DbSet<Layover> Layovers { get; set; }

    public virtual DbSet<ListPassengersFlight> ListPassengersFlights { get; set; }

    public virtual DbSet<Passenger> Passengers { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=AirTransport");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Aircraft>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__aircraft__3213E83F10A77CAD");

            entity.ToTable("aircraft");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.IdCompany).HasColumnName("id_company");
            entity.Property(e => e.Model)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("model");
            entity.Property(e => e.NumberOfSeats).HasColumnName("number_of_seats");
            entity.Property(e => e.Type)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("type");

            entity.HasOne(d => d.IdCompanyNavigation).WithMany(p => p.Aircraft)
                .HasForeignKey(d => d.IdCompany)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_aircraft_id_company");
        });

        modelBuilder.Entity<Airport>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__airport__3213E83FBF10CE9E");

            entity.ToTable("airport");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Location)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("location");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Company>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__company__3213E83FB14E0102");

            entity.ToTable("company");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });

        modelBuilder.Entity<Flight>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__flight__3213E83FBBFE6AA6");

            entity.ToTable("flight");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.EstimatedArrivalTime)
                .HasColumnType("datetime")
                .HasColumnName("estimated_arrival_time");
            entity.Property(e => e.ExitTime)
                .HasColumnType("datetime")
                .HasColumnName("exit_time");
            entity.Property(e => e.IdAircraft).HasColumnName("id_aircraft");
            entity.Property(e => e.IdDestinationAirport).HasColumnName("id_destination_airport");
            entity.Property(e => e.IdOriginAirport).HasColumnName("id_origin_airport");

            entity.HasOne(d => d.IdAircraftNavigation).WithMany(p => p.Flights)
                .HasForeignKey(d => d.IdAircraft)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_flight_id_aircraft");

            entity.HasOne(d => d.IdDestinationAirportNavigation).WithMany(p => p.FlightIdDestinationAirportNavigations)
                .HasForeignKey(d => d.IdDestinationAirport)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_flight_id_destination_airport");

            entity.HasOne(d => d.IdOriginAirportNavigation).WithMany(p => p.FlightIdOriginAirportNavigations)
                .HasForeignKey(d => d.IdOriginAirport)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_flight_id_origin_airport");
        });

        modelBuilder.Entity<Layover>(entity =>
        {
            entity.HasKey(e => new { e.IdFlight, e.IdOriginAirport }).HasName("PK__layover__F7182B171E7C9119");

            entity.ToTable("layover");

            entity.Property(e => e.IdFlight).HasColumnName("id_flight");
            entity.Property(e => e.IdOriginAirport).HasColumnName("id_origin_airport");
            entity.Property(e => e.EstimatedArrivalTime)
                .HasColumnType("datetime")
                .HasColumnName("estimated_arrival_time");
            entity.Property(e => e.ExitTime)
                .HasColumnType("datetime")
                .HasColumnName("exit_time");
            entity.Property(e => e.IdDestinationAirport).HasColumnName("id_destination_airport");

            entity.HasOne(d => d.IdDestinationAirportNavigation).WithMany(p => p.LayoverIdDestinationAirportNavigations)
                .HasForeignKey(d => d.IdDestinationAirport)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_layover_id_destination_airport");

            entity.HasOne(d => d.IdFlightNavigation).WithMany(p => p.Layovers)
                .HasForeignKey(d => d.IdFlight)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_layover_id_flight");

            entity.HasOne(d => d.IdOriginAirportNavigation).WithMany(p => p.LayoverIdOriginAirportNavigations)
                .HasForeignKey(d => d.IdOriginAirport)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_layover_id_origin_airport");
        });

        modelBuilder.Entity<ListPassengersFlight>(entity =>
        {
            // Define a composite primary key using both IdFlight and IdPassenger
            entity.HasKey(e => new { e.IdFlight, e.IdPassenger });

            entity.ToTable("list_passengers_flight");

            entity.Property(e => e.IdFlight).HasColumnName("id_flight");
            entity.Property(e => e.IdPassenger).HasColumnName("id_passenger");
            entity.Property(e => e.IsRight).HasColumnName("is_right");
            entity.Property(e => e.IsWindowSeat).HasColumnName("is_window_seat");
            entity.Property(e => e.SeatNumber).HasColumnName("seat_number");

            entity.HasOne(d => d.IdFlightNavigation).WithMany()
                .HasForeignKey(d => d.IdFlight)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("fk_list_passengers_flight_id_flight");

            entity.HasOne(d => d.IdPassengerNavigation).WithMany()
                .HasForeignKey(d => d.IdPassenger)
                .HasConstraintName("fk_list_passengers_flight_id_passenger");
        });

        /*base.OnModelCreating(modelBuilder);
        modelBuilder.Entity<ListPassengersFlight>(entity =>
        {
            entity.HasKey(x => x.IdFlight);
        });*/

        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__passenge__3213E83F1BA0284D");

            entity.ToTable("passenger");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.Name)
                .HasMaxLength(255)
                .IsUnicode(false)
                .HasColumnName("name");
        });



        OnModelCreatingPartial(modelBuilder);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
