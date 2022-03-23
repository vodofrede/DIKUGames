using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Galaga.Squadron {
    public class SquareSquadron : ISquadron {
        public EntityContainer<Enemy> Enemies { get; } = new EntityContainer<Enemy>();
        public int MaxEnemies { get; } = 9;

        public void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride, float speed) {
            // first row
            Enemies.AddEntity(new Enemy(
                new DynamicShape(0.4f, 0.9f, 0.1f, 0.1f),
                new ImageStride(80, enemyStride),
                new ImageStride(80, alternativeEnemyStride),
                speed
            ));
            Enemies.AddEntity(new Enemy(
                new DynamicShape(0.4f, 0.9f, 0.1f, 0.1f),
                new ImageStride(80, enemyStride),
                new ImageStride(80, alternativeEnemyStride),
                speed
            ));
            Enemies.AddEntity(new Enemy(
                new DynamicShape(0.6f, 0.9f, 0.1f, 0.1f),
                new ImageStride(80, enemyStride),
                new ImageStride(80, alternativeEnemyStride),
                speed
            ));

            // second row
            Enemies.AddEntity(new Enemy(
                new DynamicShape(0.4f, 0.8f, 0.1f, 0.1f),
                new ImageStride(80, enemyStride),
                new ImageStride(80, alternativeEnemyStride),
                speed
            ));
            Enemies.AddEntity(new Enemy(
                new DynamicShape(0.5f, 0.8f, 0.1f, 0.1f),
                new ImageStride(80, enemyStride),
                new ImageStride(80, alternativeEnemyStride),
                speed
            ));
            Enemies.AddEntity(new Enemy(
                new DynamicShape(0.6f, 0.8f, 0.1f, 0.1f),
                new ImageStride(80, enemyStride),
                new ImageStride(80, alternativeEnemyStride),
                speed
            ));

            // last row
            Enemies.AddEntity(new Enemy(
                new DynamicShape(0.4f, 0.7f, 0.1f, 0.1f),
                new ImageStride(80, enemyStride),
                new ImageStride(80, alternativeEnemyStride),
                speed
            ));
            Enemies.AddEntity(new Enemy(
                new DynamicShape(0.5f, 0.7f, 0.1f, 0.1f),
                new ImageStride(80, enemyStride),
                new ImageStride(80, alternativeEnemyStride),
                speed
            ));
            Enemies.AddEntity(new Enemy(
                new DynamicShape(0.6f, 0.7f, 0.1f, 0.1f),
                new ImageStride(80, enemyStride),
                new ImageStride(80, alternativeEnemyStride),
                speed
            ));
        }
    }
}