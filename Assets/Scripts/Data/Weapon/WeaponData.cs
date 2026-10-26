using Assets.Scripts.Data.Weapon.Configs;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data.Weapon
{
    [CreateAssetMenu(menuName = "Data/Gameplay/Weapon data/All data")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private List<BaseWeaponConfig> _weaponConfigs;
        public BaseWeaponConfig GetWeaponConfig(WeaponId id)
        {
            foreach (BaseWeaponConfig cfg in _weaponConfigs)
            {
                if (cfg.WeaponId == id)
                    return cfg;
            }
            Debug.LogError($"no cfg [{id}]");
            return null;
        }
    }
}