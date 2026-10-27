using Assets.Scripts.Data.Weapon;
using Assets.Scripts.Data.Weapon.Configs;
using Assets.Scripts.Gameplay.Physics;
using Assets.Scripts.Gameplay.Weapon;
using Assets.Scripts.Gameplay.Weapon.Constant;
using Assets.Scripts.Gameplay.Weapon.Gun;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.Gameplay.Services.Factories
{
    public class WeaponFactory
    {
        private readonly DiContainer _diContainer;
        private readonly WeaponData _weaponData;

        public WeaponFactory(DiContainer diContainer, WeaponData weaponData)
        {
            _diContainer = diContainer;
            _weaponData = weaponData;
        }

        public IWeapon CreateWeapon(WeaponId weaponId, Transform parent, PhysicsBody body)
        {
            IWeapon weapon = null;
            BaseWeaponConfig baseConfig = _weaponData.GetWeaponConfig(weaponId);
            switch (weaponId)
            {
                case WeaponId.Laser:
                    weapon = CreateConstant(baseConfig, parent, body);
                    break;

                case WeaponId.Gun:
                    weapon = CreateGun(baseConfig);
                    break;

                case WeaponId.None:
                default:
                    Debug.LogError($"cant create [{weaponId}]");
                    break;
            }
            return weapon;
        }

        private IWeapon CreateGun(BaseWeaponConfig baseconfig)
        {
            IWeapon weapon = null;
            if (baseconfig is Gun_WeaponConfig config)
            {
                Gun_Weapon gun_Weapon = new Gun_Weapon(config);
                weapon = gun_Weapon;
            }
            else
                CantCast();
            return weapon;
        }

        private IWeapon CreateConstant(BaseWeaponConfig baseconfig, Transform parent, PhysicsBody body)
        {
            IWeapon weapon = null;
            if (baseconfig is Constant_WeaponConfig constantConfig)
            {
                GameObject instance = Object.Instantiate(constantConfig.VisualPrefab);
                instance.transform.SetParent(parent);
                LineRenderer lineRenderer = instance.GetComponent<LineRenderer>();

                Constant_WeaponEffect laserEffect = new Constant_WeaponEffect(body, lineRenderer);
                weapon = new Constant_Weapon(constantConfig, laserEffect, body);
            }
            else
                CantCast();
            return weapon;
        }
        private void CantCast()
        {
            Debug.LogError($"cant cast");
        }
    }
}