using System.Text.RegularExpressions;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout {
    public class FileLoader {
        private static string mapPattern = @"(Map:\n)((.*\n)*)(Map\/)";
        private static string metaPattern = @"(Meta:\n)((.*\n)*)(Meta\/)";
        private static string legendPattern = @"(Legend:\n)((.*\n)*)(Legend\/)";

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
                    meta[line[9]] = BlockType.PowerUp;
                } else if (line.StartsWith("Unbreakable")) {
                    meta[line[12]] = BlockType.Unbreakable;
                }
            }

            // parse legend
            var legend = new Dictionary<char, Image>();
            foreach (string line in legendString.TrimEnd('\n').Split("\n")) {
                var left = line[0];
                var right = line[3..];
                var image = new Image(Path.Combine("Assets", "Images", right));
                legend[left] = image;
            }

            // parse map
            var blocks = new EntityContainer<Block>();
            int width = mapString.Split("\n").First().Length;
            int height = mapString.TrimEnd('\n').Split("\n").Length;
            
            foreach ((string line, float y) in mapString.Split("\n").Select((v, i) => (v, (float) i))) {
                foreach ((char c, float x) in line.Select((v, i) => (v, (float) i))) {
                    // Console.WriteLine("x: " + x + ", y: " +  y);
                    if (legend.ContainsKey(c)) {
                        if (meta.ContainsKey(c)) {
                            var type = meta[c];
                            blocks.AddEntity(new Block(type, new StationaryShape(new Vec2F(x / 12f, 1f - y / 25f), new Vec2F(1f / 12f, 1f / 25f)), legend[c]));
                        } else {
                            blocks.AddEntity(new Block(BlockType.Standard, new StationaryShape(new Vec2F(x / 12f, 1f - y / 25f), new Vec2F(1f / 12f, 1f / 25f)), legend[c]));
                        }
                    }
                }
            }

            return new Map(name, timeLimit, width, height, blocks);
        }
    }
}



