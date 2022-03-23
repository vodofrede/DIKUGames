using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga;

public class Score {
    private int points;
    private Text display;

    public int Points { get { return points; }}

    public Score(Vec2F position, Vec2F extent) {
        points = 0;
        display = new Text("Score: " + points.ToString(), position, extent);
        display.SetFontSize(1000);
        display.SetColor(new Vec3I(0, 128, 255));
    }

    public void AddPoints () {
        points++;
        display.SetText(string.Format("Score: " + points.ToString()));
    }

    public void RenderScore () {
        display.RenderText();
    }
}