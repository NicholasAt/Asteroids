using Assets.Scripts.Data.Enemy;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Physics;
using Assets.Scripts.Gameplay.Signals;
using System;
using Zenject;

namespace Assets.Scripts.Gameplay.Enemy
{
    public class EnemyBehaviour : IEnemyBehaviour
    {
        protected readonly PhysicsBody PhysicsBody;

        private readonly EnemyType _enemyId;
        private readonly PhysicsWorld _physicsWorld;
        private readonly SignalBus _signalBus;
        private readonly IEnemyRegistry _enemyRegistry;

        public Action OnDestroy { get; set; }
        private bool _destroyed;

        public EnemyBehaviour(EnemyType enemyId, PhysicsWorld physicsWorld, PhysicsBody physicsBody, SignalBus signalBus, IEnemyRegistry enemyRegistry)
        {
            _enemyId = enemyId;
            _physicsWorld = physicsWorld;
            PhysicsBody = physicsBody;
            _signalBus = signalBus;
            _enemyRegistry = enemyRegistry;
            PhysicsBody.Owner = this;
        }

        public virtual void Update(float deltaTime)
        {

        }

        public void TakeDamage(float damage)
        {
            _signalBus.Fire(new EnemyDeathSignal(_enemyId, PhysicsBody.Position));
            Destroy();
        }

        public void Destroy()
        {
            if (_destroyed)
                return;
            _destroyed = true;

            _enemyRegistry.Remove(this);
            _physicsWorld.Remove(PhysicsBody);
            OnDestroy?.Invoke();
        }
    }
}