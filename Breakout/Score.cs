using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout {
    public class Score {
        private readonly Text display;

        public uint Points { get; private set; }

        public Score(Vec2F position, Vec2F extent) {
            Points = 0;
            display = new Text("Score: " + Points.ToString(), position, extent);
            display.SetFontSize(1000);
            display.SetColor(new Vec3I(0, 128, 255));
        }

        /// <summary>
        /// Add points to the score
        /// </summary>
        public void AddPoints(int value) {
            Points += (uint)value;
            display.SetText(string.Format("Score: " + Points.ToString()));
        }

        /// <summary>
        /// Render the score to the screen
        /// </summary>
        public void RenderScore() {
            display.RenderText();
        }
    }
}