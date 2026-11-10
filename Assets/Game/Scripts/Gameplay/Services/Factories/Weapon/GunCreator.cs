using Assets.Game.Scripts.Data.Configs.Settings;
using Assets.Game.Scripts.Data.Configs.Weapon;
using Assets.Game.Scripts.Data.Models.Weapon;
using Assets.Game.Scripts.Gameplay.Interfaces.Common;
using Assets.Game.Scripts.Gameplay.Interfaces.Services.Pool;
using Assets.Game.Scripts.Gameplay.Physics;
using Assets.Game.Scripts.Gameplay.Weapon.Gun;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Services.Factories.Weapon
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
            Gun weapon = new Gun(_playerSettings.Gun, physicsBody, _bulletPool);

            return (weapon, null);
        }
    }
}
