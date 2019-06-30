using OccupancyApp;
using System;
using System.Collections.Generic;
using Xunit;

namespace OccupancyUtilsTest
{
    public class UnitTest1
    {
        [Fact]
        public void GetOccResultInCorrectForm_ConsecutiveIntervals_CorrectInputList()
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

            var occupancyResult = Program.GetAllOccupancyIntervals(roomTypeSettingsList);

            var result = Program.GetOccResultInCorrectForm(roomTypeSettingsList, occupancyResult);

            // Assert
            //Standart:  0-20 bar1 21-60 bar2 61-100 bar2

            Assert.Equal("Standart", result[0].RoomTypeName);

            Assert.Equal(0, result[0].OccRateLevels[0].occupancyFrom);
            Assert.Equal(20, result[0].OccRateLevels[0].occupancyTo);
            Assert.Equal("bar1", result[0].OccRateLevels[0].rateLevel);

            Assert.Equal(21, result[0].OccRateLevels[1].occupancyFrom);
            Assert.Equal(60, result[0].OccRateLevels[1].occupancyTo);
            Assert.Equal("bar2", result[0].OccRateLevels[1].rateLevel);

            Assert.Equal(61, result[0].OccRateLevels[2].occupancyFrom);
            Assert.Equal(100, result[0].OccRateLevels[2].occupancyTo);
            Assert.Equal("bar2", result[0].OccRateLevels[2].rateLevel);

            //Lux:  0-20 bar2 21-60 bar2 61-100 bar3

            Assert.Equal("Lux", result[1].RoomTypeName);

            Assert.Equal(0, result[1].OccRateLevels[0].occupancyFrom);
            Assert.Equal(20, result[1].OccRateLevels[0].occupancyTo);
            Assert.Equal("bar2", result[1].OccRateLevels[0].rateLevel);

            Assert.Equal(21, result[1].OccRateLevels[1].occupancyFrom);
            Assert.Equal(60, result[1].OccRateLevels[1].occupancyTo);
            Assert.Equal("bar2", result[1].OccRateLevels[1].rateLevel);

            Assert.Equal(61, result[1].OccRateLevels[2].occupancyFrom);
            Assert.Equal(100, result[1].OccRateLevels[2].occupancyTo);
            Assert.Equal("bar3", result[1].OccRateLevels[2].rateLevel);
        }

        [Fact]
        public void GetOccResultInCorrectForm_InverseConsecutiveIntervals_CorrectInputList()
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

            var occupancyResult = Program.GetAllOccupancyIntervals(roomTypeSettingsList);

            var result = Program.GetOccResultInCorrectForm(roomTypeSettingsList, occupancyResult);

            // Assert
            //Standart:  0-30 bar1 31-60 bar1 61-100 bar2

            Assert.Equal("Standart", result[0].RoomTypeName);

            Assert.Equal(0, result[0].OccRateLevels[0].occupancyFrom);
            Assert.Equal(30, result[0].OccRateLevels[0].occupancyTo);
            Assert.Equal("bar1", result[0].OccRateLevels[0].rateLevel);

            Assert.Equal(31, result[0].OccRateLevels[1].occupancyFrom);
            Assert.Equal(60, result[0].OccRateLevels[1].occupancyTo);
            Assert.Equal("bar1", result[0].OccRateLevels[1].rateLevel);

            Assert.Equal(61, result[0].OccRateLevels[2].occupancyFrom);
            Assert.Equal(100, result[0].OccRateLevels[2].occupancyTo);
            Assert.Equal("bar2", result[0].OccRateLevels[2].rateLevel);

            //Lux: 0-30 bar2 31-60 bar2 61-100 bar3

            Assert.Equal("Lux", result[1].RoomTypeName);

            Assert.Equal(0, result[1].OccRateLevels[0].occupancyFrom);
            Assert.Equal(30, result[1].OccRateLevels[0].occupancyTo);
            Assert.Equal("bar2", result[1].OccRateLevels[0].rateLevel);

            Assert.Equal(31, result[1].OccRateLevels[1].occupancyFrom);
            Assert.Equal(60, result[1].OccRateLevels[1].occupancyTo);
            Assert.Equal("bar3", result[1].OccRateLevels[1].rateLevel);

            Assert.Equal(61, result[1].OccRateLevels[2].occupancyFrom);
            Assert.Equal(100, result[1].OccRateLevels[2].occupancyTo);
            Assert.Equal("bar3", result[1].OccRateLevels[2].rateLevel);
        }

        [Fact]
        public void GetOccResultInCorrectForm_SameBorderIntervals_CorrectInputList()
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
                    new OccRateLevel{ occupancyFrom = 0,  occupancyTo = 20, rateLevel = "bar3" },
                    new OccRateLevel{ occupancyFrom = 21,  occupancyTo = 100, rateLevel = "bar4"},
                }
            });

            var occupancyResult = Program.GetAllOccupancyIntervals(roomTypeSettingsList);

            var result = Program.GetOccResultInCorrectForm(roomTypeSettingsList, occupancyResult);

            // Assert
            //Standart:  0-20 bar1 21-21 bar1 22-30 bar2 31-100 bar3

            Assert.Equal("Standart", result[0].RoomTypeName);

            Assert.Equal(0, result[0].OccRateLevels[0].occupancyFrom);
            Assert.Equal(20, result[0].OccRateLevels[0].occupancyTo);
            Assert.Equal("bar1", result[0].OccRateLevels[0].rateLevel);

            Assert.Equal(21, result[0].OccRateLevels[1].occupancyFrom);
            Assert.Equal(21, result[0].OccRateLevels[1].occupancyTo);
            Assert.Equal("bar1", result[0].OccRateLevels[1].rateLevel);

            Assert.Equal(22, result[0].OccRateLevels[2].occupancyFrom);
            Assert.Equal(30, result[0].OccRateLevels[2].occupancyTo);
            Assert.Equal("bar2", result[0].OccRateLevels[2].rateLevel);

            Assert.Equal(31, result[0].OccRateLevels[3].occupancyFrom);
            Assert.Equal(100, result[0].OccRateLevels[3].occupancyTo);
            Assert.Equal("bar3", result[0].OccRateLevels[3].rateLevel);

            //Lux:  0-20 bar3 21-21 bar4 22-30 bar4 31-100 bar4

            Assert.Equal("Lux", result[1].RoomTypeName);

            Assert.Equal(0, result[1].OccRateLevels[0].occupancyFrom);
            Assert.Equal(20, result[1].OccRateLevels[0].occupancyTo);
            Assert.Equal("bar3", result[1].OccRateLevels[0].rateLevel);

            Assert.Equal(21, result[1].OccRateLevels[1].occupancyFrom);
            Assert.Equal(21, result[1].OccRateLevels[1].occupancyTo);
            Assert.Equal("bar4", result[1].OccRateLevels[1].rateLevel);

            Assert.Equal(22, result[1].OccRateLevels[2].occupancyFrom);
            Assert.Equal(30, result[1].OccRateLevels[2].occupancyTo);
            Assert.Equal("bar4", result[1].OccRateLevels[2].rateLevel);

            Assert.Equal(31, result[1].OccRateLevels[3].occupancyFrom);
            Assert.Equal(100, result[1].OccRateLevels[3].occupancyTo);
            Assert.Equal("bar4", result[1].OccRateLevels[3].rateLevel);

        }
    }
}