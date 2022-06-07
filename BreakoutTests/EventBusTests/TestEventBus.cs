using System.IO;
using Breakout;
using DIKUArcade.Events;
using DIKUArcade.Graphics;
using DIKUArcade.GUI;
using NUnit.Framework;

namespace BreakoutTests {
    public class TestEventBus {
        EventBus eventBus = EventBus.GetInstance();

        [SetUp]
        public void Setup() {
            Window.CreateOpenGLContext();

            eventBus = EventBus.GetInstance();
        }

        [Test]
        public void TestRegisterEvent() {
            var player = new Player(new Image(Path.Combine("Assets", "Images", "player.png")));
            eventBus.Subscribe(GameEventType.PlayerEvent, player);

            eventBus.RegisterEvent(GameEventType.PlayerEvent, this, "IT WORKS");
            eventBus.ProcessEventsSequentially();
        }
    }
}