using Assets.Scripts.Gameplay.Physics;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.Bullet
{
    public class BulletView : MonoBehaviour
    {
        private PhysicsBody _physicsBody;
        private PhysicsWorld _physicsWorld;

        [Inject]
        private void Construct(PhysicsWorld physicsWorld)
        {
            _physicsWorld = physicsWorld;
        }

        public void Init(PhysicsBody physicsBody)
        {
            _physicsBody = physicsBody;
        }

        private void OnDestroy()
        {
            _physicsWorld.Remove(_physicsBody);
        }
    }
}