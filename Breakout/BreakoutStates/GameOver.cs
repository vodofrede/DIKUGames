using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;

namespace Breakout {
    public class GameOver : IGameState {
        private static GameOver? instance;
        public uint Score = 0;
        public bool WonGame = false;

        private readonly Entity backGroundImage = new(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(1.0f, 1.0f)),
            new Image(Path.Combine("Assets", "Images", "SpaceBackground.png"))
        );

        // TODO: Should display if the game was won or lost and the score
        private readonly Text gameWonText = new Text(string.Format("The game is over! You won"), new Vec2F(0.5f, 0.7f), new Vec2F(0.2f, 0.2f));
        private readonly Text gameLostText = new Text(string.Format("The game is over! You lost"), new Vec2F(0.5f, 0.7f), new Vec2F(0.2f, 0.2f));


        private readonly Text[] menuButtons = new Text[] {
            new Text(string.Format("Go to Main Menu"), new Vec2F(0.5f, 0.5f), new Vec2F(0.2f, 0.2f)),
            new Text(string.Format("Quit"), new Vec2F(0.5f, 0.4f), new Vec2F(0.2f, 0.2f))
        };

        private int activeMenuButton = 0;

        /// <summary>
        /// Get the singleton instance of the MainMenu
        /// </summary>
        public static IGameState GetInstance() {
            return instance ?? (instance = new GameOver());
        }

        /// <summary>
        /// Render the Game State
        /// </summary>
        public void RenderState() {
            // render background image
            backGroundImage.RenderEntity();

            if (WonGame) {
                gameWonText.RenderText();
            } else {
                gameLostText.RenderText();
            }
            Text scoreText = new Text(string.Format("Your score was: " + Score), new Vec2F(0.5f, 0.6f), new Vec2F(0.2f, 0.2f));
            scoreText.RenderText();

            // render menu buttons
            for (int i = 0; i < menuButtons.Length; i++) {

                if (i == activeMenuButton) {
                    menuButtons[i].SetColor(new Vec3I(255, 0, 0));
                } else {
                    menuButtons[i].SetColor(new Vec3I(255, 255, 255));
                }

                menuButtons[i].SetFontSize(3000);
                menuButtons[i].RenderText();
            }
            // continue or quit
        }

        /// <summary>
        /// Reset the Game State
        /// </summary>
        public void ResetState() {

        }

        /// <summary>
        /// Update the Game State
        /// </summary>
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

        /// <summary>
        /// Key Event Handler
        /// </summary>
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

        /// <summary>
        /// Key Press Handler
        /// </summary>
        public void KeyPress(KeyboardKey keyboardKey) {
            switch (keyboardKey) {
                case KeyboardKey.Escape:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.WindowEvent,
                        From = this,
                        Message = "CLOSE_WINDOW"
                    });
                    break;

                case KeyboardKey.Down:
                    if (activeMenuButton < menuButtons.Length - 1) {
                        activeMenuButton++;
                    } else {
                        activeMenuButton = 0;
                    }
                    UpdateState();
                    break;

                case KeyboardKey.Up:
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
                                Message = "MAIN_MENU",
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