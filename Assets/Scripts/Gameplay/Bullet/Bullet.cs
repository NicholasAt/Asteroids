using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Physics;
using System;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Bullet
{
    public class Bullet : IPoolable
    {
        private const int Damage = 1;
        private readonly PhysicsWorld _physicsWorld;
        private readonly PhysicsBody _physicsBody;
        private readonly bool _isSplit;

        public event Action OnShow;
        public event Action OnHide;

        public Bullet(PhysicsWorld physicsWorld, PhysicsBody physicsBody, bool isSplit)
        {
            _physicsWorld = physicsWorld;
            _physicsBody = physicsBody;
            _isSplit = isSplit;
            _physicsBody.Owner = this;
        }

        public void Show(Vector2 position, Vector2 direction)
        {
            _physicsBody.Position = position;
            _physicsBody.Direction = direction;
            _physicsBody.OnCollision += OnCollistion;
            _physicsWorld.Add(_physicsBody);
            OnShow?.Invoke();
        }

        public void Hide()
        {
            _physicsBody.OnCollision -= OnCollistion;
            _physicsWorld.Remove(_physicsBody);
            OnHide?.Invoke();
        }

        private void OnCollistion(object owner)
        {
            if (owner != null && owner is IApplyDamage applyDamage)
            {
                _physicsBody.IsInvulnerable = true;
                applyDamage.TakeDamage(Damage, _isSplit);
            }
            Hide();
        }
    }
}