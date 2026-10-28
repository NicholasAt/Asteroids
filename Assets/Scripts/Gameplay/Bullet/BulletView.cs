using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Physics;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.Bullet
{
    public class BulletView : MonoBehaviour, IInflictDamage
    {
        private const int Damage = 0;

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
            _physicsBody.Owner = this;
            _physicsBody.OnCollision += OnCollistion;
        }

        public float GetDamage()
        {
            return Damage;
        }

        private void OnDestroy()
        {
            _physicsBody.OnCollision -= OnCollistion;
            _physicsWorld.Remove(_physicsBody);
        }

        private void OnCollistion(object obj)
        {
            Destroy(gameObject);
        }
    }
}