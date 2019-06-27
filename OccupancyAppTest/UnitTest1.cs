using OccupancyApp;
using System;
using System.Collections.Generic;
using Xunit;

namespace OccupancyAppTest
{
    public class UnitTest1
    {
        [Fact]
        public void GetAllOccupancyIntervals_ConsecutiveIntervals_CorrectBorders()
        {
            // Act
            //0-20 21-100 and 0-60 61-100
            List<RoomTypeSettings> roomTypeSettingsList = new List<RoomTypeSettings>();

            roomTypeSettingsList.Add(new RoomTypeSettings
            {
                RoomTypeName = "Standart",

                OccRateLevels = new List<OccRateLevel>
                {
                    new OccRateLevel{ occupancyFrom = 0,  occupancyTo = 20, rateLevel = "bar1" },
                    new OccRateLevel{ occupancyFrom = 21,  occupancyTo = 100, rateLevel = "bar2"},
                }
            });

            roomTypeSettingsList.Add(new RoomTypeSettings
            {
                RoomTypeName = "Lux",

                OccRateLevels = new List<OccRateLevel>
                {
                    new OccRateLevel{ occupancyFrom = 0,  occupancyTo = 60, rateLevel = "bar2" },
                    new OccRateLevel{ occupancyFrom = 61,  occupancyTo = 100, rateLevel = "bar3"},
                }
            });

            var result = Program.GetAllOccupancyIntervals(roomTypeSettingsList);


            // Assert
            Assert.Equal(0, result[0].occupancyFrom);
            Assert.Equal(21, result[1].occupancyFrom);
            Assert.Equal(61, result[2].occupancyFrom);

            Assert.Equal(20, result[0].occupancyTo);
            Assert.Equal(60, result[1].occupancyTo);
            Assert.Equal(100, result[2].occupancyTo);
        }

        [Fact]
        public void GetAllOccupancyIntervals_InverseConsecutiveIntervals_CorrectBorders()
        {
            // Act
            //0-60 61-100 and 0-30 31-100

            List<RoomTypeSettings> roomTypeSettingsList = new List<RoomTypeSettings>();

            roomTypeSettingsList.Add(new RoomTypeSettings
            {
                RoomTypeName = "Standart",

                OccRateLevels = new List<OccRateLevel>
                {
                    new OccRateLevel{ occupancyFrom = 0,  occupancyTo = 60, rateLevel = "bar1" },
                    new OccRateLevel{ occupancyFrom = 61,  occupancyTo = 100, rateLevel = "bar2"},
                }
            });

            roomTypeSettingsList.Add(new RoomTypeSettings
            {
                RoomTypeName = "Lux",

                OccRateLevels = new List<OccRateLevel>
                {
                    new OccRateLevel{ occupancyFrom = 0,  occupancyTo = 30, rateLevel = "bar2" },
                    new OccRateLevel{ occupancyFrom = 31,  occupancyTo = 100, rateLevel = "bar3"},
                }
            });

            var result = Program.GetAllOccupancyIntervals(roomTypeSettingsList);


            // Assert
            Assert.Equal(0, result[0].occupancyFrom);
            Assert.Equal(31, result[1].occupancyFrom);
            Assert.Equal(61, result[2].occupancyFrom);

            Assert.Equal(30, result[0].occupancyTo);
            Assert.Equal(60, result[1].occupancyTo);
            Assert.Equal(100, result[2].occupancyTo);
        }

        [Fact]
        public void GetAllOccupancyIntervals_IntersectingIntervals_CorrectBorders()
        {
            // Act
            //0-20 21-30 31-60 61-100 and 0-30 31-100

            List<RoomTypeSettings> roomTypeSettingsList = new List<RoomTypeSettings>();

            roomTypeSettingsList.Add(new RoomTypeSettings
            {
                RoomTypeName = "Standart",

                OccRateLevels = new List<OccRateLevel>
                {
                    new OccRateLevel{ occupancyFrom = 0,  occupancyTo = 20, rateLevel = "bar1" },
                    new OccRateLevel{ occupancyFrom = 21,  occupancyTo = 30, rateLevel = "bar2"},
                    new OccRateLevel{ occupancyFrom = 31,  occupancyTo = 60, rateLevel = "bar3"},
                    new OccRateLevel{ occupancyFrom = 61,  occupancyTo = 100, rateLevel = "bar4"},
                }
            });

            roomTypeSettingsList.Add(new RoomTypeSettings
            {
                RoomTypeName = "Lux",

                OccRateLevels = new List<OccRateLevel>
                {
                    new OccRateLevel{ occupancyFrom = 0,  occupancyTo = 30, rateLevel = "bar2" },
                    new OccRateLevel{ occupancyFrom = 31,  occupancyTo = 100, rateLevel = "bar3"},
                }
            });

            var result = Program.GetAllOccupancyIntervals(roomTypeSettingsList);


            // Assert
            Assert.Equal(0, result[0].occupancyFrom);
            Assert.Equal(21, result[1].occupancyFrom);
            Assert.Equal(31, result[2].occupancyFrom);
            Assert.Equal(61, result[3].occupancyFrom);

            Assert.Equal(20, result[0].occupancyTo);
            Assert.Equal(30, result[1].occupancyTo);
            Assert.Equal(60, result[2].occupancyTo);
            Assert.Equal(100, result[3].occupancyTo);
        }

        [Fact]
        public void GetAllOccupancyIntervals_SameBorderIntervals_CorrectBorders()
        {
            // Act
            //0-21 22-30 31-100 and 0-20 21-100

            List<RoomTypeSettings> roomTypeSettingsList = new List<RoomTypeSettings>();

            roomTypeSettingsList.Add(new RoomTypeSettings
            {
                RoomTypeName = "Standart",

                OccRateLevels = new List<OccRateLevel>
                {
                    new OccRateLevel{ occupancyFrom = 0,  occupancyTo = 21, rateLevel = "bar1" },
                    new OccRateLevel{ occupancyFrom = 22,  occupancyTo = 30, rateLevel = "bar2"},
                    new OccRateLevel{ occupancyFrom = 31,  occupancyTo = 100, rateLevel = "bar3"},
                }
            });

            roomTypeSettingsList.Add(new RoomTypeSettings
            {
                RoomTypeName = "Lux",

                OccRateLevels = new List<OccRateLevel>
                {
                    new OccRateLevel{ occupancyFrom = 0,  occupancyTo = 20, rateLevel = "bar2" },
                    new OccRateLevel{ occupancyFrom = 21,  occupancyTo = 100, rateLevel = "bar3"},
                }
            });

            var result = Program.GetAllOccupancyIntervals(roomTypeSettingsList);


            // Assert
            Assert.Equal(0, result[0].occupancyFrom);
            Assert.Equal(21, result[1].occupancyFrom);
            Assert.Equal(22, result[2].occupancyFrom);
            Assert.Equal(31, result[3].occupancyFrom);

            Assert.Equal(20, result[0].occupancyTo);
            Assert.Equal(21, result[1].occupancyTo);
            Assert.Equal(30, result[2].occupancyTo);
            Assert.Equal(100, result[3].occupancyTo);
        }
    }
}
