using Assets.Scripts.Data.Enemy;
using Assets.Scripts.Gameplay.Interfaces.Common;
using System.Collections.Generic;
using Zenject;

namespace Assets.Scripts.Gameplay.Interfaces.Services
{
    public interface IEnemyRegistry : ITickable
    {
        void Add(bool isPursuing, IEnemyBehaviour enemy);
        void Remove(IEnemyBehaviour enemy);
        bool TryGetNotPursuing(out IReadOnlyList<IEnemyBehaviour> enemies);
        bool TryGetPursuing(out IReadOnlyList<IEnemyBehaviour> enemies);
    }
}