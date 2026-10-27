using Assets.Scripts.Gameplay.Physics;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Common
{
    public class TransformView : MonoBehaviour
    {
        private PhysicsBody _physicsBody;
        [SerializeField] private bool _q;

        public void Init(PhysicsBody physicsBody)
        {
            _physicsBody = physicsBody;
        }

        private void LateUpdate()
        {
            float angle = _physicsBody.Rotation * Mathf.Rad2Deg;
            transform.SetPositionAndRotation(_physicsBody.Position, Quaternion.Euler(0, 0, angle));
        }
    }
}