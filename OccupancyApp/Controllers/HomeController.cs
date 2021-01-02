using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using OccupancyApp.Data;
using OccupancyApp.Models;
using OccupancyApp.Service;

namespace OccupancyApp.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult NumbersOfRooms(HomeIndexModel model)
        {
            return RedirectToAction("TypeOfRooms", new {numberOfRooms = model.NumberOfRooms});
        }

        public IActionResult TypeOfRooms(int numberOfRooms)
        {
            var list = new List<string>(numberOfRooms);
            for (var i = 0; i < numberOfRooms; i++) list.Add("");

            return View(new RoomTypeModel {RoomTypeName = list});
        }

        public IActionResult RoomTypeSettings(RoomTypeModel model)
        {
            var list = new List<RoomTypeSettings>();
            for (var i = 0; i < model.RoomTypeName.Count; i++)
            {
                var rateLevelsList = new List<RateLevel>();
                for (var j = 0; j < model.RoomRateLevelsCount[i]; j++) rateLevelsList.Add(new RateLevel());
                list.Add(new RoomTypeSettings
                {
                    RoomTypeName = model.RoomTypeName[i],
                    RateLevels = rateLevelsList
                });
            }

            return View(new RoomTypeSettingsModel {CorrectList = list});
        }

        public IActionResult OccupancyResults(RoomTypeSettingsModel model)
        {
            return View(new RoomTypeSettingsModel
                {CorrectList = OccupancyService.GetOccResultInCorrectForm(model.CorrectList)});
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel {RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier});
        }
    }
}