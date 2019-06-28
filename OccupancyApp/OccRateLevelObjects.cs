using System;
using System.Collections.Generic;
using System.Linq;

namespace OccupancyApp
{
    public class OccRateLevel
    {
        public int occupancyFrom { get; set; }

        public int occupancyTo { get; set; }

        public string rateLevel { get; set; }
    }

    public class RoomTypeSettings
    {
        public string RoomTypeName { get; set; }

        public List<OccRateLevel> OccRateLevels { get; set; }
    }
}