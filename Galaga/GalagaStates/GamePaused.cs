using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.State;

namespace Galaga.GalagaStates {
    public class GamePaused : IGameState {

        private static GamePaused instance = null;

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