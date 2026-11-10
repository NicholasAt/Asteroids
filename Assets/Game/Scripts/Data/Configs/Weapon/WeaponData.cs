using Assets.Game.Scripts.Data.Configs.Weapon.Configs;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Game.Scripts.Data.Configs.Weapon
{
    [CreateAssetMenu(menuName = "Data/Gameplay/Weapon data/All data")]
    public class WeaponData : ScriptableObject
    {
        [SerializeField] private List<BaseWeaponConfig> _weaponConfigs;
        private readonly Dictionary<WeaponId, BaseWeaponConfig> _cachedConfigs = new();

        public T GetWeaponConfig<T>(WeaponId id) where T : BaseWeaponConfig
        {
            LazyInit();
            return _cachedConfigs.TryGetValue(id, out BaseWeaponConfig config) ? config as T : null;
        }
        private void LazyInit()
        {
            if (_cachedConfigs.Count != 0)
                return;

            foreach (BaseWeaponConfig config in _weaponConfigs)
            {
                if (config == null)
                    continue;

                _cachedConfigs.Add(config.WeaponId, config);
            }
        }
    }
}