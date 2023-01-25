using System;
using System.Collections.Generic;

namespace MyJourneyPlanner_OOkoye.Models;

public partial class LineStation
{
    public int LineStationId { get; set; }

    public int LineId { get; set; }

    public int StationId { get; set; }
}
