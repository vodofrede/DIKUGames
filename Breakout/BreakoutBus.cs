using DIKUArcade.Events;

namespace Breakout {
    public class BreakoutBus {
        private static GameEventBus? eventBus;

        /// <summary>
        /// Get the singleton instance
        /// </summary>
        public static GameEventBus GetBus() {
            return eventBus ?? (eventBus = new GameEventBus());
        }
    }
}