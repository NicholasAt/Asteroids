using UnityEngine;

namespace Assets.Scripts.Gameplay.Services.InputService
{

    public class PCInputService : IInputService
    {
        public Vector2 MoveAxis => new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
    }
}