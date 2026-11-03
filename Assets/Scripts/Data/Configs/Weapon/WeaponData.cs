using Assets.Scripts.Data.Configs.Weapon.Configs;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Data.Configs.Weapon
{
    [CreateAssetMenu(menuName = "Data/Gameplay/Weapon data/All data")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private List<BaseWeaponConfig> _weaponConfigs;
        private readonly Dictionary<WeaponId, BaseWeaponConfig> _caheConfigs = new();

        public T GetWeaponConfig<T>(WeaponId id) where T : BaseWeaponConfig
        {
            LazyInit();
            return _caheConfigs[id] as T;
        }
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
        private void LazyInit()
        {
            if (_caheConfigs.Count != 0)
                return;

            foreach (BaseWeaponConfig config in _weaponConfigs)
            {
                _caheConfigs.Add(config.WeaponId, config);
            }
        }
    }
}