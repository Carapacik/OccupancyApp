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


    public class Program
    {

        static void Main(string[] args)

        {
            List<RoomTypeSettings> roomTypeSettingsList = new List<RoomTypeSettings>();
            Console.Write("Введит количество категорий номеров: ");

            int countRoomTypes = int.Parse(Console.ReadLine());

            for (int i = 0; i < countRoomTypes; i++)
            {
                Console.Write("Введит название категории номера: ");
                string roomTypeName = Console.ReadLine();
                List<string> roomTypeNames = new List<string>();
                roomTypeNames.Add(roomTypeName);
                List<OccRateLevel> occRateLevels = GetOccRateLevels();

                RoomTypeSettings occRateTypeName = new RoomTypeSettings
                {
                    RoomTypeName = roomTypeName,
                    OccRateLevels = occRateLevels
                };

                roomTypeSettingsList.Add(occRateTypeName);

            }

            Console.WriteLine();
            foreach (var roomTypeSettings in roomTypeSettingsList)
            {
                Console.WriteLine(roomTypeSettings.RoomTypeName);
                foreach (OccRateLevel occRateLevel in roomTypeSettings.OccRateLevels)
                {
                    Console.WriteLine($"{occRateLevel.occupancyFrom}-{occRateLevel.occupancyTo} {occRateLevel.rateLevel}");
                }
                Console.WriteLine();
            }

            List<OccRateLevel> occupancyResult = GetAllOccupancyIntervals(roomTypeSettingsList);

            foreach (OccRateLevel typeBorder in occupancyResult)
            {
                Console.WriteLine(typeBorder.occupancyFrom + " - " + typeBorder.occupancyTo);
            }

            foreach (var roomTypeSettings in roomTypeSettingsList)
            {
                Console.WriteLine(roomTypeSettings.RoomTypeName);
                foreach (OccRateLevel occRateLevel in roomTypeSettings.OccRateLevels)
                {
                    Console.WriteLine($"{occRateLevel.occupancyFrom}-{occRateLevel.occupancyTo} {occRateLevel.rateLevel}");
                }
                Console.WriteLine();
            }

            List<RoomTypeSettings> roomTypeSettingsListCorrect = new List<RoomTypeSettings>();

            int item = 0;
            foreach (var roomTypeSettings in roomTypeSettingsList)
            {
                //roomTypeSettings.RoomTypeName;
                roomTypeSettingsListCorrect[item].RoomTypeName = roomTypeSettings.RoomTypeName;

                int ad = 0;
                foreach (OccRateLevel typeBorder in occupancyResult)
                {
                    
                    if (typeBorder.occupancyTo < roomTypeSettings.OccRateLevels[ad].occupancyTo)
                    {
                        OccRateLevel correctOccupancy = new OccRateLevel();
                        correctOccupancy.occupancyFrom = typeBorder.occupancyFrom;
                        correctOccupancy.occupancyTo = typeBorder.occupancyTo;
                        correctOccupancy.rateLevel = typeBorder.rateLevel;

                        roomTypeSettingsListCorrect[item].OccRateLevels.Add(correctOccupancy);
                    }

                    if(typeBorder.occupancyTo == roomTypeSettings.OccRateLevels[ad].occupancyTo)
                    {
                        OccRateLevel correctOccupancy = new OccRateLevel();
                        correctOccupancy.occupancyFrom = typeBorder.occupancyFrom;
                        correctOccupancy.occupancyTo = typeBorder.occupancyTo;
                        correctOccupancy.rateLevel = typeBorder.rateLevel;
                        ad++;
                        roomTypeSettingsListCorrect[item].OccRateLevels.Add(correctOccupancy);
                    }
                }

                item++;
            }

            foreach (var roomTypeSettings in roomTypeSettingsListCorrect)
            {
                Console.WriteLine(roomTypeSettings.RoomTypeName);
                foreach (OccRateLevel occRateLevel in roomTypeSettings.OccRateLevels)
                {
                    Console.WriteLine($"{occRateLevel.occupancyFrom}-{occRateLevel.occupancyTo} {occRateLevel.rateLevel}");
                }
                Console.WriteLine();
            }

        }

        static List<OccRateLevel> GetOccRateLevels()
        {
            List<OccRateLevel> occRateLevels = new List<OccRateLevel>();
            Console.WriteLine("Введите сопоставления загрузки, пример '21-50 bar1'");

            for (int count = 0; count < 50; count++)
            {
                Console.Write("Сопоставление: ");

                string newLineFromConsole = Console.ReadLine();
                string occupancy = newLineFromConsole.Split(' ')[0];
                int[] parsedOccupancy = occupancy.Split("-").Select(int.Parse).ToArray();
                string ratelevel = newLineFromConsole.Split(' ')[1];



                OccRateLevel occRateLevel = new OccRateLevel();
                occRateLevel.rateLevel = ratelevel;
                occRateLevel.occupancyFrom = parsedOccupancy[0];
                occRateLevel.occupancyTo = parsedOccupancy[1];

                occRateLevels.Add(occRateLevel);

                if (occRateLevel.occupancyTo >= 100)
                {
                    break;
                }
            }
            return occRateLevels;
        }

        public static List<int> Sort(List<int> list)
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

        static List<OccRateLevel> GetOccResult(List<int> occupancyStorageTo, List<int> occupancyStorageFrom)
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


    }
}