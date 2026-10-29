using UnityEngine;

namespace Assets.Scripts.Gameplay.Interfaces.Services
{
    public interface ICharacterRegister
    {
        Vector2 PlayerPosition { get; }
        void SetPosition(Vector2 position);
    }
}