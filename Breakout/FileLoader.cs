using System.Text.RegularExpressions;

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
            
            string map = mapRegex.Match(normalized).Groups[2].ToString();
            string meta = metaRegex.Match(normalized).Groups[2].ToString();
            string legend = legendRegex.Match(normalized).Groups[2].ToString();

            throw new NotImplementedException();
            // match symbols to blocks (make hashmap?)

            // Console.WriteLine(map);
            // Console.WriteLine(meta);
            // Console.WriteLine(legend);
        }
    }
}



