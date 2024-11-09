using System;
using System.Collections.Generic;

namespace AirTransport;

public partial class Layover
{
    public int IdFlight { get; set; }

    public int IdOriginAirport { get; set; }

    public int IdDestinationAirport { get; set; }

    public DateTime ExitTime { get; set; }

    public DateTime EstimatedArrivalTime { get; set; }

    public virtual Airport IdDestinationAirportNavigation { get; set; } = null!;

    public virtual Flight IdFlightNavigation { get; set; } = null!;

    public virtual Airport IdOriginAirportNavigation { get; set; } = null!;
}
