using Assets.Scripts.Data.Enemy;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Physics;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.Enemy
{
    public class Saucer : EnemyBehaviour
    {
        private readonly IPlayerPosition _playerPositionService;

        public Saucer(EnemyType enemyId, IPlayerPosition playerPositionService, PhysicsWorld physicsWorld, PhysicsBody physicsBody, SignalBus signalBus, IEnemyRegistry enemyRegistry, IWorldBounds worldBounds) : base(enemyId, physicsWorld, physicsBody, signalBus, enemyRegistry, worldBounds)
        {
            _playerPositionService = playerPositionService;
        }

        public override void Update(float _)
        {
            Vector2 direction = (_playerPositionService.PlayerPosition - PhysicsBody.Position).normalized;
            PhysicsBody.Velocity = direction;
            PhysicsBody.Direction = direction;
        }
        public override void Wrap(Vector2 _)
        { }
    }
}