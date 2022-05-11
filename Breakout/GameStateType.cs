using System;

namespace Breakout {
    public enum GameStateType {
        GameRunning,
        GamePaused,
        MainMenu,
    }


    public class StateTransformer {
        /// <summary>
        /// Transform a string to a game state
        /// </summary>
        public static GameStateType TransformStringToState(string state) {
            return state switch {
                "GAME_RUNNING" => GameStateType.GameRunning,
                "GAME_PAUSED" => GameStateType.GamePaused,
                "MAIN_MENU" => GameStateType.MainMenu,
                _ => throw new ArgumentException("Wrong string mate"),
            };
        }

        /// <summary>
        /// Transform game state to a string representation
        /// </summary>
        public static string TransformStateToString(GameStateType state) {
            return state switch {
                GameStateType.GameRunning => "GAME_RUNNING",
                GameStateType.GamePaused => "GAME_PAUSED",
                GameStateType.MainMenu => "MAIN_MENU",
                _ => throw new ArgumentException("Input to TransformStateToString was null"),
            };
        }
    }
}