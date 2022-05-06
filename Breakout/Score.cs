using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout {
    public class Score {
        private readonly Text display;

        public int Points { get; private set; }

        public Score(Vec2F position, Vec2F extent) {
            Points = 0;
            display = new Text("Score: " + Points.ToString(), position, extent);
            display.SetFontSize(1000);
            display.SetColor(new Vec3I(0, 128, 255));
        }

        public void AddPoints(int value) {
            Points += value;
            display.SetText(string.Format("Score: " + Points.ToString()));
        }

        public void RenderScore() {
            display.RenderText();
        }
    }
}