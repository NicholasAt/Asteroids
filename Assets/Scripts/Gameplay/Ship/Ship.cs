using Assets.Scripts.Data.Ship;
using Assets.Scripts.Gameplay.Physics;
using Assets.Scripts.Gameplay.Weapon;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Ship
{
    public class Ship
    {
        private readonly IPhysicsWorld _physicsWorld;
        private readonly ShipData _shipData;

        private readonly PhysicsBody _physicsBody;
        private IWeapon _weapon1;
        private IWeapon _weapon2;

        public Ship(IPhysicsWorld physicsWorld, ShipData shipData, PhysicsBody physicsBody)
        {
            _physicsWorld = physicsWorld;
            _shipData = shipData;
            _physicsBody = physicsBody;
        }

        public void SetWeapon(IWeapon weapon1, IWeapon weapon2)
        {
            _weapon1 = weapon1;
            _weapon2 = weapon2;
        }

        public void Update(Vector2 moveAxis, bool isAttack1, bool isAttack2)
        {
            _weapon1?.Update(isAttack1);
            _weapon2?.Update(isAttack2);
            _physicsBody.Direction = moveAxis;
        }

        public Vector2 GetPosition()
        {
            return _physicsBody.Position;
        }

        public void Destroy()
        {
            _physicsWorld.Remove(_physicsBody);
        }
    }
}