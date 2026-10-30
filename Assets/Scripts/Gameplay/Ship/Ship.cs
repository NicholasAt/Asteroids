using Assets.Scripts.Data.Ship;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Physics;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Ship
{
    public class Ship : IWrapped
    {
        private readonly IPlayerPosition _playerPosition;
        private readonly IWorldBounds _worldBounds;
        public readonly PhysicsBody PhysicsBody;
        private readonly PhysicsWorld _physicsWorld;
        private readonly ShipData _shipData;
        private readonly Move.Move _move;
        private IWeapon _weapon1;
        private IWeapon _weapon2;

        public Vector2 Position => PhysicsBody.Position;

        public Ship(PhysicsWorld physicsWorld, ShipData shipData, Move.Move move, PhysicsBody physicsBody, IPlayerPosition playerPosition, IWorldBounds worldBounds)
        {
            _physicsWorld = physicsWorld;
            _shipData = shipData;
            _move = move;
            PhysicsBody = physicsBody;
            _playerPosition = playerPosition;
            _worldBounds = worldBounds;
        }

        public void SetWeapon(IWeapon weapon1, IWeapon weapon2)
        {
            _weapon1 = weapon1;
            _weapon2 = weapon2;
        }

        public void Update(float deltaTime, Vector2 moveAxis, bool isAttack1, bool isAttack2)
        {
            _weapon1?.Update(isAttack1, deltaTime);
            _weapon2?.Update(isAttack2, deltaTime);
            _move.Update(moveAxis.y > 0, moveAxis.x);
            _playerPosition.SetPosition(Position);
        }

        public void Wrap(Vector2 newPosition)
        {
            PhysicsBody.Position = newPosition;
        }

        public void Destroy()
        {
            _physicsWorld.Remove(PhysicsBody);
            _worldBounds.Remove(this);
        }
    }
}