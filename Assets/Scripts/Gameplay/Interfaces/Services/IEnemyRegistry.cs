using Assets.Scripts.Gameplay.Interfaces.Common;
using System.Collections.Generic;
using Zenject;

namespace Assets.Scripts.Gameplay.Interfaces.Services
{
    public interface IEnemyRegistry : ITickable
    {
        IReadOnlyList<IEnemyBehaviour> NotPursuing { get; }
        void Add(bool isPursuing, IEnemyBehaviour enemy);
        void Remove(IEnemyBehaviour enemy);
    }
}