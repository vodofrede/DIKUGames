using DIKUArcade.Events;
using DIKUArcade.State;
using DIKUArcade.Input;
using System;

namespace Galaga.GalagaStates {

public class StateMachine : IGameEventProcessor {

    public IGameState ActiveState { get; private set; }

    public StateMachine() {
        GalagaBus.GetBus().Subscribe(GameEventType.GameStateEvent, this);
        GalagaBus.GetBus().Subscribe(GameEventType.InputEvent, this);
        ActiveState = MainMenu.GetInstance();
        GameRunning.GetInstance();
        GamePaused.GetInstance();
    }

    public void ProcessEvent(GameEvent gameEvent) {

        switch (gameEvent.EventType) {
            case GameEventType.GameStateEvent:
                if (gameEvent.StringArg1 == "GAME_RUNNING") {
                    if (ActiveState != GameRunning.GetInstance()) {
                        ActiveState = GameRunning.GetInstance();
                    } 
                }

                if (gameEvent.StringArg1 == "MAIN_MENU") {
                    if (ActiveState != MainMenu.GetInstance()) {
                        ActiveState = GameRunning.GetInstance();                     
                    }
                }

                if (gameEvent.Message == "GAME_PAUSED") {
                    if (ActiveState != GamePaused.GetInstance()) {
                        ActiveState = GamePaused.GetInstance();                 
                    }
                }                

                break;

            case GameEventType.InputEvent:
                if (gameEvent.Message == "CLOSE_WINDOW") {
                        GalagaBus.GetBus().RegisterEvent(
                            new GameEvent {
                                EventType = GameEventType.GameStateEvent,
                                From = this,
                                Message = "CHANGE_STATE",
                                StringArg1 = "CLOSE_WINDOW"
                            }
                        );
                    }

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
        }
    }   
}
}
