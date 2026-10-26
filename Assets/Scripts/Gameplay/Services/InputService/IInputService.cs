using UnityEngine;

namespace Assets.Scripts.Gameplay.Services.InputService
{
    public interface IInputService
    {
        Vector2 MoveAxis { get; }
    }
}