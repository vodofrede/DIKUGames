using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga.Squadron {
    public class RoundSquadron : ISquadron {
        public EntityContainer<Enemy> Enemies { get; } = new EntityContainer<Enemy>();
        public int MaxEnemies { get; } = 5;

        public void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride, float speed) {
            Vec2F[] posArray = {
            new Vec2F(0.5f, 0.9f),
            new Vec2F(0.4f, 0.8f),
            new Vec2F(0.5f, 0.8f),
            new Vec2F(0.6f, 0.8f),
            new Vec2F(0.5f, 0.7f),
        };

            foreach (Vec2F pos in posArray) {
                Enemies.AddEntity(new Enemy(
                    new DynamicShape(pos.X, pos.Y, 0.1f, 0.1f),
                    new ImageStride(80, enemyStride),
                    new ImageStride(80, alternativeEnemyStride),
                    speed
                ));
            }

        }
    }
}