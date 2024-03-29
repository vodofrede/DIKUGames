using DIKUArcade.Events;
using DIKUArcade.Timers;

namespace Breakout {
    /// <summary>
    /// Event Bus singleton class for passing events throughout the game
    /// </summary
    public class EventBus {
        protected GameEventBus eventBus;
        private static EventBus? instance;

        public static EventBus GetInstance() {
            return instance ?? (instance = new EventBus());
        }

        // safer wrapper around game event bus that doesn't need initialize function
        private EventBus() {
            eventBus = new GameEventBus();
            eventBus.InitializeEventBus(new List<GameEventType> {
                GameEventType.PlayerEvent,
                GameEventType.GraphicsEvent,
                GameEventType.InputEvent,
                GameEventType.ControlEvent,
                GameEventType.MovementEvent,
                GameEventType.SoundEvent,
                GameEventType.StatusEvent,
                GameEventType.GameStateEvent,
                GameEventType.WindowEvent,
                GameEventType.TimedEvent
            });
        }

        public void ProcessEventsSequentially() {
            eventBus.ProcessEventsSequentially();
        }

        public void Subscribe(GameEventType eventType, IGameEventProcessor gameEventProcessor) {
            eventBus.Subscribe(eventType, gameEventProcessor);
        }

        public void Unsubscribe(GameEventType eventType, IGameEventProcessor gameEventProcessor) {
            eventBus.Unsubscribe(eventType, gameEventProcessor);
        }

        public void RegisterEvent(GameEvent gameEvent) {
            eventBus.RegisterEvent(gameEvent);
        }

        public void RegisterEvent(GameEventType eventType, object from, string message, string stringArg1 = "", object? objectArg1 = null) {
            RegisterEvent(new GameEvent {
                EventType = eventType,
                From = from,
                Message = message,
                StringArg1 = stringArg1,
                ObjectArg1 = objectArg1,
            });
        }

        public void RegisterTimedEvent(GameEvent gameEvent, TimePeriod timePeriod) {
            eventBus.RegisterTimedEvent(gameEvent, timePeriod);
        }

        public void RegisterTimedEvent(object from, string message, long timeoutMilliseconds) {
            RegisterTimedEvent(new GameEvent {
                EventType = GameEventType.TimedEvent,
                From = from,
                Message = message,
            }, TimePeriod.NewMilliseconds(timeoutMilliseconds));
        }

        public void AddOrResetTimedEvent(GameEvent gameEvent, TimePeriod timePeriod) {
            eventBus.AddOrResetTimedEvent(gameEvent, timePeriod);
        }

        public void AddOrResetTimedEvent(object from, string message, long timeoutMilliseconds) {
            AddOrResetTimedEvent(new GameEvent {
                EventType = GameEventType.TimedEvent,
                From = from,
                Message = message,
            }, TimePeriod.NewMilliseconds(timeoutMilliseconds));
        }
    }
}