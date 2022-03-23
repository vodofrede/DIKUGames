using System.IO;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Math;
using DIKUArcade.State;

namespace Galaga.GalagaStates;

public class MainMenu : IGameState {

    private static MainMenu instance = null;

    private Entity backGroundImage = new Entity(
        new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(1.0f, 1.0f)),
        new Image(Path.Combine("Assets", "Images", "TitleImage.png"))
        );
    private Text[] menuButtons = new Text[] {
        new Text(string.Format("New Game"), new Vec2F(0.5f, 0.5f), new Vec2F(0.1f, 0.1f)),
        new Text(string.Format("Quit"), new Vec2F(0.5f, 0.4f), new Vec2F(0.1f, 0.1f))
    };
    private int activeMenuButton;
    private int maxMenuButtons;

    public static IGameState GetInstance() {
        return instance ?? (instance = new MainMenu());
    }

    public void RenderState() {


        // render background image
        backGroundImage.RenderEntity();

        // render menu buttons
        for (int i = 0; i < menuButtons.Length; i++)
        {

            if (i == activeMenuButton)
            {
                menuButtons[i].SetColor(new Vec3I(255, 0, 0));
            }

            else
            {
                menuButtons[i].SetColor(new Vec3I(255, 255, 255));
            }

            menuButtons[i].SetFontSize(1000);
            menuButtons[i].RenderText();
        }
    }


    public void ResetState() {

    }

    public void UpdateState() {

        for (int i = 0; i < menuButtons.Length; i++)
        {

            if (i == activeMenuButton)
            {
                menuButtons[i].SetColor(new Vec3I(255, 0, 0));
            }

            else
            {
                menuButtons[i].SetColor(new Vec3I(255, 255, 255));
            }

            menuButtons[i].RenderText();
        }


    }

    public void HandleKeyEvent(KeyboardAction action, KeyboardKey key) {
        switch (action)
        {
            case KeyboardAction.KeyPress:
                KeyPress(key);
                break;
        }
    }

    public void KeyPress(KeyboardKey keyboardKey) {
        switch (keyboardKey)
        {


            case KeyboardKey.Escape:
                GalagaBus.GetBus().RegisterEvent(new GameEvent {
                    EventType = GameEventType.WindowEvent,
                    From = this,
                    Message = "CLOSE_WINDOW"
                });
                break;

            case KeyboardKey.Up:
                if (activeMenuButton < menuButtons.Length - 1)
                {
                    activeMenuButton++;
                }
                else
                {
                    activeMenuButton = 0;
                }
                UpdateState();
                break;

            case KeyboardKey.Down:
                if (activeMenuButton > 0)
                {
                    activeMenuButton--;
                }
                else
                {
                    activeMenuButton = menuButtons.Length - 1;
                }
                UpdateState();
                break;

            case KeyboardKey.Enter:
                if (activeMenuButton == 0)
                {
                    GalagaBus.GetBus().RegisterEvent(
                        new GameEvent {
                            EventType = GameEventType.GameStateEvent,
                            From = this,
                            Message = "CHANGE_STATE",
                            StringArg1 = "GAME_RUNNING"
                        });
                }
                else if (activeMenuButton == 1)
                {
                    GalagaBus.GetBus().RegisterEvent(
                        new GameEvent {
                            EventType = GameEventType.WindowEvent,
                            From = this,
                            Message = "CHANGE_STATE",
                            StringArg1 = "CLOSE_WINDOW"
                        });
                }
                break;

        }
    }
}
