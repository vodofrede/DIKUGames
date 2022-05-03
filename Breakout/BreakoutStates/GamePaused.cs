using DIKUArcade.Input;
using DIKUArcade.State;

namespace Breakout {
    public class GamePaused : IGameState {

        private static GamePaused instance = null;

        public GamePaused() {
            // endGameText
            endGameText = new Text(string.Format("Game Over!"), new Vec2F(0.5f, 0.5f), new Vec2F(0.5f, 0.5f));
            endGameText.SetColor(new Vec3I(0, 128, 255));
        }

        public static IGameState GetInstance() {
            return instance ?? (instance = new GamePaused());
        }


        public void RenderState() {
            // render background image
            // render menu buttons
            // new game & quit
        }

        public void ResetState() {

        }

        public void UpdateState() {
        }

        public void HandleKeyEvent(KeyboardAction keyboardAction, KeyboardKey keyboardKey) {
        }
    }
}