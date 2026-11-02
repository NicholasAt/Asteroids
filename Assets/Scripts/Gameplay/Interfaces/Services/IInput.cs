using UnityEngine;

namespace Assets.Scripts.Gameplay.Interfaces.Services
{
    public interface IInput
    {
        Vector2 MoveAxis { get; }
        bool IsAttack1 {  get; }
        bool IsAttack2 {  get; }
    }
}