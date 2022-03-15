using System.Collections.Generic;
using System.Diagnostics.Tracing;
using System.Net.NetworkInformation;
using DIKUArcade.Entities;
using DIKUArcade.Graphics;
using DIKUArcade.Math;

namespace Galaga.Squadron;

public class RoundSquadron : ISquadron {
    private EntityContainer<Enemy> enemies = new EntityContainer<Enemy>();
    private int maxEnemies = 9;
    
    public EntityContainer<Enemy> Enemies { 
        get{ return enemies;}
    }
    public int MaxEnemies { 
        get { return maxEnemies; }
    }

    public void CreateEnemies(List<Image> enemyStride, List<Image> alternativeEnemyStride) {
        Vec2F[] posArray = { 
            new Vec2F(0.5f, 0.9f), 
            new Vec2F(0.4f, 0.8f), 
            new Vec2F(0.5f, 0.8f), 
            new Vec2F(0.6f, 0.8f), 
            new Vec2F(0.5f, 0.7f),
        };

        foreach (var pos in posArray) {
            enemies.AddEntity(new Enemy(
                new DynamicShape(pos.X, pos.Y, 0.1f, 0.1f),
                new ImageStride(80, enemyStride),
                new ImageStride(80, alternativeEnemyStride)
            ));
        }

    }
}