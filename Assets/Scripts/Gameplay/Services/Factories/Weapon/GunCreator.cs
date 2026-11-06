using Assets.Scripts.Data.Configs.Settings;
using Assets.Scripts.Data.Configs.Weapon;
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
        private readonly PlayerSettings _playerSettings;
        private readonly IBulletPool _bulletPool;

        public WeaponId[] WeaponIds => new WeaponId[1] { WeaponId.Gun_Gun };

        public GunCreator(PlayerSettings playerSettings, IBulletPool bulletPool)
        {
            _playerSettings = playerSettings;
            _bulletPool = bulletPool;
        }

        (IWeapon, IWeaponModel) IWeaponCreator.Create(WeaponId weaponId, Transform parent, PhysicsBody physicsBody)
        {
            Gun_Weapon weapon = new Gun_Weapon(_playerSettings.Gun, physicsBody, _bulletPool);

            return (weapon, null);
        }
    }
}
