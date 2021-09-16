using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RobotWars.Classes
{
    public class Robot
    {
        [JsonProperty("robots")]
        public List<RobotConfig> Robots { get; set; }

        public static Robot ReadRobotsFromFile() {
            try
            {
                var path = AppDomain.CurrentDomain.BaseDirectory + @"\Config\robots.json";
                var json = File.ReadAllText(path);
                var robot = JsonConvert.DeserializeObject<Robot>(json);
                return robot;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR - Arena.GetRobotsFromFile: " + ex);
                return new Robot();
            }
        }

        public static RobotConfig ChangeRobotPosition(RobotConfig robot, string command, Arena arena) {
            var commands = command.ToCharArray();
            int direction = GetCurrentDirectionDegrees(robot);
            foreach (char cmd in commands) {
                string movement = cmd.ToString();
                switch (movement) {
                    case "L":
                        direction += 90;
                        if (direction == 360) {
                            direction = 0;
                        }
                        break;
                    case "R":
                        direction -= 90;
                        break;
                    case "M":
                        robot.Direction = GetCurrentDirection(direction);
                        robot = MoveRobotOnGrid(robot, arena);
                        break;
                }
            }
            robot.Direction = GetCurrentDirection(direction);
            return robot;
        }

        private static int GetCurrentDirectionDegrees(RobotConfig robot) {
            switch (robot.Direction) {
                case "N":
                    return 0;
                case "E":
                    return 90;
                case "S":
                    return 180;
                case "W":
                    return 270;
            }
            return 0;
        }

        private static string GetCurrentDirection(int direction)
        {
            switch (direction) {
                case 0:
                    return "N";
                case 90:
                    return "E";
                case -90:
                    return "W";
                case -180:
                    return "S";
                case -270:
                    return "E";
                case -360:
                    return "N";
                case 180:
                    return "S";
                case 270:
                    return "W";
                case 360:
                    return "N";
            }
            return "N";
        }

        private static RobotConfig MoveRobotOnGrid(RobotConfig robot, Arena arena) {
            switch (robot.Direction) {
                case "N":
                    if (robot.Y_Position != arena.Height)
                    {
                        robot.Y_Position += 1;
                    }
                    break;
                case "E":
                    if (robot.X_Position != arena.Width) {
                        robot.X_Position += 1;
                    }
                    break;
                case "S":
                    if (robot.Y_Position > 0) {
                        robot.Y_Position -= 1;
                    }
                    break;
                case "W":
                    if (robot.X_Position > 0) {
                        robot.X_Position -= 1;
                    }
                    break;
            }
            return robot;
        }
    }

    public class RobotConfig {
        [JsonProperty("xPosition")]
        public int X_Position { get; set; }
        [JsonProperty("yPosition")]
        public int Y_Position { get; set; }
        [JsonProperty("direction")]
        public string Direction { get; set; }

        public static List<RobotConfig> ReadUserInput(List<RobotConfig> robots, Arena arena) {
            int count = 1;
            foreach (RobotConfig robot in robots)
            {
                Console.WriteLine("===================================================================================");
                Console.WriteLine(@$"Please enter starting position for Robot {count}, for example 0,0,N:");
                // Capture input as comma separated as this makes it easier to assign cooridnates and position from input
                // therefore easier to validate each seperate part.
                var getInput = Console.ReadLine();
                var splitInput = getInput.Split(",");
                while (splitInput.Length != 3)
                {
                    Console.WriteLine("Sorry, please enter in the format 0,0,N:");
                    getInput = Console.ReadLine();
                }

                // Validate user input
                robot.X_Position = ValidateCoordinate(Convert.ToInt32(splitInput[0]), arena.Width);
                robot.Y_Position = ValidateCoordinate(Convert.ToInt32(splitInput[1]), arena.Height);
                robot.Direction = ValidateDirection(splitInput[2]);

                // Ask user where they would like the robot to move
                Console.WriteLine(@"Please enter commands in sequence by letter to move robot 
                                    L - Turn Left
                                    R - Turn Right
                                    M - Move Forward - For example, LMMRM:");
                var moveRobot = Console.ReadLine();

                // Output current postition to user
                Console.WriteLine(@$"Current position for Robot {count}: {robot.X_Position},{robot.Y_Position},{robot.Direction}");

                var newRobotPosition = Robot.ChangeRobotPosition(robot, moveRobot, arena);
                // Output new location to user
                Console.WriteLine(@$"New position for Robot {count}:     {newRobotPosition.X_Position},{newRobotPosition.Y_Position},{newRobotPosition.Direction}");
                Console.WriteLine("===================================================================================");
                count++;
            }
            return robots;
        }

        private static int ValidateCoordinate(int coordinate, int coordinateLimit) {
            while (coordinate < 0 || coordinate > coordinateLimit) {
                // Ask user to type in valid cooridnate as it is not in arena limits
                Console.WriteLine(@$"Please enter valid X Coordinate between 0 and {coordinateLimit}:");
                var readCoordiante = Console.ReadLine();
                coordinate = Convert.ToInt32(readCoordiante);
            }
            return coordinate;
        }

        private static string ValidateDirection(string direction) {
            while (direction != "N" && direction != "E" && direction != "S" &&
                   direction != "W") {
                Console.WriteLine(direction);
                Console.WriteLine(@$"Please enter correct direction (N,E,S,W):");
                direction = Console.ReadLine();
            }
            return direction;
        }

    }
}
