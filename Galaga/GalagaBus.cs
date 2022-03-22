using DIKUArcade.Events;

namespace Galaga;

public class GalagaBus {
    private static GameEventBus eventBus;

    public static GameEventBus GetBus() {
        return GalagaBus.eventBus ?? (GalagaBus.eventBus = new GameEventBus());
    }
}