using System.Text.RegularExpressions;
using Breakout.Block;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout {
    public class FileLoader {
        private static FileLoader? instance;

        private string[] maps;
        private int mapIndex = 0;

        // constructors
        private FileLoader() {
            maps = Directory.GetFiles(Path.Combine("Assets", "Levels"));
            Array.Sort(maps, StringComparer.InvariantCulture);
        }

        // get instance
        public static FileLoader GetInstance() {
            return instance ?? (instance = new FileLoader());
        }

        // methods
        /// <summary>
        /// Get the next map in the list of maps
        /// </summary>
        public Map? NextMap() {
            var map = mapIndex < maps.Length ? ParseFile(maps[mapIndex]) : null;
            mapIndex++;;
            return map;
        }

        /// <summary>
        /// Reset the map index
        /// </summary>
        public void ResetMaps() {
            mapIndex = 0;
        }

        private static string mapPattern = @"(Map:\n)((.*\n)*)(Map\/)";
        private static string metaPattern = @"(Meta:\n)((.*\n)*)(Meta\/)";
        private static string legendPattern = @"(Legend:\n)((.*\n)*)(Legend\/)";

        // static methods
        /// <summary>
        /// Parse a path into a map
        /// </summary>
        public static Map ParseFile(string filePath) {
            string file = File.ReadAllText(filePath);
            string normalized = Regex.Replace(file, @"\r\n|\n\r|\n|\r", "\n");

            Regex mapRegex = new Regex(mapPattern);
            Regex metaRegex = new Regex(metaPattern);
            Regex legendRegex = new Regex(legendPattern);

            string mapString = mapRegex.Match(normalized).Groups[2].ToString();
            string metaString = metaRegex.Match(normalized).Groups[2].ToString();
            string legendString = legendRegex.Match(normalized).Groups[2].ToString();

            // parse meta
            string name = "placeholder";
            int timeLimit = 0;
            var meta = new Dictionary<char, string>();
            foreach (string line in metaString.Split("\n")) {
                if (line.StartsWith("Name")) {
                    name = line[6..];
                } else if (line.StartsWith("Time")) {
                    timeLimit = int.Parse(line[6..]);
                } else if (line.StartsWith("Hardened")) {
                    meta[line[10]] = "Hardened";
                } else if (line.StartsWith("PowerUp")) {
                    meta[line[9]] = "PowerUp";
                } else if (line.StartsWith("Unbreakable")) {
                    meta[line[12]] = "Unbreakable";
                }
            }

            // parse legend
            var legend = new Dictionary<char, string>();
            foreach (string line in legendString.TrimEnd('\n').Split("\n")) {
                var left = line[0];
                var right = line[3..];
                legend[left] = right;
            }

            // parse map
            var blocks = new EntityContainer<Standard>();
            foreach ((string line, float y) in mapString.Split("\n").Select((v, i) => (v, (float)i))) {
                foreach ((char c, float x) in line.Select((v, i) => (v, (float)i))) {
                    if (legend.ContainsKey(c)) {
                        if (meta.ContainsKey(c)) {
                            var type = meta[c];
                            switch (type) {
                                case "Hardened":
                                    blocks.AddEntity(new Hardened(new Vec2F(x, y), legend[c]));
                                    break;
                                case "PowerUp":
                                    blocks.AddEntity(new Hungry(new Vec2F(x, y), legend[c]));
                                    break;
                                case "Unbreakable":
                                    blocks.AddEntity(new Unbreakable(new Vec2F(x, y), legend[c]));
                                    break;
                            }
                        } else {
                            blocks.AddEntity(new Standard(new Vec2F(x, y), legend[c]));
                        }
                    }
                }
            }

            return new Map(name, timeLimit, blocks);
        }

        /// <summary>
        /// Catch any errors from parsing a map file
        /// </summary>
        public static Map TryParseFile(string file) {
            Map map;
            try {
                map = ParseFile(file);
            } catch (Exception e) {
                Console.WriteLine("Cannot parse level file, please make sure that the file is correctly formatted or choose another level.");
                Console.WriteLine("Error: " + e);
                Environment.Exit(0);
                map = ParseFile(Path.Combine("Assets", "Levels", "empty.txt"));
            }
            return map;
        }
    }
}



