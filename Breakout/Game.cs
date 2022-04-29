using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Math;

namespace Breakout {

    public class Game : DIKUGame, IGameEventProcessor {
        private Player player;
        private Map map;

        public Game(WindowArgs windowArgs) : base(windowArgs) {
            // event bus
            GameEventBus eventBus = BreakoutBus.GetBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.PlayerEvent, GameEventType.WindowEvent });
            // player setup
            player = new Player(new Image(Path.Combine("Assets", "Images", "player.png")));
            eventBus.Subscribe(GameEventType.PlayerEvent, player);
            eventBus.Subscribe(GameEventType.InputEvent, this);
            eventBus.Subscribe(GameEventType.WindowEvent, this);

            // load map
            map = FileLoader.TryParseFile(Path.Combine("Assets", "Levels", "level2.txt"));

            window.SetKeyEventHandler(HandleKeyEvent);
        }

        public void ProcessEvent(GameEvent gameEvent) {
            switch (gameEvent.EventType) {
                case GameEventType.WindowEvent:
                    switch (gameEvent.Message) {
                        case "CLOSE_WINDOW":
                            window.CloseWindow();
                            break;
                    }
                    break;
            }
        }

        public override void Render() {
            window.Clear();
            player.RenderEntity();
            map.RenderMap();
        }

        public override void Update() {
            player.Move();
            window.PollEvents();
            BreakoutBus.GetBus().ProcessEventsSequentially();
        } 

        public void HandleKeyEvent(KeyboardAction keyboardAction, KeyboardKey keyboardKey) {
            switch (keyboardAction) {
                case KeyboardAction.KeyPress:
                    KeyPress(keyboardKey);
                    break;
                case KeyboardAction.KeyRelease:
                    KeyRelease(keyboardKey);
                    break;
            }
        }

        public void KeyPress(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Right:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent,
                        From = this,
                        To = player,
                        Message = "START_MOVE_RIGHT"
                    });
                    break;
                case KeyboardKey.Left:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent,
                        From = this,
                        To = player,
                        Message = "START_MOVE_LEFT"
                    });
                    break;
            }
        }

        public void KeyRelease(KeyboardKey key) {
            switch (key) {
                case KeyboardKey.Right:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent,
                        From = this,
                        To = player,
                        Message = "STOP_MOVE_RIGHT"
                    });
                    break;

                case KeyboardKey.Left:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.PlayerEvent,
                        From = this,
                        To = player,
                        Message = "STOP_MOVE_LEFT"
                    });
                    break;

                case KeyboardKey.Escape:
                    BreakoutBus.GetBus().RegisterEvent(new GameEvent {
                        EventType = GameEventType.WindowEvent,
                        From = this,
                        Message = "CLOSE_WINDOW"
                    });
                    break;
            }
        }
    }
}

