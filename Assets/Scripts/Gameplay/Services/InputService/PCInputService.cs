using Assets.Scripts.Gameplay.Interfaces.Services;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Services.InputService
{
    public class PCInputService : IInput
    {
        public Vector2 MoveAxis => GetMoveAxis();

        public bool IsAttack1 => Input.GetKey(KeyCode.Space);
        public bool IsAttack2 => Input.GetKey(KeyCode.LeftControl);

        private Vector2 GetMoveAxis()
        {
            Vector2 axis = new(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            return Vector2.ClampMagnitude(axis, 1);
        }
    }
}