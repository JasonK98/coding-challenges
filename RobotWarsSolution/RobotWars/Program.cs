using System;
using System.Collections.Generic;
using RobotWars.Classes;

namespace RobotWars
{
    class Program
    {
        static void Main(string[] args)
        {
            // Read arena.json and get dimensions of arena
            Arena getArena = Arena.GetArenaFromFile();

            // Read robots from robots.json and get starting positions
            Robot robot = Robot.ReadRobotsFromFile();

            // Loop round each robot and ask user to type in where they would like it to move within arena
            List<RobotConfig> readInputFromUser = RobotConfig.ReadUserInput(robot.Robots, getArena);
        }
    }
}
