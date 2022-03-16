using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga;

public class Score {
    private int score;
    private Text display;

    public Score(Vec2F position, Vec2F extent) {
        score = 0;
        display = new Text("Score: " + score.ToString(), position, extent);
        display.SetFontSize(1000);
        display.SetColor(new Vec3I(0, 128, 255));
    }

    public void AddPoints () {
        score++;
        display.SetText(string.Format("Score: " + score.ToString()));
    }

    public void RenderScore () {
        display.RenderText();
    }
}