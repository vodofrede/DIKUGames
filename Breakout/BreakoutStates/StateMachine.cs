using DIKUArcade.Events;
using DIKUArcade.State;

namespace Breakout.BreakoutStates {
    /// <summary>
    /// State Machine class which acts by switching between active states using messages passed by the event bus.
    /// The state machine will call the relevant update, render etc. functions on the active game state.
    /// This class also contains
    /// </summary
    public class StateMachine : IGameEventProcessor {
        protected EventBus eventBus;
        protected Dictionary<string, IGameStateExt> states;
        public IGameState? ActiveState { get; private set; }

        public StateMachine() {
            eventBus = EventBus.GetInstance();

            eventBus.Subscribe(GameEventType.GameStateEvent, this);
            eventBus.Subscribe(GameEventType.InputEvent, this);

            states = new();
        }

        /// <summary>
        /// Adds a state and sets it to active if it is the first state to be added.
        /// </summary>
        public void AddState(IGameStateExt gameState) {
            string name = gameState.GetType().Name;

            if (ActiveState == null && states.Count == 0) {
                ActiveState = gameState;
            }

            states.Add(name, gameState);
        }

        /// <summary>
        /// Process incoming events
        /// </summary>
        public void ProcessEvent(GameEvent gameEvent) {
            switch (gameEvent.EventType) {
                case GameEventType.GameStateEvent:
                    switch (gameEvent.Message) {
                        case "SWITCH_STATE": SwitchState(gameEvent.StringArg1); break;
                        case "RESET_STATE": states[gameEvent.StringArg1]?.ResetState(); break;
                        case "SET_STATE": states[gameEvent.StringArg1]?.SetState(gameEvent.ObjectArg1); break;
                    }
                    break;
                case GameEventType.InputEvent:
                    if (gameEvent.Message == "CLOSE_WINDOW") {
                        eventBus.RegisterEvent(GameEventType.GameStateEvent, this, "CLOSE_WINDOW");
                    }
                    break;
                default: break;
            }
        }

        /// <summary>
        /// Switch the state of the state machine
        /// </summary>
        public void SwitchState(string name) {
            ActiveState = states[name] ?? ActiveState;
        }
    }
}
