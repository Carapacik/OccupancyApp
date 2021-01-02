using System.Collections.Generic;

namespace OccupancyApp.Data
{
    public class RoomTypeSettings
    {
        public string RoomTypeName { get; set; }
        public List<RateLevel> RateLevels { get; set; }
    }
}