using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;

namespace Breakout.BreakoutStates {
    public class MainMenu : IGameState {
        private EventBus eventBus;

        // visuals
        private Entity backgroundImage = new(
            new DynamicShape(new Vec2F(-1.0f, -1.0f), new Vec2F(2.0f, 2.0f)),
            new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"))
        );
        private TextDisplay menuButtons = new();
        private MainMenuButton activeMenuButton = MainMenuButton.Play;

        public MainMenu(EventBus eventBus) {
            this.eventBus = eventBus;

            // text on-screen
            var playButton = new TextField(() => "New Game", new Vec2F(0.2f, 0.5f), new Vec2F(0.2f, 0.2f));
            playButton.Behaviors.Add(() => { if (activeMenuButton == MainMenuButton.Play) { playButton.SetColor(255, 0, 0); } else { playButton.SetColor(255, 255, 255); } });
            menuButtons.AddTextField(playButton);

            var quitButton = new TextField(() => "Exit", new Vec2F(0.2f, 0.4f), new Vec2F(0.2f, 0.2f));
            playButton.Behaviors.Add(() => { if (activeMenuButton == MainMenuButton.Exit) { playButton.SetColor(255, 0, 0); } else { playButton.SetColor(255, 255, 255); } });
            menuButtons.AddTextField(quitButton);
        }

        /// <summary>
        /// RenderText the State
        /// </summary>
        public void RenderState() {
            // render background image
            backgroundImage.RenderEntity();
            menuButtons.RenderText();
        }

        public void UpdateState() { }

        /// <summary>
        /// Reset the State
        /// </summary>
        public void ResetState() {
            activeMenuButton = MainMenuButton.Play;
        }

        /// <summary>
        /// Update the State
        /// </summary>

        /// <summary>
        /// Handle Key Events
        /// </summary>
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            switch (action) {
                case KeyboardAction.KeyPress:
                    switch (key) {
                        case KeyboardKey.Escape: eventBus.RegisterEvent(GameEventType.WindowEvent, this, "CLOSE_WINDOW"); break;
                        case KeyboardKey.Up: activeMenuButton = activeMenuButton.Prev(); break;
                        case KeyboardKey.Down: activeMenuButton = activeMenuButton.Next(); break;
                        case KeyboardKey.Enter:
                            switch (activeMenuButton) {
                                case MainMenuButton.Play:
                                    eventBus.RegisterEvent(GameEventType.GameStateEvent, this, "RESET_STATE", "GameRunning");
                                    eventBus.RegisterEvent(GameEventType.GameStateEvent, this, "SWITCH_STATE", "GameRunning");
                                    break;
                                case MainMenuButton.Exit:
                                    eventBus.RegisterEvent(GameEventType.WindowEvent, this, "CLOSE_WINDOW");
                                    break;
                            }
                            break;
                    }
                    break;
                default:
                    break;
            }
        }

    }

    enum MainMenuButton {
        Play,
        Exit
    }

    static class MainMenuButtonExt {
        public static MainMenuButton Prev(this MainMenuButton button) {
            var index = (int)button;
            return (MainMenuButton)Math.Max(0, index - 1);
        }

        public static MainMenuButton Next(this MainMenuButton button) {
            var all = typeof(MainMenuButton).GetEnumValues();
            var index = (int)button;
            return (MainMenuButton)Math.Min(all.Length - 1, index + 1);
        }
    }
}