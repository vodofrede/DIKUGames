using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;
using DIKUArcade.Entities;
using DIKUArcade.Events;

namespace Breakout {
    public class GamePaused : IGameState {

        private static GamePaused? instance;

        private Text endGameText;

        private readonly Entity backGroundImage = new(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(1.0f, 1.0f)),
            new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"))
        );
        private readonly Text[] menuButtons = new Text[] {
            new Text(string.Format("Continue"), new Vec2F(0.5f, 0.5f), new Vec2F(0.1f, 0.1f)),
            new Text(string.Format("Quit"), new Vec2F(0.5f, 0.4f), new Vec2F(0.1f, 0.1f))
        };
        private int activeMenuButton;

        public GamePaused() {
        }

        public static IGameState GetInstance() {
            return instance ?? (instance = new GamePaused());
        }

        public void RenderState() {
            // render background image
            backGroundImage.RenderEntity();
            
            // render menu buttons
            for (int i = 0; i < menuButtons.Length; i++) {

                if (i == activeMenuButton) {
                    menuButtons[i].SetColor(new Vec3I(255, 0, 0));
                } else {
                    menuButtons[i].SetColor(new Vec3I(255, 255, 255));
                }

                menuButtons[i].SetFontSize(1000);
                menuButtons[i].RenderText();
            }
            // continue or quit
        }

        public void ResetState() {

        }

        public void UpdateState() {
            for (int i = 0; i < menuButtons.Length; i++) {
                if (i == activeMenuButton) {
                    menuButtons[i].SetColor(new Vec3I(255, 0, 0));
                } else {
                    menuButtons[i].SetColor(new Vec3I(255, 255, 255));
                }

                menuButtons[i].RenderText();
            }
        }

        public void HandleKeyEvent(KeyboardAction keyboardAction, KeyboardKey keyboardKey) {
            switch (keyboardAction) {
                case KeyboardAction.KeyPress:
                    KeyPress(keyboardKey);
                    break;
                case KeyboardAction.KeyRelease:
                    break;
                default:
                    break;
            }
        }

        public void KeyPress(KeyboardKey keyboardKey) {
            switch (keyboardKey) {
                case KeyboardKey.Escape:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.WindowEvent,
                        From = this,
                        Message = "CLOSE_WINDOW"
                    });
                    break;

                case KeyboardKey.Up:
                    if (activeMenuButton < menuButtons.Length - 1) {
                        activeMenuButton++;
                    } else {
                        activeMenuButton = 0;
                    }
                    UpdateState();
                    break;

                case KeyboardKey.Down:
                    if (activeMenuButton > 0) {
                        activeMenuButton--;
                    } else {
                        activeMenuButton = menuButtons.Length - 1;
                    }
                    UpdateState();
                    break;

                case KeyboardKey.Enter:
                    if (activeMenuButton == 0) {
                        BreakoutBus.GetBus().RegisterEvent(
                            new GameEvent {
                                EventType = GameEventType.GameStateEvent,
                                From = this,
                                Message = "GAME_RUNNING",
                            });
                    } else if (activeMenuButton == 1) {
                        BreakoutBus.GetBus().RegisterEvent(
                            new GameEvent {
                                EventType = GameEventType.WindowEvent,
                                From = this,
                                Message = "CLOSE_WINDOW",
                            });
                    }
                    break;
                default:
                    break;
            }
        }
    }
}