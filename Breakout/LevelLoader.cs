using System.Text.RegularExpressions;
using Breakout.Blocks;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout {
    public class LevelLoader {
        // fields
        private List<Level> levels;
        private int levelIndex = 0;

        // constructors
        public LevelLoader(List<Level> levels) {
            foreach (var level in levels) {
                Console.WriteLine("Loaded level: " + level.Name);
            }
            // this.levels = levels;
            this.levels = new() { ParseFile(Path.Combine("Assets", "Levels", "level2.txt")) };
            
        }
        public LevelLoader(List<string> levelPaths) : this(levelPaths.Select(levelPath => ParseFile(levelPath)).ToList()) { }
        public LevelLoader() : this(Directory.GetFiles(Path.Combine("Assets", "Levels")).ToList()) { }

        // methods
        /// <summary>
        /// Get the next level in the list of maps
        /// </summary>
        public Level? Next() {
            var level = levelIndex < levels.Count ? levels[levelIndex] : null;
            levelIndex++; ;
            return level;
        }

        /// <summary>
        /// Reset the level index
        /// </summary>
        public void Reset() {
            levelIndex = 0;
        }

        private static string mapPattern = @"(Map:\n)((.*\n)*)(Map\/)";
        private static string metaPattern = @"(Meta:\n)((.*\n)*)(Meta\/)";
        private static string legendPattern = @"(Legend:\n)((.*\n)*)(Legend\/)";

        // static methods
        /// <summary>
        /// Parse a path into a level
        /// </summary>
        public static Level ParseFile(string filePath) {
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
        public static Level? TryParseFile(string file) {
            Level? level = null;
            try {
                level = ParseFile(file);
            } catch (Exception e) {
                Console.WriteLine("Cannot parse level file, please make sure that the file is correctly formatted or choose another level.");
                Console.WriteLine("Error: " + e);
                Environment.Exit(0);
            }
            return level;
        }
    }
}



