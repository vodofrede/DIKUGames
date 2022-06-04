using System.Text.RegularExpressions;

namespace Breakout.Levels {
    /// <summary>
    /// Loads levels from text files and parses them into Level instances
    /// </summary
    public class LevelLoader {
        // fields
        private List<Level> levels;
        private int levelIndex = 0;

        // constructors
        public LevelLoader(List<Level> levels) {
            this.levels = levels;
            // this.levels = new() { LoadLevel(Path.Combine("Assets", "Levels", "level2.txt")) };
        }
        public LevelLoader(List<string> levelPaths) : this(levelPaths.Select(levelPath => LoadLevel(levelPath)).ToList()) { }
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

        // static methods
        /// <summary>
        /// Parse a path into a level
        /// </summary>
        public static Level LoadLevel(string filePath) {
            return LevelParser.ParseContents(LoadFileToString(filePath));
        }

        /// <summary>
        /// Loads a file to a string, normalising the line endings to LF (\n)
        /// </summary>
        public static string LoadFileToString(string filePath) {
            string file = File.ReadAllText(filePath);
            return Regex.Replace(file, @"\r\n|\n\r|\n|\r", "\n");
        }

        /// <summary>
        /// Catch any errors from parsing a level file
        /// </summary>
        public static Level? TryLoadLevel(string file) {
            Level? level = null;
            try {
                level = LoadLevel(file);
            } catch (Exception e) {
                Console.WriteLine("Cannot parse level file, please make sure that the file is correctly formatted or choose another level.");
                Console.WriteLine("Error: " + e);
            }
            return level;
        }
    }
}



