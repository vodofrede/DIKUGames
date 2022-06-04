using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;

namespace Breakout.BreakoutStates {
    public class GameOver : IGameStateExt {
        private EventBus eventBus;

        // fields
        private int score = 0;
        private bool won = true;

        // visuals
        private Entity backGroundImage = new(
            new DynamicShape(new Vec2F(-1.0f, -1.0f), new Vec2F(2.0f, 2.0f)),
            new Image(Path.Combine("Assets", "Images", "BreakoutTitleScreen.png"))
        );

        private TextDisplay statusText = new();
        private TextDisplay menuButtons = new();
        private GameOverButton activeMenuButton = GameOverButton.MainMenu;

        public GameOver() {
            eventBus = EventBus.GetInstance();

            // status text
            statusText.AddTextField(new TextField(() => "Game Over!", new Vec2F(0.17f, 0.5f), new Vec2F(0.8f, 0.4f)));

            var outcomeText = new TextField(() => "You " + (won ? "Won!" : "Lost!"), new Vec2F(0.18f, 0.4f), new Vec2F(0.8f, 0.4f));
            outcomeText.Behaviors.Add(() => _ = won ? outcomeText.SetColor(0, 255, 0) : outcomeText.SetColor(255, 165, 0));
            statusText.AddTextField(outcomeText);
            statusText.AddTextField(new TextField(() => "Score: " + score, new Vec2F(0.2f, 0.3f), new Vec2F(0.3f, 0.4f)));

            // menu buttons
            var mainMenuButton = new TextField(() => "Main Menu", new Vec2F(0.2f, 0.2f), new Vec2F(0.2f, 0.2f));
            mainMenuButton.Behaviors.Add(() => _ = activeMenuButton == GameOverButton.MainMenu ? mainMenuButton.SetColor(255, 0, 0) : mainMenuButton.SetColor(255, 255, 255));
            menuButtons.AddTextField(mainMenuButton);

            var exitButton = new TextField(() => "Exit", new Vec2F(0.2f, 0.1f), new Vec2F(0.2f, 0.2f));
            exitButton.Behaviors.Add(() => _ = activeMenuButton == GameOverButton.Exit ? exitButton.SetColor(255, 0, 0) : exitButton.SetColor(255, 255, 255));
            menuButtons.AddTextField(exitButton);
        }

        /// <summary>
        /// RenderText the Game State
        /// </summary>
        public void RenderState() {
            // render background image
            backGroundImage.RenderEntity();
            statusText.RenderText();
            menuButtons.RenderText();
        }

        /// <summary>
        /// Reset the Game State
        /// </summary>
        public void ResetState() { }

        public void SetState(object extraState) {
            dynamic state = extraState;
            score = state.Score;
            won = state.Won;
        }

        /// <summary>
        /// Update the Game State
        /// </summary>
        public void UpdateState() {

        }

        /// <summary>
        /// Key Event Handler
        /// </summary>
        public void HandleKeyEvent(KeyboardAction keyboardAction, KeyboardKey keyboardKey) {
            switch (keyboardAction) {
                case KeyboardAction.KeyPress:
                    switch (keyboardKey) {
                        case KeyboardKey.Escape: eventBus.RegisterEvent(GameEventType.WindowEvent, this, "CLOSE_WINDOW"); break;
                        case KeyboardKey.Up: activeMenuButton = activeMenuButton.Prev(); break;
                        case KeyboardKey.Down: activeMenuButton = activeMenuButton.Next(); break;
                        case KeyboardKey.Enter:
                            switch (activeMenuButton) {
                                case GameOverButton.MainMenu:
                                    eventBus.RegisterEvent(GameEventType.GameStateEvent, this, "SWITCH_STATE", "MainMenu");
                                    break;
                                case GameOverButton.Exit:
                                    eventBus.RegisterEvent(GameEventType.WindowEvent, this, "CLOSE_WINDOW");
                                    break;
                            }
                            break;
                        default: break;
                    }
                    break;
                default: break;
            }
        }
    }

    enum GameOverButton {
        MainMenu,
        Exit
    }

    static class GameOverButtonExt {
        public static GameOverButton Prev(this GameOverButton button) {
            var index = (int)button;
            return (GameOverButton)Math.Max(0, index - 1);
        }

        public static GameOverButton Next(this GameOverButton button) {
            var all = typeof(GameOverButton).GetEnumValues();
            var index = (int)button;
            return (GameOverButton)Math.Min(all.Length - 1, index + 1);
        }
    }
}