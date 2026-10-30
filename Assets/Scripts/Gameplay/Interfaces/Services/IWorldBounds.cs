using Assets.Scripts.Gameplay.Interfaces.Common;

namespace Assets.Scripts.Gameplay.Interfaces.Services
{
    public interface IWorldBounds
    {
        void AddInMap(IWrapped wrappers);
        void AddOutMap(IWrapped wrappers);
        void Remove(IWrapped wrappers);
    }
}