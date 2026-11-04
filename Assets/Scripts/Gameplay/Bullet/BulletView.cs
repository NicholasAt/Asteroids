using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Physics;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.Bullet
{
    public class BulletView : MonoBehaviour
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

        private void OnDestroy()
        {
            _physicsBody.OnCollision -= OnCollistion;
            _physicsWorld.Remove(_physicsBody);
        }

        private void OnCollistion(object owner)
        {
            if (owner != null && owner is IApplyDamage applyDamage)
            {
                _physicsBody.IsInvulnerable = true;
                applyDamage.TakeDamage(Damage, true);
            }

            Destroy(gameObject);
        }
    }
}