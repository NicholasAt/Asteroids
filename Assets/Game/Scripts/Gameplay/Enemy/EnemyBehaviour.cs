using Assets.Game.Scripts.Data;
using Assets.Game.Scripts.Data.Configs.Enemy;
using Assets.Game.Scripts.Data.Configs.Settings;
using Assets.Game.Scripts.Gameplay.Interfaces.Common;
using Assets.Game.Scripts.Gameplay.Interfaces.Services;
using Assets.Game.Scripts.Gameplay.Physics;
using Assets.Game.Scripts.Gameplay.Signals;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Gameplay.Enemy
{
    public class EnemyBehaviour : IEnemyBehaviour
    {
        protected readonly PhysicsBody PhysicsBody;

        private readonly bool _canSplit;
        private readonly float _force;
        private readonly float _collisionDamage;
        private readonly PhysicsWorld _physicsWorld;
        private readonly SignalBus _signalBus;
        private readonly IEnemyRegistry _enemyRegistry;
        private readonly IWorldBounds _worldBounds;

        public EnemyType EnemyType { get; }
        public Vector2 Position => PhysicsBody.Position;

        public event Action OnShow;
        public event Action OnDie;

        private bool _active;

        public EnemyBehaviour(EnemyType enemyType, EnemyParams parameters, bool canSplit, PhysicsWorld physicsWorld, PhysicsBody physicsBody, SignalBus signalBus, IEnemyRegistry enemyRegistry, IWorldBounds worldBounds)
        {
            EnemyType = enemyType;
            _canSplit = canSplit;
            _force = parameters.Force;
            _collisionDamage = parameters.CollisionDamage;
            _physicsWorld = physicsWorld;
            PhysicsBody = physicsBody;
            _signalBus = signalBus;
            _enemyRegistry = enemyRegistry;
            _worldBounds = worldBounds;
            PhysicsBody.Owner = this;
        }

        public void TakeDamage(float damage, HitType hitType)
        {
            bool spawnFragments = _canSplit && hitType == HitType.Bullet;

            _enemyRegistry.Remove(this);
            _signalBus.Fire(new EnemyDeathSignal(EnemyType, PhysicsBody.Position, spawnFragments));
            Hide();
        }
        public void Show(Vector2 position, Vector2 direction)
        {
            if (_active)
                return;
            _active = true;

            PhysicsBody.Position = position;
            PhysicsBody.Direction = direction;
            PhysicsBody.Velocity = direction * _force;
            PhysicsBody.OnCollision += OnCollision;
            _enemyRegistry.Add(this);
            _physicsWorld.Add(PhysicsBody);
            _worldBounds.AddOutMap(this);
            OnShown();
            OnShow?.Invoke();
        }
        public void Hide()
        {
            if (_active == false)
                return;
            _active = false;

            PhysicsBody.OnCollision -= OnCollision;
            _enemyRegistry.Remove(this);
            _physicsWorld.Remove(PhysicsBody);
            _worldBounds.Remove(this);
            OnHidden();
            OnDie?.Invoke();
        }

        public virtual void Wrap(Vector2 newPosition)
        {
            PhysicsBody.Position = newPosition;
        }

        protected virtual void OnShown()
        { }

        protected virtual void OnHidden()
        { }

        private void OnCollision(object obj)
        {
            if (obj != null && obj is IApplyDamage damage)
                damage.TakeDamage(_collisionDamage, HitType.Collision);
        }
    }
}