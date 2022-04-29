using DIKUArcade.Entities;
using DIKUArcade.Math;

namespace Breakout {
    public class Map {
        private string name {get;}
        private int timeLimit {get;}

        private EntityContainer<Block> blocks { get; }
        
        public Map(string name, int timeLimit, EntityContainer<Block> blocks) {
            this.name = name;
            this.timeLimit = timeLimit;
            this.blocks = blocks;
        }

        public void RenderMap() {
            blocks.RenderEntities();
        }

        public EntityContainer<Block> GetBlocks() {
            return blocks;
        }

        public string GetName() {
            return name;
        }

        public int GetTimeLimit() {
            return timeLimit;
        }
    }
}