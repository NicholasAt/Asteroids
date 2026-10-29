using Assets.Scripts.Data.Weapon;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Physics;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Interfaces.Services
{
    public interface IWeaponFactory
    {
        IWeapon CreateWeapon(WeaponId weaponId, Transform parent, PhysicsBody physicsBody);
    }
}