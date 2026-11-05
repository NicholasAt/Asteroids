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
    public class EnemyBehaviour : IEnemyBehaviour
    {
        protected readonly PhysicsBody PhysicsBody;

        private readonly EnemyType _enemyId;
        private readonly bool _isSplit;
        private readonly PhysicsWorld _physicsWorld;
        private readonly SignalBus _signalBus;
        private readonly IEnemyRegistry _enemyRegistry;
        private readonly IWorldBounds _worldBounds;

        public Vector2 Position => PhysicsBody.Position;

        public event Action OnShow;
        public event Action OnDie;

        private bool _active;

        public EnemyBehaviour(EnemyType enemyId, bool isSplit, PhysicsWorld physicsWorld, PhysicsBody physicsBody, SignalBus signalBus, IEnemyRegistry enemyRegistry, IWorldBounds worldBounds)
        {
            _enemyId = enemyId;
            _isSplit = isSplit;
            _physicsWorld = physicsWorld;
            PhysicsBody = physicsBody;
            _signalBus = signalBus;
            _enemyRegistry = enemyRegistry;
            _worldBounds = worldBounds;
            PhysicsBody.Owner = this;
        }

        public void TakeDamage(float damage, bool isSplit)
        {
            _signalBus.Fire(new EnemyDeathSignal(_enemyId, PhysicsBody.Position, isSplit));
            Hide();
        }
        public void Show(Vector2 position, Vector2 direction)
        {
            if (_active)
                return;
            _active = true;

            PhysicsBody.Position = position;
            PhysicsBody.Direction = direction;
            PhysicsBody.OnCollision += OnCollision;
            _enemyRegistry.Add(_isSplit, this);
            _physicsWorld.Add(PhysicsBody);
            _worldBounds.AddOutMap(this);
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
            OnDie?.Invoke();
        }
        public virtual void Update(float deltaTime)
        { }

        public virtual void Wrap(Vector2 newPosition)
        {
            PhysicsBody.Position = newPosition;
        }

        private void OnCollision(object obj)
        {
            if (obj != null && obj is IApplyDamage damage)
                damage.TakeDamage(1, false);//default values
        }
    }
}