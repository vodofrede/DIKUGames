using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga {
    public class Enemy : Entity {
        public Enemy(DynamicShape shape, IBaseImage image)
            : base(shape, image) {}
    }
}
