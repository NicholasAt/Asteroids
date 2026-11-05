using Assets.Scripts.Data.Configs.Enemy;
using Assets.Scripts.Gameplay.Interfaces.Common;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Interfaces.Services
{
    public interface IEnemyFactory
    {
        IEnemyBehaviour Create(EnemyType enemyType);
    }
}