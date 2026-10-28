using Assets.Scripts.Data.Enemy;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Physics;
using Assets.Scripts.Gameplay.Signals;
using System;
using Zenject;

namespace Assets.Scripts.Gameplay.Enemy
{
    public class Asteroid
    {
        private readonly PhysicsWorld _physicsWorld;
        private readonly PhysicsBody _physicsBody;
        private readonly SignalBus _signalBus;
        public Action OnDestroy { get; set; }
        private bool _destroyed;

        public Asteroid(PhysicsWorld physicsWorld, PhysicsBody physicsBody, SignalBus signalBus)
        {
            _physicsWorld = physicsWorld;
            _physicsBody = physicsBody;
            _signalBus = signalBus;
        }

        public void Init()
        {
            _physicsBody.OnCollision += OnCollision;
        }

        public void Destroy()
        {
            if (_destroyed)
                return;
            _destroyed = true;

            OnDestroy?.Invoke();
            _physicsBody.OnCollision -= OnCollision;
            _physicsWorld.Remove(_physicsBody);
        }

        private void OnCollision(object owner)
        {
            if (owner is IInflictDamage)
            {
                _signalBus.Fire(new EnemyDeathSignal(EnemyType.Asteroid, _physicsBody.Position));
                Destroy();
            }
        }
    }
}