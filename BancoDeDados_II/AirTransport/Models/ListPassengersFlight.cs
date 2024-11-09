using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AirTransport;

public partial class ListPassengersFlight
{
    [Key]
    [Column(Order = 0)]
    public int IdFlight { get; set; }

    [Key]
    [Column(Order = 1)]
    public int? IdPassenger { get; set; }

    public bool IsWindowSeat { get; set; }

    public bool IsRight { get; set; }

    public int SeatNumber { get; set; }

    public virtual Flight IdFlightNavigation { get; set; } = null!;

    public virtual Passenger? IdPassengerNavigation { get; set; }
}
