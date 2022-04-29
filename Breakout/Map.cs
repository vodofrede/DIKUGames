using DIKUArcade.Entities;
using DIKUArcade.Math;

namespace Breakout {
    public class Map {
        private string name;
        private int timeLimit;

        private int width;
        private int height;

        private EntityContainer<Block> blocks { get; }
        
        public Map(string name, int timeLimit, int width, int height, EntityContainer<Block> blocks) {
            this.name = name;
            this.timeLimit = timeLimit;
            this.width = width;
            this.height = height;
            this.blocks = blocks;
        }

        public void RenderMap() {
            blocks.RenderEntities();
        }
    }
}