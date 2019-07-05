using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using OccupancyApp;

namespace Web.Controllers
{
    [Route("api/roomtype-settings")]
    [ApiController]
    public class RoomTypeSettingsController : ControllerBase
    {
        static List<RoomTypeSettings> _roomTypeSettingsList = new List<RoomTypeSettings>();

        // GET api/values
        [HttpGet]
        public List<RoomTypeSettings> GetAll()
        {
            return _roomTypeSettingsList;
        }

        [HttpPost]
        public void Add([FromBody] RoomTypeSettings roomTypeSettings)
        {
            _roomTypeSettingsList.Add(roomTypeSettings);
        }

        [HttpPost("delete/{id}")]
        public void Delete(int id)
        {
            _roomTypeSettingsList.RemoveAt(id);
        }

        [HttpGet("{id}")]
        public RoomTypeSettings GetItem(int id)
        {
           return _roomTypeSettingsList[id];
        }

        [HttpPost("update/{id}")]
        public void Update([FromBody] RoomTypeSettings roomTypeSettings, int id)
        {
            _roomTypeSettingsList[id] = roomTypeSettings;
        }

        [HttpGet("calculate")]
        public List<RoomTypeSettings> Calculate()
        {
            List<RoomTypeSettings> roomTypeSettingsListCorrect = OccupancyUtils.GetOccResultInCorrectForm(_roomTypeSettingsList, OccupancyUtils.GetAllOccupancyIntervals(_roomTypeSettingsList));
            return roomTypeSettingsListCorrect;
        }

        [HttpGet("error")]
        public string Error()
        {
            return "404";
        }
    }
}
