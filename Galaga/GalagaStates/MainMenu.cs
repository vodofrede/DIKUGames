using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;

namespace Galaga.GalagaStates {
    public class MainMenu : IGameState {

        private static MainMenu instance;

        private readonly Entity backGroundImage = new(
            new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(1.0f, 1.0f)),
            new Image(Path.Combine("Assets", "Images", "TitleImage.png"))
            );
        private readonly Text[] menuButtons = new Text[] {
        new Text(string.Format("New Game"), new Vec2F(0.5f, 0.5f), new Vec2F(0.1f, 0.1f)),
        new Text(string.Format("Quit"), new Vec2F(0.5f, 0.4f), new Vec2F(0.1f, 0.1f))
    };
        private int activeMenuButton;

        public static IGameState GetInstance() {
            return instance ?? (instance = new MainMenu());
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

        public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
            switch (action) {
                case KeyboardAction.KeyPress:
                    KeyPress(key);
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
                    GalagaBus.GetBus().RegisterEvent(new GameEvent {
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
                        GalagaBus.GetBus().RegisterEvent(
                            new GameEvent {
                                EventType = GameEventType.GameStateEvent,
                                From = this,
                                Message = "CHANGE_STATE",
                                StringArg1 = "GAME_RUNNING"
                            });
                    } else if (activeMenuButton == 1) {
                        GalagaBus.GetBus().RegisterEvent(
                            new GameEvent {
                                EventType = GameEventType.WindowEvent,
                                From = this,
                                Message = "CHANGE_STATE",
                                StringArg1 = "CLOSE_WINDOW"
                            });
                    }
                    break;
                case KeyboardKey.Unknown:
                    break;
                case KeyboardKey.Space:
                    break;
                case KeyboardKey.Apostrophe:
                    break;
                case KeyboardKey.Comma:
                    break;
                case KeyboardKey.Minus:
                    break;
                case KeyboardKey.Plus:
                    break;
                case KeyboardKey.Period:
                    break;
                case KeyboardKey.Slash:
                    break;
                case KeyboardKey.Num_0:
                    break;
                case KeyboardKey.Num_1:
                    break;
                case KeyboardKey.Num_2:
                    break;
                case KeyboardKey.Num_3:
                    break;
                case KeyboardKey.Num_4:
                    break;
                case KeyboardKey.Num_5:
                    break;
                case KeyboardKey.Num_6:
                    break;
                case KeyboardKey.Num_7:
                    break;
                case KeyboardKey.Num_8:
                    break;
                case KeyboardKey.Num_9:
                    break;
                case KeyboardKey.Semicolon:
                    break;
                case KeyboardKey.Equal:
                    break;
                case KeyboardKey.A:
                    break;
                case KeyboardKey.B:
                    break;
                case KeyboardKey.C:
                    break;
                case KeyboardKey.D:
                    break;
                case KeyboardKey.E:
                    break;
                case KeyboardKey.F:
                    break;
                case KeyboardKey.G:
                    break;
                case KeyboardKey.H:
                    break;
                case KeyboardKey.I:
                    break;
                case KeyboardKey.J:
                    break;
                case KeyboardKey.K:
                    break;
                case KeyboardKey.L:
                    break;
                case KeyboardKey.M:
                    break;
                case KeyboardKey.N:
                    break;
                case KeyboardKey.O:
                    break;
                case KeyboardKey.P:
                    break;
                case KeyboardKey.Q:
                    break;
                case KeyboardKey.R:
                    break;
                case KeyboardKey.S:
                    break;
                case KeyboardKey.T:
                    break;
                case KeyboardKey.U:
                    break;
                case KeyboardKey.V:
                    break;
                case KeyboardKey.W:
                    break;
                case KeyboardKey.X:
                    break;
                case KeyboardKey.Y:
                    break;
                case KeyboardKey.Z:
                    break;
                case KeyboardKey.LeftBracket:
                    break;
                case KeyboardKey.Backslash:
                    break;
                case KeyboardKey.RightBracket:
                    break;
                case KeyboardKey.GraveAccent:
                    break;
                case KeyboardKey.AcuteAccent:
                    break;
                case KeyboardKey.Tab:
                    break;
                case KeyboardKey.Backspace:
                    break;
                case KeyboardKey.Insert:
                    break;
                case KeyboardKey.Delete:
                    break;
                case KeyboardKey.Right:
                    break;
                case KeyboardKey.Left:
                    break;
                case KeyboardKey.PageUp:
                    break;
                case KeyboardKey.PageDown:
                    break;
                case KeyboardKey.Home:
                    break;
                case KeyboardKey.End:
                    break;
                case KeyboardKey.CapsLock:
                    break;
                case KeyboardKey.ScrollLock:
                    break;
                case KeyboardKey.NumLock:
                    break;
                case KeyboardKey.PrintScreen:
                    break;
                case KeyboardKey.Pause:
                    break;
                case KeyboardKey.F1:
                    break;
                case KeyboardKey.F2:
                    break;
                case KeyboardKey.F3:
                    break;
                case KeyboardKey.F4:
                    break;
                case KeyboardKey.F5:
                    break;
                case KeyboardKey.F6:
                    break;
                case KeyboardKey.F7:
                    break;
                case KeyboardKey.F8:
                    break;
                case KeyboardKey.F9:
                    break;
                case KeyboardKey.F10:
                    break;
                case KeyboardKey.F11:
                    break;
                case KeyboardKey.F12:
                    break;
                case KeyboardKey.F13:
                    break;
                case KeyboardKey.F14:
                    break;
                case KeyboardKey.F15:
                    break;
                case KeyboardKey.F16:
                    break;
                case KeyboardKey.F17:
                    break;
                case KeyboardKey.F18:
                    break;
                case KeyboardKey.F19:
                    break;
                case KeyboardKey.F20:
                    break;
                case KeyboardKey.F21:
                    break;
                case KeyboardKey.F22:
                    break;
                case KeyboardKey.F23:
                    break;
                case KeyboardKey.F24:
                    break;
                case KeyboardKey.F25:
                    break;
                case KeyboardKey.KeyPad0:
                    break;
                case KeyboardKey.KeyPad1:
                    break;
                case KeyboardKey.KeyPad2:
                    break;
                case KeyboardKey.KeyPad3:
                    break;
                case KeyboardKey.KeyPad4:
                    break;
                case KeyboardKey.KeyPad5:
                    break;
                case KeyboardKey.KeyPad6:
                    break;
                case KeyboardKey.KeyPad7:
                    break;
                case KeyboardKey.KeyPad8:
                    break;
                case KeyboardKey.KeyPad9:
                    break;
                case KeyboardKey.KeyPadDecimal:
                    break;
                case KeyboardKey.KeyPadDivide:
                    break;
                case KeyboardKey.KeyPadMultiply:
                    break;
                case KeyboardKey.KeyPadSubtract:
                    break;
                case KeyboardKey.KeyPadAdd:
                    break;
                case KeyboardKey.KeyPadEnter:
                    break;
                case KeyboardKey.KeyPadEqual:
                    break;
                case KeyboardKey.LeftShift:
                    break;
                case KeyboardKey.LeftControl:
                    break;
                case KeyboardKey.LeftAlt:
                    break;
                case KeyboardKey.LeftSuper:
                    break;
                case KeyboardKey.RightShift:
                    break;
                case KeyboardKey.RightControl:
                    break;
                case KeyboardKey.RightAlt:
                    break;
                case KeyboardKey.RightSuper:
                    break;
                case KeyboardKey.Menu:
                    break;
                case KeyboardKey.Diaresis:
                    break;
                case KeyboardKey.LessThan:
                    break;
                case KeyboardKey.GreaterThan:
                    break;
                case KeyboardKey.Backslash_161:
                    break;
                case KeyboardKey.FractionOneHalf:
                    break;
                case KeyboardKey.Danish_AA:
                    break;
                case KeyboardKey.Danish_AE:
                    break;
                case KeyboardKey.Danish_OE:
                    break;
                case KeyboardKey.LastKey:
                    break;
                default:
                    break;
            }
        }
    }
}