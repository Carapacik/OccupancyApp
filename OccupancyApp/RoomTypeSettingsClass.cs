using System;
using System.Collections.Generic;
using System.Linq;

namespace OccupancyApp
{ 

    public class RoomTypeSettings
    {
        public string RoomTypeName { get; set; }

        public List<OccRateLevel> OccRateLevels { get; set; }
    }
}