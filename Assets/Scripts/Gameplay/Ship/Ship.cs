using Assets.Scripts.Data.Models;
using Assets.Scripts.Data.Ship;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Physics;
using Assets.Scripts.Gameplay.Signals;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.Ship
{
    public class Ship : IWrapped, IApplyDamage
    {
        public readonly PhysicsBody PhysicsBody;
        private readonly IPlayerPosition _playerPosition;
        private readonly IWorldBounds _worldBounds;
        private readonly PhysicsWorld _physicsWorld;
        private readonly ShipData _shipData;
        private readonly SignalBus _signalBus;
        private readonly Health _health;
        private readonly Move.Move _move;
        private IWeapon _weapon1;
        private IWeapon _weapon2;

        public Vector2 Position => PhysicsBody.Position;

        public Ship(PhysicsWorld physicsWorld, SignalBus signalBus, ShipData shipData, Health health, Move.Move move, PhysicsBody physicsBody, IPlayerPosition playerPosition, IWorldBounds worldBounds)
        {
            PhysicsBody = physicsBody;
            _physicsWorld = physicsWorld;
            _signalBus = signalBus;
            _shipData = shipData;
            _health = health;
            _move = move;
            _playerPosition = playerPosition;
            _worldBounds = worldBounds;
        }

        public void SetWeapon(IWeapon weapon1, IWeapon weapon2)
        {
            _weapon1 = weapon1;
            _weapon2 = weapon2;
        }

        public void Update(float deltaTime, bool isAttack1, bool isAttack2)
        {
            _weapon1?.Update(isAttack1, deltaTime);
            _weapon2?.Update(isAttack2, deltaTime);
            _move.Update();
            _playerPosition.SetPosition(Position);
        }

        public void TakeDamage(float _)
        {
            _health.ChangeHealth(_health.CurrentHealth - 1);
            _signalBus.Fire(new ShipChangeHealthSignal(_health.CurrentHealth));
        }

        public void Wrap(Vector2 newPosition)
        {
            PhysicsBody.Position = newPosition;
            _playerPosition.SetPosition(Position);
        }

        public void Destroy()
        {
            _physicsWorld.Remove(PhysicsBody);
            _worldBounds.Remove(this);
        }
    }
}