using Assets.Game.Scripts.Core.Common;
using Assets.Game.Scripts.Data.Configs.Enemy;
using Assets.Game.Scripts.Gameplay.Interfaces.Common;
using Assets.Game.Scripts.Gameplay.Interfaces.Services;
using System.Collections.Generic;

namespace Assets.Game.Scripts.Gameplay.Services.Register
{
    public class EnemyRegistry : IEnemyRegistry
    {
        private const int InitialCapacity = 16;

        private readonly List<IEnemyBehaviour> _enemies = new(InitialCapacity);
        private readonly Dictionary<EnemyType, int> _countByType = new();

        public int Count => _enemies.Count;

        public int GetCount(EnemyType enemyType)
        {
            return _countByType.TryGetValue(enemyType, out int count) ? count : 0;
        }

        public void Add(IEnemyBehaviour enemy)
        {
            _enemies.Add(enemy);
            _countByType[enemy.EnemyType] = GetCount(enemy.EnemyType) + 1;
        }

        public void Remove(IEnemyBehaviour enemy)
        {
            if (CollectionExtension.RemoveFrom(_enemies, enemy))
                _countByType[enemy.EnemyType] = GetCount(enemy.EnemyType) - 1;
        }
    }
}