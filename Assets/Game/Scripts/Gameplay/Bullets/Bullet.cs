using Assets.Game.Scripts.Data;
using Assets.Game.Scripts.Gameplay.Extensions;
using Assets.Game.Scripts.Gameplay.Interfaces.Common;
using Assets.Game.Scripts.Gameplay.Interfaces.Services.Tick;
using Assets.Game.Scripts.Gameplay.Physics;
using System;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Bullets
{
    public class Bullet : ILaunched, ICustomTick
    {
        public readonly PhysicsBody PhysicsBody;
        private readonly ITickService _tickService;
        private readonly PhysicsWorld _physicsWorld;
        private readonly float _lifeTime;
        private readonly float _damage;

        public event Action OnShow;
        public event Action OnHide;
        private bool _active;
        private float _currentLifeTime;
        public Bullet(PhysicsWorld physicsWorld, PhysicsBody physicsBody, ITickService tickService, float lifeTime, float damage)
        {
            _physicsWorld = physicsWorld;
            PhysicsBody = physicsBody;
            _tickService = tickService;
            _lifeTime = lifeTime;
            _damage = damage;
            PhysicsBody.Owner = this;
        }
        public void Launch(Vector2 position, float rotate, float force, float speedLimit)
        {
            if (_active)
                return;
            _active = true;

            _currentLifeTime = _lifeTime;

            Vector2 direction = rotate.ToRotateToDirection();
            PhysicsBody.Rotation = rotate;
            PhysicsBody.Position = position;
            PhysicsBody.Direction = direction;
            PhysicsBody.Velocity = direction * force;
            PhysicsBody.SpeedLimit = speedLimit;
            PhysicsBody.Force = force;

            PhysicsBody.IsInvulnerable = false;
            PhysicsBody.OnCollision += OnCollision;
            _physicsWorld.Add(PhysicsBody);
            _tickService.Add(this);
            OnShow?.Invoke();
        }
        public void Tick(float deltaTime)
        {
            _currentLifeTime -= deltaTime;
            if (_currentLifeTime < 0)
                Hide();
        }
        public void Hide()
        {
            if (_active == false)
                return;
            _active = false;

            PhysicsBody.OnCollision -= OnCollision;
            _physicsWorld.Remove(PhysicsBody);
            _tickService.Remove(this);
            OnHide?.Invoke();
        }
        private void OnCollision(object owner)
        {
            if (owner != null && owner is IApplyDamage applyDamage)
            {
                PhysicsBody.IsInvulnerable = true;
                applyDamage.TakeDamage(_damage, HitType.Bullet);
            }
            Hide();
        }
    }
}