using System.IO;
using System.Numerics;
using System.Security;
using DIKUArcade;
using DIKUArcade.GUI;
using DIKUArcade.Input;

namespace Galaga {
    public class Game : DIKUGame {
        private Player player;

        public Game(WindowArgs windowArgs) : base(windowArgs) {
            // TODO: Set key event handler (inherited window field of DIKUGame class)
            // player = new Player(
            //     new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
            //     new Image(Path.Combine("Assets", "Images", "Player.png"))
            // );
        }

        //private void KeyHandler(KeyboardAction action, KeyboardKey key) {} // TODO: Outcomment

        public override void Render() {
            // this.player.Render();
        }

        public override void Update() {
            throw new System.NotImplementedException("Galaga game has no entities to update yet.");
        }
    }
}
