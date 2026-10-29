using Assets.Scripts.Data.Enemy;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Physics;
using Assets.Scripts.Gameplay.Signals;
using System;
using Zenject;

namespace Assets.Scripts.Gameplay.Enemy
{
    public class Enemy
    {
        public readonly PhysicsBody PhysicsBody;

        private readonly EnemyType _enemyType;
        private readonly PhysicsWorld _physicsWorld;
        private readonly SignalBus _signalBus;
        public Action OnDestroy { get; set; }
        private bool _destroyed;

        public Enemy(EnemyType enemyType, PhysicsWorld physicsWorld, PhysicsBody physicsBody, SignalBus signalBus)
        {
            _enemyType = enemyType;
            _physicsWorld = physicsWorld;
            PhysicsBody = physicsBody;
            _signalBus = signalBus;
        }

        public void Init()
        {
            PhysicsBody.OnCollision += OnCollision;
        }

        public void Destroy()
        {
            if (_destroyed)
                return;
            _destroyed = true;

            OnDestroy?.Invoke();
            PhysicsBody.OnCollision -= OnCollision;
            _physicsWorld.Remove(PhysicsBody);
        }

        private void OnCollision(object owner)
        {
            if (owner is IInflictDamage)
            {
                _signalBus.Fire(new EnemyDeathSignal(_enemyType, PhysicsBody.Position));
                Destroy();
            }
        }
    }
}