using Assets.Scripts.Data.Enemy;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Physics;
using Assets.Scripts.Gameplay.Signals;
using System;
using Zenject;

namespace Assets.Scripts.Gameplay.Enemy
{
    public class EnemyBehaviour : IEnemyBehaviour
    {
        protected readonly PhysicsBody PhysicsBody;

        private readonly EnemyType _enemyType;
        private readonly PhysicsWorld _physicsWorld;
        private readonly SignalBus _signalBus;
        public Action OnDestroy { get; set; }
        private bool _destroyed;

        public EnemyBehaviour(EnemyType enemyType, PhysicsWorld physicsWorld, PhysicsBody physicsBody, SignalBus signalBus)
        {
            _enemyType = enemyType;
            _physicsWorld = physicsWorld;
            PhysicsBody = physicsBody;
            _signalBus = signalBus;
            PhysicsBody.Owner = this;
        }

        public virtual void Update(float deltaTime)
        {

        }

        public void TakeDamage(float damage)
        {
            _signalBus.Fire(new EnemyDeathSignal(_enemyType, PhysicsBody.Position));
            Destroy();
        }

        public void Destroy()
        {
            if (_destroyed)
                return;
            _destroyed = true;

            OnDestroy?.Invoke();
            _physicsWorld.Remove(PhysicsBody);
        }
    }
}