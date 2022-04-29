using DIKUArcade;
using DIKUArcade.Entities;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using DIKUArcade.Math;

namespace Breakout {

    public class Game : DIKUGame {
        private Player player;
        private Map map;

        public Game(WindowArgs windowArgs) : base(windowArgs) {
            
            // event bus
            GameEventBus eventBus = BreakoutBus.GetBus();
            eventBus.InitializeEventBus(new List<GameEventType> { GameEventType.InputEvent, GameEventType.PlayerEvent, GameEventType.WindowEvent, GameEventType.GameStateEvent });
            // player setup
            player = new Player(
                new DynamicShape(new Vec2F(0.45f, 0.1f), new Vec2F(0.1f, 0.1f)),
                new Image(Path.Combine("Assets", "Images", "player.png"))
            );
            eventBus.Subscribe(GameEventType.PlayerEvent, player);

            // load map
            map = FileLoader.ParseFile(Path.Combine("Assets", "Levels", "level2.txt"));
        }

        public override void Render() {
            player.Render();
            map.RenderMap();
        }

        public override void Update() {

        }
    }
}

