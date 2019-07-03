using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OccupancyApp;

namespace Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        static List<RoomTypeSettings> roomTypeSettingsList = new List<RoomTypeSettings>();

        static RoomTypeSettings _roomTypeSetting = new RoomTypeSettings
        {
            RoomTypeName = "Standart",
            OccRateLevels = new List<OccRateLevel> {
                new OccRateLevel
                {
                    occupancyFrom = 0,
                     occupancyTo = 30,
                     rateLevel = "bar1"
                },
                new OccRateLevel
                {
                    occupancyFrom = 31,
                     occupancyTo = 70,
                     rateLevel = "bar2"
                },
                new OccRateLevel
                {
                    occupancyFrom = 71,
                     occupancyTo = 100,
                     rateLevel = "bar3"
                },
            }
        };

        // GET api/values
        [HttpGet]
        public List<RoomTypeSettings> Get()
        {
            return roomTypeSettingsList;
        }

        [HttpPost]
        public RoomTypeSettings SetRoomTypeName([FromBody] RoomTypeSettings _roomTypeSettings)
        {
            // _roomTypeSetting.RoomTypeName = name;
            roomTypeSettingsList.Add(_roomTypeSettings);
            return _roomTypeSettings;
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public RoomTypeSettings Get(int id)
        {
            return roomTypeSettingsList[id];
        }

        [HttpPost("{id}")]
        public RoomTypeSettings ChangeRoomTypeName([FromBody] RoomTypeSettings _roomTypeSettings, int id)
        {
            roomTypeSettingsList[id] = _roomTypeSettings;
            return roomTypeSettingsList[id];
        }


        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
