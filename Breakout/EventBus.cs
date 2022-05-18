using DIKUArcade.Events;
using DIKUArcade.Timers;

namespace Breakout {
    public class EventBus {
        protected GameEventBus eventBus;

        // safer wrapper around game event bus that doesn't need initialize function
        public EventBus() {
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

        public void RegisterTimedEvent(GameEvent gameEvent, TimePeriod timePeriod) {
            eventBus.RegisterTimedEvent(gameEvent, timePeriod);
        }

        public void RegisterEvent(GameEvent gameEvent) {
            eventBus.RegisterEvent(gameEvent);
        }
    }
}