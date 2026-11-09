using Assets.Game.Scripts.Data.Configs.Settings;
using Assets.Game.Scripts.Data.Configs.Weapon;
using Assets.Game.Scripts.Data.Configs.Weapon.Configs;
using Assets.Game.Scripts.Data.Models.Weapon;
using Assets.Game.Scripts.Gameplay.Interfaces.Common;
using Assets.Game.Scripts.Gameplay.Physics;
using Assets.Game.Scripts.Gameplay.Weapon.Constant;
using UnityEngine;

namespace Assets.Game.Scripts.Gameplay.Services.Factories.Weapon
{
    public class ConstantCreator : IWeaponCreator
    {
        public WeaponId[] WeaponIds => new WeaponId[1] { WeaponId.Constant_Laser };

        private readonly WeaponData _weaponData;
        private readonly PlayerSettings _playerSettings;

        public ConstantCreator(WeaponData weaponData, PlayerSettings playerSettings)
        {
            _weaponData = weaponData;
            _playerSettings = playerSettings;
        }

        (IWeapon, IWeaponModel) IWeaponCreator.Create(WeaponId weaponId, Transform parent, PhysicsBody physicsBody)
        {
            Constant_WeaponConfig config = _weaponData.GetWeaponConfig<Constant_WeaponConfig>(weaponId);
            Constant_WeaponModel model = new Constant_WeaponModel();

            GameObject instance = Object.Instantiate(config.VisualPrefab);
            instance.transform.SetParent(parent);
            LineRenderer lineRenderer = instance.GetComponent<LineRenderer>();

            Constant_WeaponEffect laserEffect = new Constant_WeaponEffect(physicsBody, lineRenderer);
            Constant_Weapon weapon = new Constant_Weapon(_playerSettings.Laser, model, laserEffect, physicsBody);

            return (weapon, model);
        }
    }
}