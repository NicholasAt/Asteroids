using Assets.Game.Scripts.Data.Configs.Enemy;
using Assets.Game.Scripts.Gameplay.Interfaces.Common;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Interfaces.Services.Pool
{
    public interface IEnemyPool
    {
        IEnemyBehaviour Get(EnemyType enemyType, Vector2 position, Vector2 direction);
    }
}