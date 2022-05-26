using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;

namespace Breakout.BreakoutStates {
    public class GamePaused : IGameStateExt {
        private EventBus eventBus;

        private Entity backGroundImage = new(
            new DynamicShape(new Vec2F(-1.0f, -1.0f), new Vec2F(2.0f, 2.0f)),
            new Image(Path.Combine("Assets", "Images", "BreakoutTitleScreen.png"))
        );

        private TextDisplay menuButtons = new();
        private GamePausedButton activeMenuButton = GamePausedButton.Continue;

        public GamePaused(EventBus eventBus) {
            this.eventBus = eventBus;

            // buttons
            var continueButton = new TextField(() => "Continue", new Vec2F(0.2f, 0.6f), new Vec2F(0.2f, 0.2f));
            continueButton.Behaviors.Add(() => _ = activeMenuButton == GamePausedButton.Continue ? continueButton.SetColor(255, 0, 0) : continueButton.SetColor(255, 255, 255));
            menuButtons.AddTextField(continueButton);

            var mainMenuButton = new TextField(() => "Main Menu", new Vec2F(0.2f, 0.5f), new Vec2F(0.2f, 0.2f));
            mainMenuButton.Behaviors.Add(() => _ = activeMenuButton == GamePausedButton.MainMenu ? mainMenuButton.SetColor(255, 0, 0) : mainMenuButton.SetColor(255, 255, 255));
            menuButtons.AddTextField(mainMenuButton);

            var exitButton = new TextField(() => "Exit", new Vec2F(0.2f, 0.4f), new Vec2F(0.2f, 0.2f));
            exitButton.Behaviors.Add(() => _ = activeMenuButton == GamePausedButton.Exit ? exitButton.SetColor(255, 0, 0) : exitButton.SetColor(255, 255, 255));
            menuButtons.AddTextField(exitButton);
        }

        /// <summary>
        /// RenderText the Game State
        /// </summary>
        public void RenderState() {
            // render background image
            backGroundImage.RenderEntity();
            menuButtons.RenderText();
        }

        /// <summary>
        /// Reset the Game State
        /// </summary>
        public void ResetState() {
            activeMenuButton = GamePausedButton.Continue;
        }

        /// <summary>
        /// Update the Game State
        /// </summary>
        public void UpdateState() { }

        /// <summary>
        /// Ingest variables from other state
        /// </summary>
        public void SetState(object extraState) { }

        /// <summary>
        /// Key Event Handler
        /// </summary>
        public void HandleKeyEvent(KeyboardAction keyboardAction, KeyboardKey keyboardKey) {
            switch (keyboardAction) {
                case KeyboardAction.KeyPress:
                    switch (keyboardKey) {
                        case KeyboardKey.Escape:
                            eventBus.RegisterEvent(GameEventType.WindowEvent, this, "CLOSE_WINDOW");
                            break;
                        case KeyboardKey.Up:
                            activeMenuButton = activeMenuButton.Prev();
                            break;
                        case KeyboardKey.Down:
                            activeMenuButton = activeMenuButton.Next();
                            break;
                        case KeyboardKey.Enter:
                            switch (activeMenuButton) {
                                case GamePausedButton.Continue:
                                    eventBus.RegisterEvent(GameEventType.GameStateEvent, this, "SWITCH_STATE", "GameRunning");
                                    break;
                                case GamePausedButton.MainMenu:
                                    eventBus.RegisterEvent(GameEventType.GameStateEvent, this, "SWITCH_STATE", "MainMenu");
                                    break;
                                case GamePausedButton.Exit:
                                    eventBus.RegisterEvent(GameEventType.WindowEvent, this, "CLOSE_WINDOW");
                                    break;
                            }
                            break;
                        default:
                            break;
                    }
                    break;
                default:
                    break;
            }
        }
    }

    enum GamePausedButton {
        Continue,
        MainMenu,
        Exit,
    }

    static class MenuButtonExt {
        public static GamePausedButton Prev(this GamePausedButton button) {
            var index = (int)button;
            return (GamePausedButton)Math.Max(0, index - 1);
        }

        public static GamePausedButton Next(this GamePausedButton button) {
            var all = typeof(GamePausedButton).GetEnumValues();
            var index = (int)button;
            return (GamePausedButton)Math.Min(all.Length - 1, index + 1);
        }
    }
}