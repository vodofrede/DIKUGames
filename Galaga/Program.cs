using DIKUArcade.GUI;

namespace Galaga {
    internal class Program {
        private static void Main() {
            WindowArgs windowArgs = new WindowArgs { Title = "Galaga v0.1" };
            Game game = new(windowArgs);
            game.Run();
        }
    }
}
