using System.Text.RegularExpressions;
using Breakout.Blocks;
using DIKUArcade.Entities;
using DIKUArcade.Math;

namespace Breakout.Levels {
    public class LevelParser {
        private static string mapPattern = @"(Map:\n)((.*\n)*)(Map\/)";
        private static string metaPattern = @"(Meta:\n)((.*\n)*)(Meta\/)";
        private static string legendPattern = @"(Legend:\n)((.*\n)*)(Legend\/)";

        // static methods
        /// <summary>
        /// Parse a path into a level
        /// </summary>
        public static Level ParseContents(string levelContents) {
            if (levelContents == null) {
                throw new ArgumentException("levelContents cannot be null or empty");
            }

            Regex mapRegex = new Regex(mapPattern);
            Regex metaRegex = new Regex(metaPattern);
            Regex legendRegex = new Regex(legendPattern);

            string mapString = mapRegex.Match(levelContents).Groups[2].ToString();
            string metaString = metaRegex.Match(levelContents).Groups[2].ToString();
            string legendString = legendRegex.Match(levelContents).Groups[2].ToString();

            // parse meta
            string name = "placeholder";
            int? timeLimit = null;
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

            // parse level
            var blocks = new EntityContainer<Block>();
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
                                    blocks.AddEntity(new DoubleSize(new Vec2F(x, y), legend[c]));
                                    break;
                                case "Unbreakable":
                                    blocks.AddEntity(new Unbreakable(new Vec2F(x, y), legend[c]));
                                    break;
                            }
                        } else {
                            blocks.AddEntity(new Block(new Vec2F(x, y), legend[c]));
                        }
                    }
                }
            }

            return new Level(name, timeLimit, blocks);
        }

        /// <summary>
        /// Catch any errors from parsing a level file
        /// </summary>
        public static Level? TryParseContents(string levelContents) {
            Level? level = null;
            try {
                level = ParseContents(levelContents);
            } catch (Exception e) {
                Console.WriteLine("Cannot parse level file, please make sure that the file is correctly formatted or choose another level.");
                Console.WriteLine("Error: " + e);
            }
            return level;
        }
    }
}