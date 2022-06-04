using DIKUArcade.State;

namespace Breakout.BreakoutStates {
    /// <summary>
    /// Extension to the IGameState interface which allows raw state passing between implementors.
    /// This allows a greater degree of separation as Game States don't need contain instances of each other to modify state.
    /// </summary
    public interface IGameStateExt : IGameState {
        public abstract void SetState(object extraState);
    }
}