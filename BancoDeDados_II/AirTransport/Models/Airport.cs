using System;
using System.Collections.Generic;

namespace AirTransport;

public partial class Airport
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public string Location { get; set; } = null!;

    public virtual ICollection<Flight> FlightIdDestinationAirportNavigations { get; set; } = new List<Flight>();

    public virtual ICollection<Flight> FlightIdOriginAirportNavigations { get; set; } = new List<Flight>();

    public virtual ICollection<Layover> LayoverIdDestinationAirportNavigations { get; set; } = new List<Layover>();

    public virtual ICollection<Layover> LayoverIdOriginAirportNavigations { get; set; } = new List<Layover>();
}
