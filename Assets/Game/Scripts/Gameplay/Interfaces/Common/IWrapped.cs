using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Interfaces.Common
{
    public interface IWrapped
    {
        Vector2 Position { get; }
        void Wrap(Vector2 newPosition);
    }
}