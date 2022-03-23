using System;
using DIKUArcade.Entities;

namespace Galaga.MovementStrategy {
    public class ZigZagDown : IMovementStrategy {

        // private float s = 0.0003f;
        private readonly float p = 0.045f;
        private readonly float a = 0.005f;

        public void MoveEnemy(Enemy enemy) {
            // float previousPosition = enemy.Shape.Position.Y;
            enemy.Shape.Position.Y -= enemy.Speed;
            enemy.Shape.Position.X = enemy.StartingPosition.X + (a * (float)Math.Sin(2 * Math.PI * (enemy.StartingPosition.Y + enemy.Shape.Position.Y) / p));
        }

        public void MoveEnemies(EntityContainer<Enemy> enemies) {
            foreach (Enemy enemy in enemies) {
                MoveEnemy(enemy);
            }
        }
    }
}