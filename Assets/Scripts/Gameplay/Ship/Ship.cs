using Assets.Scripts.Data.Ship;
using Assets.Scripts.Gameplay.Physics;
using Assets.Scripts.Gameplay.Weapon;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Ship
{
    public class Ship
    {
        public readonly PhysicsBody PhysicsBody;

        private readonly PhysicsWorld _physicsWorld;
        private readonly ShipData _shipData;
        private IWeapon _weapon1;
        private IWeapon _weapon2;

        public Ship(PhysicsWorld physicsWorld, ShipData shipData, PhysicsBody physicsBody)
        {
            _physicsWorld = physicsWorld;
            _shipData = shipData;
            PhysicsBody = physicsBody;
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
            PhysicsBody.Direction = moveAxis;
        }

        public void Destroy()
        {
            _physicsWorld.Remove(PhysicsBody);
        }
    }
}