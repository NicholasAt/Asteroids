using Assets.Scripts.Data.Configs.Enemy;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Physics;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.Enemy
{
    public class Saucer : EnemyBehaviour
    {
        private readonly IProgressService _progressService;

        public Saucer(IProgressService progressService, EnemyType enemyId, bool isSplit, float force, PhysicsWorld physicsWorld, PhysicsBody physicsBody, SignalBus signalBus, IEnemyRegistry enemyRegistry, IWorldBounds worldBounds) : base(enemyId, isSplit, force, physicsWorld, physicsBody, signalBus, enemyRegistry, worldBounds)
        {
            _progressService = progressService;
        }

        public override void Update(float _)
        {
            Vector2 shipPosition = _progressService.ShipProgress.Position;
            Vector2 direction = (shipPosition - PhysicsBody.Position).normalized;
            PhysicsBody.Direction = direction;
        }
        public override void Wrap(Vector2 _)
        { }
    }
}