using UnityEngine;

namespace Assets.Scripts.Gameplay.Interfaces.Common
{
    public interface IPlayerPosition
    {
        Vector2 PlayerPosition { get; }
        void SetPosition(Vector2 position);
    }
}