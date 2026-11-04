using Assets.Scripts.Data.Configs.Weapon;
using Assets.Scripts.Data.Configs.Weapon.Configs;
using Assets.Scripts.Data.Models.Weapon;
using Assets.Scripts.Gameplay.Interfaces.Common;
using Assets.Scripts.Gameplay.Physics;
using Assets.Scripts.Gameplay.Weapon.Constant;
using UnityEngine;

namespace Assets.Scripts.Gameplay.Services.Factories.Weapon
{
    public class ConstantCreator : IWeaponCreator
    {
        public WeaponId[] WeaponIds => new WeaponId[1] { WeaponId.Constant_Laser };

        private readonly WeaponData _weaponData;

        public ConstantCreator(WeaponData weaponData)
        {
            _weaponData = weaponData;
        }

        (IWeapon, IWeaponModel) IWeaponCreator.Create(WeaponId weaponId, Transform parent, PhysicsBody physicsBody)
        {
            Constant_WeaponConfig config = _weaponData.GetWeaponConfig<Constant_WeaponConfig>(weaponId);
            Constant_WeaponModel model = new Constant_WeaponModel();

            GameObject instance = Object.Instantiate(config.VisualPrefab);
            instance.transform.SetParent(parent);
            LineRenderer lineRenderer = instance.GetComponent<LineRenderer>();

            Constant_WeaponEffect laserEffect = new Constant_WeaponEffect(physicsBody, lineRenderer);
            Constant_Weapon weapon = new Constant_Weapon(config, model, laserEffect, physicsBody);

            return (weapon, model);
        }
    }
}