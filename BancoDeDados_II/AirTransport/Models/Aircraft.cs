using System;
using System.Collections.Generic;

namespace AirTransport;

public partial class Aircraft
{
    public int Id { get; set; }

    public string Type { get; set; } = null!;

    public string Model { get; set; } = null!;

    public int NumberOfSeats { get; set; }

    public int IdCompany { get; set; }

    public virtual ICollection<Flight> Flights { get; set; } = new List<Flight>();

    public virtual Company IdCompanyNavigation { get; set; } = null!;
}
