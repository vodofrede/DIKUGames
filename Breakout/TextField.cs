using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Breakout {
    /// <summary>
    /// Wrapper around the Text class which can auto-update the contents of the text.
    /// </summary
    public class TextField {
        protected Text text;
        protected Func<string> displayed;
        public List<Action> Behaviors { get; protected set; } = new();

        public TextField(Func<string> displayed, Vec2F pos, Vec2F extent) {
            text = new Text(displayed(), pos, extent);
            this.displayed = displayed;
            text.SetFontSize(1000);
            text.SetColor(new Vec3I(255, 255, 255));
        }

        public void Render() {
            foreach (var b in Behaviors) {
                b();
            }
            text.SetText(displayed());
            text.RenderText();
        }

        public void SetFontSize(int size) {
            text.SetFontSize(size);
        }

        public bool? SetColor(int r, int b, int g) {
            SetColor(new Vec3I(r, g, b));
            return null;
        }

        public void SetColor(Vec3I color) {
            text.SetColor(color);
        }
    }
}