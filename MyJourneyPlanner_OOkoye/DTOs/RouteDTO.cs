namespace MyJourneyPlanner_OOkoye.DTOs
{
    public class RouteDTO
    {
        public int LineId { get; set; }
        public int StationId { get; set; }
        public int StationOrder { get; set; }
        public string StationName { get; set; }
        public string LineName { get; set; }
        public string LineColor { get; set; }

      
    }
    public class StationDTO
    {
        public int StationId { get; set; }
    }

}
