using Assets.Scripts.Data.Configs.Enemy;
using Assets.Scripts.Gameplay.Interfaces.Common;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Interfaces.Services.Pool
{
    public interface IEnemyPool
    {
        IEnemyBehaviour Get(EnemyType enemyType, Vector2 position, Vector2 direction);
    }
}