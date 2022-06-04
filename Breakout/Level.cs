using Breakout.Blocks;
using DIKUArcade.Entities;

namespace Breakout {
    /// <summary>
    /// Simple level class which contains relevant information for rendering a level
    /// </summary
    public class Level {
        public string Name { get; private set; }
        public int TimeLimit { get; private set; }
        public EntityContainer<Block> Blocks { get; private set; }

        public Level(string name, int? timeLimit, EntityContainer<Block> blocks) {
            Name = name;
            TimeLimit = timeLimit ?? 300;
            Blocks = blocks;
        }

        /// <summary>
        /// RenderText the current level
        /// </summary>
        public void Render() {
            Blocks.RenderEntities();
        }
    }
}