using Breakout.BreakoutStates;
using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Input;

namespace Breakout {
    public class Game : DIKUGame, IGameEventProcessor {
        private EventBus eventBus;
        private StateMachine stateMachine;

        public Game(WindowArgs windowArgs) : base(windowArgs) {
            eventBus = new EventBus();
            stateMachine = new StateMachine(eventBus);

            stateMachine.AddState(new MainMenu(eventBus));
            stateMachine.AddState(new GameRunning(eventBus));
            stateMachine.AddState(new GamePaused(eventBus));
            stateMachine.AddState(new GameOver(eventBus));

            // event bus
            eventBus.Subscribe(GameEventType.WindowEvent, this);
            eventBus.Subscribe(GameEventType.GameStateEvent, stateMachine);

            // key input forwarding
            window.SetKeyEventHandler(KeyHandler);
        }

        /// <summary>
        /// Process game events
        /// </summary>
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

        /// <summary>
        /// Set the key handler
        /// </summary>
        private void KeyHandler(KeyboardAction action, KeyboardKey key) {
            stateMachine.ActiveState?.HandleKeyEvent(action, key);
        }

        /// <summary>
        /// RenderText the game using the current state
        /// </summary>
        public override void Render() {
            window.Clear();
            stateMachine.ActiveState?.RenderState();
        }

        /// <summary>
        /// Update the game using the current state
        /// </summary>
        public override void Update() {
            window.PollEvents();
            eventBus.ProcessEventsSequentially();
            stateMachine.ActiveState?.UpdateState();
        }
    }
}

