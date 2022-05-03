using DIKUArcade.Events;
using DIKUArcade.State;

namespace Breakout {

    public class StateMachine : IGameEventProcessor {

        public IGameState ActiveState { get; private set; }

        public StateMachine() {
            BreakoutBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
            BreakoutBus.GetBus().Subscribe(GameEventType.InputEvent, this);
            SwitchState(GameStateType.MainMenu);
            _ = GameRunning.GetInstance();
            _ = GamePaused.GetInstance();
        }

        public void ProcessEvent(GameEvent gameEvent) {

            switch (gameEvent.EventType) {
                case GameEventType.GameStateEvent:
                    if (gameEvent.StringArg1 == "GAME_RUNNING") {
                        SwitchState(GameStateType.GameRunning);
                    }

                    if (gameEvent.StringArg1 == "MAIN_MENU") {
                        SwitchState(GameStateType.MainMenu);
                    }

                    if (gameEvent.StringArg1 == "GAME_PAUSED") {
                        SwitchState(GameStateType.GamePaused);
                    }

                    break;

                case GameEventType.InputEvent:
                    if (gameEvent.Message == "CLOSE_WINDOW") {
                        BreakoutBus.GetBus().RegisterEvent(
                            new GameEvent {
                                EventType = GameEventType.GameStateEvent,
                                From = this,
                                Message = "CHANGE_STATE",
                                StringArg1 = "CLOSE_WINDOW"
                            }
                        );
                    }
                    break;
                default:
                    break;
            }
        }

        private void SwitchState(GameStateType stateType) {
            switch (stateType) {
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
