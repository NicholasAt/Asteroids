using Assets.Game.Scripts.Data.Configs.Enemy;
using Assets.Game.Scripts.Data.Configs.Settings;
using Assets.Game.Scripts.Gameplay.Interfaces.Services;
using Assets.Game.Scripts.Gameplay.Interfaces.Services.Tick;
using Assets.Game.Scripts.Gameplay.Physics;
using UnityEngine;
using Zenject;

namespace Assets.Game.Scripts.Gameplay.Enemy
{
    public class Saucer : EnemyBehaviour, ICustomTick
    {
        private readonly IProgressService _progressService;
        private readonly ITickService _tickService;

        public Saucer(IProgressService progressService, ITickService tickService, EnemyType enemyType, EnemyParams parameters, PhysicsWorld physicsWorld, PhysicsBody physicsBody, SignalBus signalBus, IEnemyRegistry enemyRegistry, IWorldBounds worldBounds) : base(enemyType, parameters, false, physicsWorld, physicsBody, signalBus, enemyRegistry, worldBounds)
        {
            _progressService = progressService;
            _tickService = tickService;
        }

        public void Tick(float _)
        {
            Vector2 shipPosition = _progressService.ShipProgress.Position;
            Vector2 direction = (shipPosition - PhysicsBody.Position).normalized;
            PhysicsBody.Direction = direction;
        }

        public override void Wrap(Vector2 _)
        { }

        protected override void OnShown()
        {
            _tickService.Add(this);
        }

        protected override void OnHidden()
        {
            _tickService.Remove(this);
        }
    }
}