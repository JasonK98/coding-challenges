using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;
using RobotWars.Classes;
using System;
using System.IO;

namespace RobotWarsUnitTest
{
    [TestClass]
    public class ReadConfigTest
    {
        [TestMethod]
        public void ReadRobotsFromFile()
        {
            // Read file and check robot count is greater than zero
            var path = AppDomain.CurrentDomain.BaseDirectory + @"\Config\robots.json";
            var json = File.ReadAllText(path);
            var robot = JsonConvert.DeserializeObject<Robot>(json);
            var count = robot.Robots.Count;

            Assert.IsTrue(count > 0);
        }

        [TestMethod]
        public void ReadArenaFromFIle() {
            // Get arena size from file
            var path = AppDomain.CurrentDomain.BaseDirectory + @"\Config\arena.json";
            var json = File.ReadAllText(path);
            var arena = JsonConvert.DeserializeObject<Arena>(json);

            Assert.IsTrue(arena.Height != 0 && arena.Width != 0);
        }


    }
}
