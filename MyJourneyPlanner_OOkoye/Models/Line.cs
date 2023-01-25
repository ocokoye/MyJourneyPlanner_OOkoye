using System;
using System.Collections.Generic;

namespace MyJourneyPlanner_OOkoye.Models;

public partial class Line
{
    public int LineId { get; set; }

    public string LineName { get; set; } = null!;

    public string? LineColor { get; set; }
}
