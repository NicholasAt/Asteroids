using Assets.Scripts.Data.Configs.Weapon;
using UnityEngine;

namespace Assets.Scripts.Data.Configs.Weapon.Configs
{
    public abstract class BaseWeaponConfig : ScriptableObject
    {
        public const string MenuName = "Data/Gameplay/Weapon data/Configs/";
        [field: SerializeField] public WeaponId WeaponId { get; private set; }
    }
}