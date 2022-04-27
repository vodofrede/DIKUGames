using DIKUArcade.GUI;

namespace Breakout {
    public class Program {
        private static void Main() {
            WindowArgs windowArgs = new WindowArgs { Title = "Breakout v0.1" };
            Game game = new(windowArgs);
            game.Run();
        }
    }
}