using System.Collections.Generic;
using System.Linq;

namespace OccupancyApp
{
    public class OccupancyUtils
    {
        public static List<OccRateLevel> GetAllOccupancyIntervals(List<RoomTypeSettings> roomTypeSettingsList)
        {
            List<OccRateLevel> occupancyStorage = new List<OccRateLevel>();

            List<int> occupancyStorageFrom = new List<int>();
            List<int> occupancyStorageTo = new List<int>();
            foreach (RoomTypeSettings roomTypeSettings in roomTypeSettingsList)
            {
                foreach (OccRateLevel occRateLevel in roomTypeSettings.OccRateLevels)
                {
                    occupancyStorageFrom.Add(occRateLevel.occupancyFrom);
                    occupancyStorageTo.Add(occRateLevel.occupancyTo);
                }
            }

            return GetOccResult(occupancyStorageTo, occupancyStorageFrom);
        }

        public static List<RoomTypeSettings> GetOccResultInCorrectForm(List<RoomTypeSettings> roomTypeSettingsList, List<OccRateLevel> occupancyResult)
        {
            List<RoomTypeSettings> roomTypeSettingsListCorrect = new List<RoomTypeSettings>();

            foreach (var roomTypeSettings in roomTypeSettingsList)
            {
                RoomTypeSettings newRoomType = new RoomTypeSettings();
                newRoomType.RoomTypeName = roomTypeSettings.RoomTypeName;
                newRoomType.OccRateLevels = new List<OccRateLevel>();

                int ad = 0;
                foreach (OccRateLevel typeBorder in occupancyResult)
                {
                    OccRateLevel occRateLevelToCorrect = roomTypeSettings.OccRateLevels[ad];

                    if (typeBorder.occupancyTo <= occRateLevelToCorrect.occupancyTo)
                    {
                        OccRateLevel correctOccupancy = new OccRateLevel
                        {
                            occupancyFrom = typeBorder.occupancyFrom,
                            occupancyTo = typeBorder.occupancyTo,
                            rateLevel = occRateLevelToCorrect.rateLevel
                        };
                        newRoomType.OccRateLevels.Add(correctOccupancy);
                    }

                    if (typeBorder.occupancyTo == occRateLevelToCorrect.occupancyTo)
                    {
                        ad++;
                    }
                }
                roomTypeSettingsListCorrect.Add(newRoomType);
            }

            return roomTypeSettingsListCorrect;
        }

        private static List<OccRateLevel> GetOccResult(List<int> occupancyStorageTo, List<int> occupancyStorageFrom)
        {
            List<int> occupancyStorageToSorted = Sort(occupancyStorageTo).Distinct().ToList();
            List<int> occupancyStorageFromSorted = Sort(occupancyStorageFrom).Distinct().ToList();

            List<OccRateLevel> occupancyResult = new List<OccRateLevel>();
            for (var addend = 0; addend < occupancyStorageToSorted.Count(); addend++)
            {
                OccRateLevel occupancyBorders = new OccRateLevel
                {
                    occupancyFrom = occupancyStorageFromSorted[addend],
                    occupancyTo = occupancyStorageToSorted[addend]
                };
                occupancyResult.Add(occupancyBorders);
            }

            return occupancyResult;
        }



        private static List<int> Sort(List<int> list)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                int min = i;
                for (int j = i + 1; j < list.Count; j++)
                {
                    if (list[j] < list[min])
                    {
                        min = j;
                    }
                }
                int dummy = list[i];
                list[i] = list[min];
                list[min] = dummy;
            }
            return list;
        }
    }


}