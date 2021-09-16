using System;
using System.IO;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Newtonsoft.Json;

namespace RobotWarsTest
{
    [TestClass]
    public class ConfigTest
    {
        [TestMethod]
        public void ReadRobotsFromFile()
        {
            // Read JSON configuration of current robot positions and directions
            var path = @"..\\Config\\robots.json";
            var json = File.ReadAllText(path);
            var robots = JsonConvert.DeserializeObject<Robot>(json);

        }

        [TestMethod]
        public void ReadArenaFromFile() { 
            
        }
    }
}
