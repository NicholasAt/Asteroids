using Assets.Game.Scripts.Data.Configs.Weapon;
using Assets.Game.Scripts.Data.Models.Weapon;
using Assets.Game.Scripts.Gameplay.Interfaces.Common;
using Assets.Game.Scripts.Gameplay.Physics;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Interfaces.Services.Factory
{
    public interface IWeaponFactory
    {
        (IWeapon, IWeaponModel) CreateWeapon(WeaponId weaponId, Transform parent, PhysicsBody physicsBody);
    }
}