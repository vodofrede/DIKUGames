using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;

namespace Breakout.BreakoutStates {
    enum MenuButton {
        Play,
        Exit
    }

    static class MenuButtonExt {
        public static MenuButton Prev(this MenuButton button) {
            var index = (int)button;
            return (MenuButton)Math.Max(0, index - 1);
        }

        public static MenuButton Next(this MenuButton button) {
            var all = typeof(MenuButton).GetEnumValues();
            var index = (int)button;
            return (MenuButton)Math.Min(all.Length - 1, index + 1);
        }
    }

    public class MainMenu : IGameState {
        private EventBus eventBus;

        private Entity backgroundImage = new(
            new DynamicShape(new Vec2F(-1.0f, -1.0f), new Vec2F(2.0f, 2.0f)),
            new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"))
        );
        private List<Text> buttons = new() {
            new Text(string.Format("New Game"), new Vec2F(0.5f, 0.5f), new Vec2F(0.2f, 0.2f)),
            new Text(string.Format("Quit"), new Vec2F(0.5f, 0.4f), new Vec2F(0.2f, 0.2f))
        };

        private MenuButton activeMenuButton;

        public MainMenu(EventBus eventBus) {
            this.eventBus = eventBus;
            activeMenuButton = MenuButton.Play;

            // workaround DIKUArcade bug
            foreach (var button in buttons) {
                button.SetFontSize(1000);
            }
        }

        /// <summary>
        /// Render the State
        /// </summary>
        public void RenderState() {
            // render background image
            backgroundImage.RenderEntity();

            foreach (var button in buttons) {
                button.RenderText();
            }
        }

        private void UpdateTextColor() {
            // render menu buttons
            for (int i = 0; i < buttons.Count; i++) {
                if (i == (int)activeMenuButton) {
                    buttons[i].SetColor(new Vec3I(255, 0, 0));
                } else {
                    buttons[i].SetColor(new Vec3I(255, 255, 255));
                }
            }
        }

        /// <summary>
        /// Reset the State
        /// </summary>
        public void ResetState() {
            activeMenuButton = MenuButton.Play;
        }

        /// <summary>
        /// Update the State
        /// </summary>
        public void UpdateState() { }

        /// <summary>
        /// Handle Key Events
        /// </summary>
        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            switch (action) {
                case KeyboardAction.KeyPress:
                    switch (key) {
                        case KeyboardKey.Escape:
                            eventBus.RegisterEvent(new GameEvent {
                                EventType = GameEventType.WindowEvent,
                                From = this,
                                Message = "CLOSE_WINDOW"
                            });
                            break;
                        case KeyboardKey.Up:
                            activeMenuButton = activeMenuButton.Prev();
                            UpdateTextColor();
                            break;
                        case KeyboardKey.Down:
                            activeMenuButton = activeMenuButton.Next();
                            UpdateTextColor();
                            break;
                        case KeyboardKey.Enter:
                            switch (activeMenuButton) {
                                case MenuButton.Play:
                                    eventBus.RegisterEvent(new GameEvent {
                                        EventType = GameEventType.GameStateEvent,
                                        From = this,
                                        Message = "RESET_STATE",
                                        StringArg1 = "GameRunning",
                                    });
                                    eventBus.RegisterEvent(new GameEvent {
                                        EventType = GameEventType.GameStateEvent,
                                        From = this,
                                        Message = "SWITCH_STATE",
                                        StringArg1 = "GameRunning",
                                    });
                                    break;
                                case MenuButton.Exit:
                                    eventBus.RegisterEvent(new GameEvent {
                                        EventType = GameEventType.WindowEvent,
                                        From = this,
                                        Message = "CLOSE_WINDOW",
                                    });
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
}