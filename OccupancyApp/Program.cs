using System;
using System.Collections.Generic;
using System.Linq;

namespace OccupancyApp
{
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

            List<OccRateLevel> occupancyResult = OccupancyUtils.GetAllOccupancyIntervals(roomTypeSettingsList);

            foreach (OccRateLevel typeBorder in occupancyResult)
            {
                Console.WriteLine(typeBorder.occupancyFrom + " - " + typeBorder.occupancyTo);
            }

            List<RoomTypeSettings> roomTypeSettingsListCorrect = OccupancyUtils.GetOccResultInCorrectForm(roomTypeSettingsList, occupancyResult);

            Console.WriteLine();
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

    }
}