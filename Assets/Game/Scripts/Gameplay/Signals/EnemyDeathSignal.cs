using Assets.Game.Scripts.Data.Configs.Enemy;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Signals
{
    public readonly struct EnemyDeathSignal
    {
        public readonly EnemyType EnemyType;
        public readonly Vector2 Position;
        public readonly bool SpawnFragments;
        public EnemyDeathSignal(EnemyType enemyType, Vector2 position, bool spawnFragments)
        {
            EnemyType = enemyType;
            Position = position;
            SpawnFragments = spawnFragments;
        }
    }
}