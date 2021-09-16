using Newtonsoft.Json;
using RobotWars.Interfaces;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace RobotWars.Classes
{
    
    public class Arena : IArena {
        [JsonProperty("width")]
        public int Width { get; set; }
        [JsonProperty("height")]
        public int Height { get; set; }

        public static Arena GetArenaFromFile() {
            try
            {
                var path = AppDomain.CurrentDomain.BaseDirectory + @"\Config\arena.json";
                var json = File.ReadAllText(path);
                var arena = JsonConvert.DeserializeObject<Arena>(json);
                return arena;
            }
            catch (Exception ex)
            {
                Console.WriteLine("ERROR - Arena.GetArenaFromFile: " + ex);
                return new Arena();
            }
        }
    }
}
