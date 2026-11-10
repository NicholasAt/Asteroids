using Assets.Game.Scripts.Data.Configs.Enemy;
using Assets.Game.Scripts.Gameplay.Interfaces.Common;

namespace Assets.Game.Scripts.Gameplay.Interfaces.Services
{
    public interface IEnemyRegistry
    {
        int Count { get; }
        int GetCount(EnemyType enemyType);
        void Add(IEnemyBehaviour enemy);
        void Remove(IEnemyBehaviour enemy);
    }
}