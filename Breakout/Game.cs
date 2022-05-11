using Breakout.BreakoutStates;
using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Math;

namespace Breakout {

    public class Game : DIKUGame, IGameEventProcessor {
        private StateMachine stateMachine;

        public Game(WindowArgs windowArgs) : base(windowArgs) {
            stateMachine = new StateMachine();

            // event bus
            GameEventBus eventBus = BreakoutBus.GetBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.PlayerEvent, GameEventType.WindowEvent, GameEventType.GameStateEvent });
            eventBus.Subscribe(GameEventType.WindowEvent, this);
            eventBus.Subscribe(GameEventType.GameStateEvent, stateMachine);

            // key input forwarding
            window.SetKeyEventHandler(KeyHandler);
        }

        public void ProcessEvent(GameEvent gameEvent) {
            switch (gameEvent.EventType) {
                // case GameEventType.GameStateEvent:
                //     switch(gameEvent.Message) {
                //         case "PAUSE_GAME":
                //             // stateMachine.ActiveState == 
                //             break;
                //     }
                //     break;
                case GameEventType.WindowEvent:
                    switch (gameEvent.Message) {
                        case "CLOSE_WINDOW":
                            window.CloseWindow();
                            break;
                        
                    }
                    break;
            }
        }

        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            stateMachine.ActiveState.HandleKeyEvent(action, key);
        }

        public override void Render() {
            window.Clear();
            stateMachine.ActiveState.RenderState();
        }

        public override void Update() {
            window.PollEvents();
            BreakoutBus.GetBus().ProcessEventsSequentially();
            stateMachine.ActiveState.UpdateState();
        }
    }
}

