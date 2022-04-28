using Breakout.Block;
using DIKUArcade.Math;

namespace Breakout {
    public class Map {
        private string name;
        private int timeLimit;

        private int width;
        private int height;

        private List<IBlock> blocks;
        // tegn til .png-fil konvertering
        // måske burde vi lave en dedikeret "image" klasse eller bruge en eksisterende klasse fra DIKUArcade
        private Dictionary<char, string> legend;
        
        public Map(string name, int timeLimit, int width, int height, List<IBlock> blocks, Dictionary<char, string> legend) {
            this.name = name;
            this.timeLimit = timeLimit;
            this.width = width;
            this.height = height;
            this.blocks = blocks;
            this.legend = legend;
        }

        public void RenderMap() {
            foreach (IBlock block in blocks) {
                // render
            }
            throw new NotImplementedException();
        }

        public IBlock GetBlock(Vec2I position) {
            return blocks.Find(block => block.GetPosition() == position) ?? throw new ArgumentException("No block was found with position " + position);
        }
    }
}