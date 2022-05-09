using System.Text.RegularExpressions;
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
            Console.WriteLine("Found levels: " + maps);
        }

        // get instance
        public static FileLoader GetInstance() {
            return instance ?? (instance = new FileLoader());
        }

        // methods
        public Map? NextMap() {
            var map = mapIndex < maps.Length ? ParseFile(maps[mapIndex]) : null;
            mapIndex++;
            return map;
        }

        public void ResetMaps() {
            mapIndex = 0;
        }

        private static string mapPattern = @"(Map:\n)((.*\n)*)(Map\/)";
        private static string metaPattern = @"(Meta:\n)((.*\n)*)(Meta\/)";
        private static string legendPattern = @"(Legend:\n)((.*\n)*)(Legend\/)";

        // static methods
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
            var meta = new Dictionary<char, BlockType>();
            foreach (string line in metaString.Split("\n")) {
                if (line.StartsWith("Name")) {
                    name = line[6..];
                } else if (line.StartsWith("Time")) {
                    timeLimit = int.Parse(line[6..]);
                } else if (line.StartsWith("Hardened")) {
                    meta[line[10]] = BlockType.Hardened;
                } else if (line.StartsWith("PowerUp")) {
                    meta[line[9]] = BlockType.Hungry;
                } else if (line.StartsWith("Unbreakable")) {
                    meta[line[12]] = BlockType.Unbreakable;
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
            var blocks = new EntityContainer<Block>();
            foreach ((string line, float y) in mapString.Split("\n").Select((v, i) => (v, (float)i))) {
                foreach ((char c, float x) in line.Select((v, i) => (v, (float)i))) {
                    // Console.WriteLine("x: " + x + ", y: " +  y);
                    if (legend.ContainsKey(c)) {
                        if (meta.ContainsKey(c)) {
                            var type = meta[c];
                            blocks.AddEntity(new Block(type, new Vec2F(x, y), legend[c]));
                        } else {
                            blocks.AddEntity(new Block(BlockType.Standard, new Vec2F(x, y), legend[c]));
                        }
                    }
                }
            }

            return new Map(name, timeLimit, blocks);
        }

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



