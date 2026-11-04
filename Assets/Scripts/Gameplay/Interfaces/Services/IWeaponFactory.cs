using Assets.Scripts.Data.Configs.Weapon;
using Assets.Scripts.Data.Models.Weapon;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Physics;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Interfaces.Services
{
    public interface IWeaponFactory
    {
        (IWeapon, IWeaponModel) CreateWeapon(WeaponId weaponId, Transform parent, PhysicsBody physicsBody);
    }
}