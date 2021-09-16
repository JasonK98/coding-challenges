using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Text;
using RobotWars.Classes;
using Newtonsoft.Json;

namespace RobotWarsUnitTest
{
    [TestClass]
    public class RobotMovementTest
    {
        [TestMethod]
        public void CheckRobotCorrectLocation() {
            Arena arena = Arena.GetArenaFromFile();
            RobotConfig robot = new RobotConfig { 
                X_Position = 1,
                Y_Position = 2,
                Direction = "N"
            };
            RobotConfig expected = new RobotConfig {
                X_Position = 1,
                Y_Position = 3,
                Direction = "N"
            }; 
            string moveCommand = "LMLMLMLMM";
            RobotConfig actual = Robot.ChangeRobotPosition(robot, moveCommand, arena);

            var expectedJson = JsonConvert.SerializeObject(expected);
            var actualJson = JsonConvert.SerializeObject(actual);

            Assert.AreEqual(expectedJson, actualJson);
        }
    }
}
