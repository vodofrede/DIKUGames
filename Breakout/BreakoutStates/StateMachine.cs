using DIKUArcade.Events;
using DIKUArcade.State;

namespace Breakout.BreakoutStates {

    public class StateMachine : IGameEventProcessor {

        public IGameState ActiveState { get; private set; }

        public StateMachine() {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            _ = GameRunning.GetInstance();
            _ = GamePaused.GetInstance();
            _ = GameOver.GetInstance();


            // følgende statement forhindrer en lint warning
            ActiveState = MainMenu.GetInstance();
            // SwitchState(GameStateType.MainMenu);
        }

        /// <summary>
        /// Process incoming events
        /// </summary>
        public void ProcessEvent(GameEvent gameEvent) {
            switch (gameEvent.EventType) {
                case GameEventType.GameStateEvent:
                    switch (gameEvent.Message) {
                        case "GAME_OVER":
                            SwitchState(GameStateType.GameOver);
                            break;
                        case "GAME_RUNNING":
                            SwitchState(GameStateType.GameRunning);
                            break;
                        case "MAIN_MENU":
                            SwitchState(GameStateType.MainMenu);
                            break;
                        case "GAME_PAUSED":
                            SwitchState(GameStateType.GamePaused);
                            break;
                    }
                    break;
                case GameEventType.InputEvent:
                    if (gameEvent.Message == "CLOSE_WINDOW") {
                        BreakoutBus.GetBus().RegisterEvent(
                            new GameEvent {
                                EventType = GameEventType.GameStateEvent,
                                From = this,
                                Message = "CLOSE_WINDOW"
                            }
                        );
                    }
                    break;
                default:
                    break;
            }
        }

        /// <summary>
        /// Switch the state of the state machine
        /// </summary>
        private void SwitchState(GameStateType stateType) {
            switch (stateType) {
                case GameStateType.GameOver:
                    ActiveState = GameOver.GetInstance();
                    break;
                case GameStateType.MainMenu:
                    ActiveState = MainMenu.GetInstance();
                    break;
                case GameStateType.GameRunning:
                    ActiveState = GameRunning.GetInstance();
                    break;
                case GameStateType.GamePaused:
                    ActiveState = GamePaused.GetInstance();
                    break;
                default:
                    break;
            }
        }
    }
}
