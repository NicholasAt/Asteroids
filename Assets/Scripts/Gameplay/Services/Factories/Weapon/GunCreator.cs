using Assets.Scripts.Data.Weapon;
using Assets.Scripts.Data.Weapon.Configs;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Interfaces.Services;
using Assets.Scripts.Gameplay.Physics;
using Assets.Scripts.Gameplay.Weapon.Gun;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Services.Factories.Weapon
{
    public class GunCreator : IWeaponCreator
    {
        private readonly WeaponData _weaponData;
        private readonly IBulletFactory _bulletFactory;
        public WeaponId[] WeaponIds => new WeaponId[1] { WeaponId.Gun_Gun };

        public GunCreator(WeaponData weaponData, IBulletFactory bulletFactory)
        {
            _weaponData = weaponData;
            _bulletFactory = bulletFactory;
        }

        public IWeapon Create(WeaponId weaponId, Transform _, PhysicsBody physicsBody) =>
            new Gun_Weapon(_weaponData.GetWeaponConfig<Gun_WeaponConfig>(weaponId), physicsBody, _bulletFactory);

    }
}