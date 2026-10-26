using UnityEngine;

namespace Assets.Scripts.Gameplay.Services.InputService
{
    public class PCInputService : IInputService
    {
        public Vector2 MoveAxis => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        
        public bool IsAttack1 => Input.GetKey(KeyCode.Space);
        public bool IsAttack2 => Input.GetKey(KeyCode.LeftControl);
    }
}