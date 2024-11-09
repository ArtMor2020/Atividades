using System;
using System.Collections.Generic;

namespace AirTransport;

public partial class Flight
{
    public int Id { get; set; }

    public int IdAircraft { get; set; }

    public int IdOriginAirport { get; set; }

    public int IdDestinationAirport { get; set; }

    public DateTime ExitTime { get; set; }

    public DateTime EstimatedArrivalTime { get; set; }

    public virtual Aircraft IdAircraftNavigation { get; set; } = null!;

    public virtual Airport IdDestinationAirportNavigation { get; set; } = null!;

    public virtual Airport IdOriginAirportNavigation { get; set; } = null!;

    public virtual ICollection<Layover> Layovers { get; set; } = new List<Layover>();
}
