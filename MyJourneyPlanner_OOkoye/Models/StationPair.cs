using System;
using System.Collections.Generic;

namespace MyJourneyPlanner_OOkoye.Models;

public partial class StationPair
{
    public int StationPairId { get; set; }

    public int StartStationId { get; set; }

    public int DestinatioinStationId { get; set; }

    public decimal? TimeSpent { get; set; }

    public decimal? Distance { get; set; }
}
