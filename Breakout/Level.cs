using Breakout.Blocks;
using DIKUArcade.Entities;
using DIKUArcade.Math;

namespace Breakout {
    public class Level {
        public string Name { get; private set; }
        public int TimeLimit { get; private set; }
        public EntityContainer<Block> Blocks { get; private set; }

        public Level(string name, int timeLimit, EntityContainer<Block> blocks) {
            Name = name;
            TimeLimit = timeLimit;
            Blocks = blocks;
        }

        /// <summary>
        /// Render the current map
        /// </summary>
        public void Render() {
            Blocks.RenderEntities();
        }
    }
}