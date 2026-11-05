using Assets.Scripts.Data.Configs.Weapon;
using Assets.Scripts.Data.Configs.Weapon.Configs;
using Assets.Scripts.Data.Models.Weapon;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Interfaces.Services.Pool;
using Assets.Scripts.Gameplay.Physics;
using Assets.Scripts.Gameplay.Weapon.Gun;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Services.Factories.Weapon
{
    public class GunCreator : IWeaponCreator
    {
        private readonly WeaponData _weaponData;
        private readonly IBulletPool _bulletPool;

        public WeaponId[] WeaponIds => new WeaponId[1] { WeaponId.Gun_Gun };

        public GunCreator(WeaponData weaponData, IBulletPool bulletPool)
        {
            _weaponData = weaponData;
            _bulletPool = bulletPool;
        }

        (IWeapon, IWeaponModel) IWeaponCreator.Create(WeaponId weaponId, Transform parent, PhysicsBody physicsBody)
        {
            Gun_WeaponConfig config = _weaponData.GetWeaponConfig<Gun_WeaponConfig>(weaponId);
            Gun_Weapon weapon = new Gun_Weapon(config, physicsBody, _bulletPool);

            return (weapon, null);
        }
    }
}