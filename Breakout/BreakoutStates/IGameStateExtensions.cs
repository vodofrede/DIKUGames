using DIKUArcade.State;

namespace Breakout.BreakoutStates {
    public interface IGameStateExt : IGameState {
        public abstract void SetState(object extraState);
    }
}