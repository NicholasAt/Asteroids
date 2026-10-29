using Assets.Scripts.Data.Enemy;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Physics;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.Enemy
{
    public class Saucer : EnemyBehaviour
    {
        private readonly ICharacterRegister _characterRegister;

        public Saucer(EnemyType enemyType, ICharacterRegister characterRegister, PhysicsWorld physicsWorld, PhysicsBody physicsBody, SignalBus signalBus) : base(enemyType, physicsWorld, physicsBody, signalBus)
        {
            _characterRegister = characterRegister;
        }

        public override void Update(float _)
        {
            Vector2 direction = (_characterRegister.PlayerPosition - PhysicsBody.Position).normalized;
            PhysicsBody.Velocity = direction;
            PhysicsBody.Direction = direction;
        }
    }
}