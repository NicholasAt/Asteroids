using Assets.Scripts.Data.Configs.Enemy;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Physics;
using Assets.Scripts.Gameplay.Signals;
using System;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.Enemy
{
    public class EnemyBehaviour : IEnemyBehaviour, IDisposable
    {
        protected readonly PhysicsBody PhysicsBody;

        private readonly EnemyType _enemyId;
        private readonly PhysicsWorld _physicsWorld;
        private readonly SignalBus _signalBus;
        private readonly IEnemyRegistry _enemyRegistry;
        private readonly IWorldBounds _worldBounds;

        public Action OnDestroy { get; set; }

        public Vector2 Position => PhysicsBody.Position;

        private bool _destroyed;

        public EnemyBehaviour(EnemyType enemyId, PhysicsWorld physicsWorld, PhysicsBody physicsBody, SignalBus signalBus, IEnemyRegistry enemyRegistry, IWorldBounds worldBounds)
        {
            _enemyId = enemyId;
            _physicsWorld = physicsWorld;
            PhysicsBody = physicsBody;
            _signalBus = signalBus;
            _enemyRegistry = enemyRegistry;
            _worldBounds = worldBounds;
            PhysicsBody.Owner = this;
            PhysicsBody.OnCollision += OnCollision;
        }

        public void Dispose()
        {
            PhysicsBody.OnCollision -= OnCollision;
        }

        public void TakeDamage(float damage)
        {
            _signalBus.Fire(new EnemyDeathSignal(_enemyId, PhysicsBody.Position));
            Destroy();
        }

        public virtual void Update(float deltaTime)
        { }

        public virtual void Wrap(Vector2 newPosition)
        {
            PhysicsBody.Position = newPosition;
        }

        public void Destroy()
        {
            if (_destroyed)
                return;
            _destroyed = true;

            _enemyRegistry.Remove(this);
            _physicsWorld.Remove(PhysicsBody);
            _worldBounds.Remove(this);
            OnDestroy?.Invoke();
        }
        private void OnCollision(object obj)
        {
            if (obj != null && obj is IApplyDamage damage)
                damage.TakeDamage(1);
        }
    }
}