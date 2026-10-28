using Assets.Scripts.Data.Enemy;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Signals
{
    public readonly struct EnemyDeathSignal
    {
        public readonly EnemyType EnemyType;
        public readonly Vector2 Position;
        public EnemyDeathSignal(EnemyType enemyType, Vector2 position)
        {
            EnemyType = enemyType;
            Position = position;
        }
    }
}