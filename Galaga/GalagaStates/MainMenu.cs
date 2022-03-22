using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Input;

namespace Galaga.GalagaStates {
    public class MainMenu : IGameState {
        
        private static MainMenu instance = null;
        private Entity backGroundImage;
        private Text[] menuButtons;
        private int activeMenuButton;
        private int maxMenuButtons;
    
        public static IGameState GetInstance() {
            throw null;
        }

        public void ResetState() {
        }

        public void UpdateState() {
        }

        public void RenderState() {
        }

        public void HandleKeyEvent(KeyboardAction keyboardAction, KeyboardKey keyboardKey) {
        }
    }
}