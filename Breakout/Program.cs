using DIKUArcade.GUI;

namespace Breakout {
    /// <summary>
    /// Main 
    /// </summary
    public class Program {
        private static void Main() {
            WindowArgs windowArgs = new WindowArgs { Title = "Breakout v999.0" };
            Game game = new(windowArgs);
            game.Run();
        }
    }
}