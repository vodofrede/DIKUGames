using System.Collections.Generic;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;

namespace Galaga.Squadron;

public class SquareSquadron : ISquadron {
    private EntityContainer<Enemy> enemies = new EntityContainer<Enemy>();
    private readonly int maxEnemies = 9;

    public EntityContainer<Enemy> Enemies { get { return enemies;} }
    public int MaxEnemies { get { return maxEnemies;} }

    public void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
        // first row
        enemies.AddEntity(new Enemy(
            new DynamicShape(0.4f, 0.9f, 0.1f, 0.1f),
            new ImageStride(80, enemyStride),
            new ImageStride(80, alternativeEnemyStride)
        ));
        enemies.AddEntity(new Enemy(
            new DynamicShape(0.4f, 0.9f, 0.1f, 0.1f),
            new ImageStride(80, enemyStride),
            new ImageStride(80, alternativeEnemyStride)
        ));
        enemies.AddEntity(new Enemy(
            new DynamicShape(0.6f, 0.9f, 0.1f, 0.1f),
            new ImageStride(80, enemyStride),
            new ImageStride(80, alternativeEnemyStride)
        ));

        // second row
        enemies.AddEntity(new Enemy(
            new DynamicShape(0.4f, 0.8f, 0.1f, 0.1f),
            new ImageStride(80, enemyStride),
            new ImageStride(80, alternativeEnemyStride)
        ));
        enemies.AddEntity(new Enemy(
            new DynamicShape(0.5f, 0.8f, 0.1f, 0.1f),
            new ImageStride(80, enemyStride),
            new ImageStride(80, alternativeEnemyStride)
        ));
        enemies.AddEntity(new Enemy(
            new DynamicShape(0.6f, 0.8f, 0.1f, 0.1f),
            new ImageStride(80, enemyStride),
            new ImageStride(80, alternativeEnemyStride)
        ));

        // last row
        enemies.AddEntity(new Enemy(
            new DynamicShape(0.4f, 0.7f, 0.1f, 0.1f),
            new ImageStride(80, enemyStride),
            new ImageStride(80, alternativeEnemyStride)
        ));
        enemies.AddEntity(new Enemy(
            new DynamicShape(0.5f, 0.7f, 0.1f, 0.1f),
            new ImageStride(80, enemyStride),
            new ImageStride(80, alternativeEnemyStride)
        ));
        enemies.AddEntity(new Enemy(
            new DynamicShape(0.6f, 0.7f, 0.1f, 0.1f),
            new ImageStride(80, enemyStride),
            new ImageStride(80, alternativeEnemyStride)
        ));
    }
}