using System.Collections.Generic;
using System.Linq;
using OccupancyApp.Data;

namespace OccupancyApp.Service
{
    public static class OccupancyService
    {
        public static List<RoomTypeSettings> GetOccResultInCorrectForm(List<RoomTypeSettings> roomTypeSettingsList)
        {
            var occupancyResult = GetAllOccupancyIntervals(roomTypeSettingsList);
            var roomTypeSettingsListCorrect = new List<RoomTypeSettings>();

            foreach (var roomTypeSettings in roomTypeSettingsList)
            {
                var newRoomType = new RoomTypeSettings
                {
                    RoomTypeName = roomTypeSettings.RoomTypeName, RateLevels = new List<RateLevel>()
                };

                var ad = 0;
                foreach (var typeBorder in occupancyResult)
                {
                    var occRateLevelToCorrect = roomTypeSettings.RateLevels[ad];

                    if (typeBorder.OccupancyTo <= occRateLevelToCorrect.OccupancyTo)
                    {
                        var correctOccupancy = new RateLevel
                        {
                            OccupancyFrom = typeBorder.OccupancyFrom,
                            OccupancyTo = typeBorder.OccupancyTo,
                            OccupancyRateLevel = occRateLevelToCorrect.OccupancyRateLevel
                        };
                        newRoomType.RateLevels.Add(correctOccupancy);
                    }

                    if (typeBorder.OccupancyTo == occRateLevelToCorrect.OccupancyTo) ad++;
                }

                roomTypeSettingsListCorrect.Add(newRoomType);
            }

            return roomTypeSettingsListCorrect;
        }

        private static List<RateLevel> GetAllOccupancyIntervals(List<RoomTypeSettings> roomTypeSettingsList)
        {
            var occupancyStorageFrom = new List<int>();
            var occupancyStorageTo = new List<int>();
            foreach (var occRateLevel in roomTypeSettingsList.SelectMany(
                roomTypeSettings => roomTypeSettings.RateLevels))
            {
                occupancyStorageFrom.Add(occRateLevel.OccupancyFrom);
                occupancyStorageTo.Add(occRateLevel.OccupancyTo);
            }

            return GetOccResult(occupancyStorageTo, occupancyStorageFrom);
        }

        private static List<RateLevel> GetOccResult(List<int> occupancyStorageTo, List<int> occupancyStorageFrom)
        {
            occupancyStorageTo.Sort();
            var occupancyStorageToSorted = occupancyStorageTo.Distinct().ToList();
            occupancyStorageFrom.Sort();
            var occupancyStorageFromSorted = occupancyStorageFrom.Distinct().ToList();
            return occupancyStorageToSorted.Select((t, i) => new RateLevel
                {OccupancyFrom = occupancyStorageFromSorted[i], OccupancyTo = t}).ToList();
        }
    }
}