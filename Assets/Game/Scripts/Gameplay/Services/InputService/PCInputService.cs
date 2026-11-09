using Assets.Game.Scripts.Gameplay.Interfaces.Services;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Services.InputService
{
    public class PCInputService : IInput
    {
        public Vector2 MoveAxis => GetMoveAxis();

        public bool IsAttack1 => Input.GetKey(KeyCode.Mouse0);
        public bool IsAttack2 => Input.GetKey(KeyCode.Mouse1);

        private Vector2 GetMoveAxis()
        {
            Vector2 axis = new(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
            return Vector2.ClampMagnitude(axis, 1);
        }
    }
}