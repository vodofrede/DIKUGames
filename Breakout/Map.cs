using Breakout.Block;
using DIKUArcade.Entities;
using DIKUArcade.Math;

namespace Breakout {
    public class Map {
        private string name {get;}
        private int timeLimit {get;}

        private EntityContainer<StandardBlock> blocks { get; }
        
        public Map(string name, int timeLimit, EntityContainer<StandardBlock> blocks) {
            this.name = name;
            this.timeLimit = timeLimit;
            this.blocks = blocks;
        }

        /// <summary>
        /// Render the current map
        /// </summary>
        public void RenderMap() {
            blocks.RenderEntities();
        }

        /// <summary>
        /// Get all blocks as an entitycontainer
        /// </summary>
        public EntityContainer<StandardBlock> GetBlocks() {
            return blocks;
        }

        /// <summary>
        /// Get the name of the map
        /// </summary>
        public string GetName() {
            return name;
        }

        public int GetTimeLimit() {
            return timeLimit;
        }
    }
}