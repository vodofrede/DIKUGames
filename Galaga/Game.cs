using System;
using System.Collections.Generic;
using System.IO;
using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Input;
using DIKUArcade.Physics;
using Galaga.GalagaStates;

namespace Galaga {
    public class Game : DIKUGame, IGameEventProcessor {
        // game state
        private StateMachine stateMachine;

        public Game(WindowArgs windowArgs) : base(windowArgs) {
            // state machine
            stateMachine = new StateMachine();

            // event bus
            var eventBus = GalagaBus.GetBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.PlayerEvent, GameEventType.WindowEvent, GameEventType.GameStateEvent });
            eventBus.Subscribe(GameEventType.WindowEvent, this);
            eventBus.Subscribe(GameEventType.GameStateEvent, stateMachine);

            // key input forwarding
            window.SetKeyEventHandler(KeyHandler);
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

        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            stateMachine.ActiveState.HandleKeyEvent(action, key);
        }

        public override void Render() {
            window.Clear();
            stateMachine.ActiveState.RenderState();
        }

        public override void Update() {
            window.PollEvents();
            GalagaBus.GetBus().ProcessEventsSequentially();
            stateMachine.ActiveState.UpdateState();
        }
    }
}
