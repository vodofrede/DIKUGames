using Breakout.BreakoutStates;
using DIKUArcade;
using DIKUArcade.Events;
using DIKUArcade.GUI;
using DIKUArcade.Input;

namespace Breakout {
    /// <summary>
    /// Game Class which implements DIKUGame and acts as the main container for the state and the logic of the game.
    /// </summary
    public class Game : DIKUGame, IGameEventProcessor {
        private EventBus eventBus;
        private StateMachine stateMachine;

        public Game(WindowArgs windowArgs) : base(windowArgs) {
            // state machine initialization
            stateMachine = new StateMachine();
            stateMachine.AddState(new MainMenu());
            stateMachine.AddState(new GameRunning());
            stateMachine.AddState(new GamePaused());
            stateMachine.AddState(new GameOver());

            // event bus
            eventBus = EventBus.GetInstance();
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

