using Assets.Scripts.Data.Configs.Enemy;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Signals
{
    public readonly struct EnemyDeathSignal
    {
        public readonly EnemyType EnemyType;
        public readonly Vector2 Position;
        public readonly bool IsSplit;
        public EnemyDeathSignal(EnemyType enemyType, Vector2 position, bool isSplit)
        {
            EnemyType = enemyType;
            Position = position;
            IsSplit = isSplit;
        }
    }
    public readonly struct PlayerDeathSignal
    {

    }
}