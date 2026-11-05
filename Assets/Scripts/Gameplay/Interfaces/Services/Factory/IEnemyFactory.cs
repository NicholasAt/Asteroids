using Assets.Scripts.Data.Configs.Enemy;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Interfaces.Services
{
    public interface IEnemyFactory
    {
        void Create(EnemyType enemyType, Vector2 position, Vector2 direction);
    }
}