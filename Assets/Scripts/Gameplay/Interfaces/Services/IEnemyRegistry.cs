using Assets.Scripts.Gameplay.Interfaces.Common;
using Zenject;

namespace Assets.Scripts.Gameplay.Interfaces.Services
{
    public interface IEnemyRegistry : ITickable
    {
        void Add(IEnemyBehaviour enemy);
        void Remove(IEnemyBehaviour enemy);
    }
}