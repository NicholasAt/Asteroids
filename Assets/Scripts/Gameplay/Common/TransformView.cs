using Assets.Scripts.Gameplay.Physics;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Common
{
    public class TransformView : MonoBehaviour
    {
        private PhysicsBody _physicsBody;

        public void Init(PhysicsBody physicsBody)
        {
            _physicsBody = physicsBody;
        }

        private void LateUpdate()
        {
            transform.position = _physicsBody.Position;
        }
    }
}