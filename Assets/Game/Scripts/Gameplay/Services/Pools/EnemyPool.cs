using Assets.Game.Scripts.Data.Configs.Enemy;
using Assets.Game.Scripts.Gameplay.Common;
using Assets.Game.Scripts.Gameplay.Interfaces.Common;
using Assets.Game.Scripts.Gameplay.Interfaces.Services.Factory;
using Assets.Game.Scripts.Gameplay.Interfaces.Services.Pool;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Services.Pools
{
    public class EnemyPool : IEnemyPool
    {
        private readonly Dictionary<EnemyType, ObjectsPool<IEnemyBehaviour>> _pool;
        private readonly IEnemyFactory _enemyFactory;

        public EnemyPool(IEnemyFactory enemyFactory)
        {
            _enemyFactory = enemyFactory;
            _pool = new Dictionary<EnemyType, ObjectsPool<IEnemyBehaviour>>();
        }

        public IEnemyBehaviour Get(EnemyType enemyType, Vector2 position, Vector2 direction)
        {
            if (_pool.TryGetValue(enemyType, out ObjectsPool<IEnemyBehaviour> pool) == false)
            {
                pool = new ObjectsPool<IEnemyBehaviour>(() =>
                {
                    return NewEnemy(enemyType);
                });
                _pool.Add(enemyType, pool);
            }
            IEnemyBehaviour enemy = pool.Pop();
            enemy.Show(position, direction);
            return enemy;
        }
        private IEnemyBehaviour NewEnemy(EnemyType enemyType)
        {
            IEnemyBehaviour enemy = _enemyFactory.Create(enemyType);
            ObjectsPool<IEnemyBehaviour> pool = _pool[enemyType];
            enemy.OnDie += () => pool.Return(enemy);
            return enemy;
        }
    }
}