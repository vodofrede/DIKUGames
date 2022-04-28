using System.Text.RegularExpressions;
using Breakout.Block;
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
            var legend = new Dictionary<char, string>();
            foreach (string line in legendString.TrimEnd('\n').Split("\n")) {
                var left = line[0];
                var right = line[3..];
                legend[left] = right;
            }

            // parse map
            var blocks = new List<IBlock>();
            int width = mapString.Split("\n").First().Length;
            int height = mapString.TrimEnd('\n').Split("\n").Length;
            foreach (var (line, y) in mapString.Split("\n").Select((v, i) => (v, i))) {
                foreach (var (c, x) in line.Select((v, i) => (v, i))) {
                    Console.Write(c + ", ");
                    if (legend.ContainsKey(c)) {
                        if (meta.ContainsKey(c)) {
                            var type = meta[c];
                            switch (type) {
                                case BlockType.Hardened:
                                    blocks.Add(new Hardened(new Vec2I(x, y)));
                                    break;
                                case BlockType.PowerUp:
                                    blocks.Add(new PowerUp(new Vec2I(x, y)));
                                    break;
                                case BlockType.Unbreakable:
                                    blocks.Add(new Unbreakable(new Vec2I(x, y)));
                                    break;
                                default:
                                    break;
                            }
                        } else {
                            blocks.Add(new Standard(new Vec2I(x, y)));
                        }
                    }
                }
            }

            return new Map(name, timeLimit, width, height, blocks, legend);
        }
    }
}



